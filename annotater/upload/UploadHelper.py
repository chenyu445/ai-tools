#coding:utf-8
import base64
import json
import hashlib
import os
import urllib2
from Crypto import Random
from Crypto.Cipher import PKCS1_v1_5 as Cipher_pkcs1_v1_5
from Crypto.PublicKey import RSA

from qiniu import Auth, put_file,BucketManager
import EncryptHelper
from Utils import conf,LogUtils

base_name = os.path.basename(__file__)
name = base_name.split('.')[0]
mylog = LogUtils.MyLog(os.path.join('/tmp', name) + '.log')
conf_name = 'config.conf'

class UploadHelper():

    def __init__(self):
        try:
            root_path = os.path.dirname(__file__)
            conf_path = os.path.join(root_path, 'Utils')
            conf_path = os.path.join(conf_path, conf_name)
            self.config = conf.Config(conf_path)
            self.access_key = self.config.getQiNiuAccessKey()
            self.secret_key = self.config.getQiNiuSecretKey()
            self.domain = self.config.getQiniuDomain()
            self.local_path = 'local.jpg'
            self.q = Auth(self.access_key,self.secret_key)
            self.createBucketHost = self.config.getCreateBucketHost()
            self.uploadFileHost = self.config.getUploadFileHost()
            mylog.info('success init upload helper')
        except Exception as e:
            mylog.error('fail to init upload helper : %s'%str(e))
            raise AssertionError('fail to init upload helper : %s'%str(e))

    def createBucket(self,name,domain,types):
        try:

            PUBLICKEY = name+'-'+'public.key'
            PRIVATEKEY = name+'-'+'private.key'
            qiniuKey = md5_for_file(PRIVATEKEY)

            section = self.config.addBucket(name,qiniuKey,domain,PUBLICKEY,PRIVATEKEY,types)
            if section:
                mylog.info('success add bucket : %s to conf'%section)
            else:
                mylog.info('fail to add bucket')
                raise AssertionError('fail to add bucket')

            random_generator = Random.new().read
            rsa = RSA.generate(1024, random_generator)
            privateKey = rsa.exportKey()
            publicKey = rsa.publickey().exportKey()
            if os.path.exists(PUBLICKEY):
                f = open(PUBLICKEY, 'rb')
                publicKey = f.read()
                f.close()
            else:
                f = open(PUBLICKEY, 'wb')
                f.write(publicKey)
                f.close()

            if os.path.exists(PRIVATEKEY):
                f = open(PRIVATEKEY, 'rb')
                privateKey = f.read()
                f.close()
            else:
                f = open(PRIVATEKEY, 'wb')
                f.write(privateKey)
                f.close()
            self.publicKey = publicKey
            self.privateKey = privateKey
            mylog.info('success init or read publickey and privatekey')
            bucket = BucketManager(self.q)
            ret, info = bucket.stat(name, qiniuKey)
            if info.status_code == 200:
                mylog.error('privatekey already uploaded in qiniuyun ,please check manually')
                raise AssertionError('privatekey already uploaded in qiniuyun ,please check manually')
            token = self.q.upload_token(name, qiniuKey, 3600)
            ret, info = put_file(token, qiniuKey, PRIVATEKEY)
            status_code = info.status_code
            if status_code == 200:
                mylog.info('success upload private key to qiniuyun')
                body_value = {"name": name, "qiniukey": qiniuKey, "domain": domain, 'filetypes': types}
                headers = {'Content-Type': 'application/json'}
                request = urllib2.Request(url=self.createBucketHost, headers=headers, data=json.dumps(body_value))
                result = urllib2.urlopen(request).read()
                result = json.loads(result)
                if result['code'] == 1:
                    mylog.info('success create bucket in sql')
                else:
                    mylog.warning('fail to create bucket in sql')
                    bucket = BucketManager(self.q)
                    ret, info = bucket.delete(name, qiniuKey)
                    status_code = info.status_code
                    if status_code == 200:
                        mylog.info('success delete private key from qiniuyun')
                    else:
                        mylog.error('fail to delete private key from qiniuyun')
            else:
                mylog.error('fail to upload private key to qiniuyun')
        except  Exception as e:
            mylog.error('fail to create bucket :%s  :%s'%(name,str(e)))

    def uploadFile(self,bucket_name,local_img):
        print 'start...'
        img_base_name = md5_for_file(local_img)
        print img_base_name
        f = open(local_img,'rb')
        g = f.read()
        picture_length = len(g)
        f.close()

        try:
            PUBLICKEY = bucket_name + '-' + 'public.key'
            publicKey = None
            if os.path.exists(PUBLICKEY):
                f = open(PUBLICKEY, 'rb')
                publicKey = f.read()
                f.close()
            key, iv = EncryptHelper.encryptImg(local_img, self.local_path)
            key = key + iv
            if publicKey is None:
                raise AssertionError("public key is None")
            rsaKey = RSA.importKey(publicKey)
            cipher = Cipher_pkcs1_v1_5.new(rsaKey)
            key = cipher.encrypt(key)
            key = base64.b64encode(key)
            body_value = {"bucket_name": bucket_name, "qiniu_key": img_base_name, "encrypt_key": key,"picture_length":picture_length}
            headers = {'Content-Type': 'application/json'}
            request = urllib2.Request(url=self.uploadFileHost, headers=headers, data=json.dumps(body_value))
            result = urllib2.urlopen(request).read()
            result = json.loads(result)
            if result['code'] == 1:
                mylog.info('success insert file : %s  into sql'%img_base_name)
                token = self.q.upload_token(bucket_name, img_base_name, 3600)
                ret, info = put_file(token, img_base_name, self.local_path)
                if info.status_code == 200:
                    mylog.info('success upload file : %s  to qiniuyun' % img_base_name)
                    mylog.info('======== end of upload file ===========')
                else:
                    mylog.error('fail to upload file : %s  to qiniuyun' % img_base_name)
                    mylog.error('please check the file in sql manually')
                    mylog.info('======== end of upload file ===========')
            else:
                mylog.error('fail to insert file : %s  into sql' % img_base_name)
        except Exception as e:
            mylog.error('fail to upload file : %s  : %s  ' % (img_base_name,str(e)))


def md5_for_file(file_path, block_size=2**20):
    f = open(file_path,'rb')
    md5 = hashlib.md5()
    while True:
        data = f.read(block_size)
        if not data:
            break
        md5.update(data)
    f.close()
    return md5.hexdigest()

def test(imgs_path):
    name = 'eyesoct'
    filetypes = ['正常', '神经上皮脱离', '色素上皮层脱离', '视网膜前膜', '玻璃体黄斑中心牵拉','囊样水肿','黄斑裂孔','视网膜脱离','其他']
    helper = UploadHelper()
    #helper.createBucket(name, helper.domain, filetypes)

    i = 0
    for root, dirs, imgs in os.walk(imgs_path):
        for img in imgs:
            #if img.endswith('.jpg'):
            if 'cache_' in img:
                print img
                helper.uploadFile(name, os.path.join(root, img))
                i += 1
            print i
            print '=============='
        #     if i > 30:
        #         break
        # if i > 30:
        #     break

def test_md5(imgs_path):
    i = 0
    for root, dirs, imgs in os.walk(imgs_path):
        for img in imgs:
            if img.endswith('.jpg'):
                img_path = os.path.join(root,img)
                img_md5 = md5_for_file(img_path)
                f = open(img_path,'rb')
                g = f.read()
                f.close()
                print img_md5,len(g)
                i += 1
            if i>10:
                break
        if i>10:
            break


if __name__ == '__main__':
    #test()
    local_path = 'anoy/data/20170829'
    aliyun_path = '../../../../data/OCT/save2'
    print os.path.exists(os.path.abspath(aliyun_path))
    test(aliyun_path)

