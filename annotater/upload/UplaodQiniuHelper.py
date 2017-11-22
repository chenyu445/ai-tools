#coding:utf-8
import base64
import json
import os
import urllib2
from Crypto import Random
from Crypto.Cipher import PKCS1_v1_5 as Cipher_pkcs1_v1_5
from Crypto.PublicKey import RSA

from qiniu import Auth, put_file
import EncryptHelper
import MySQLHelper

access_key = 'BrU07WtuzvGD3S1gyWTD55L6A65y188SJMSCVsiQ'
secret_key = 'eeUJK36KFZDwWjWrb8TsuIxadFxOoGXRcj0-0c_B'
bucket_name = 'eyesoct'
domain = 'ovy6ljhqz.bkt.clouddn.com'
serverHost = 'http://localhost:8003/api/uploadFileInfo'
createBucketHost = 'http://localhost:8003/api/createBucket'
encryptPath = 'encrypt.jpg'
q = Auth(access_key, secret_key)

random_generator = Random.new().read
rsa = RSA.generate(1024,random_generator)
privateKeyDer = rsa.exportKey('DER')
privateKeyDer = base64.b64encode(privateKeyDer)
privateKeyPem = rsa.exportKey()
publicKey = rsa.publickey().exportKey()
PUBLICKEY = bucket_name+'-'+'public.key'
PRIVATEKEYDER = bucket_name+'-'+'privateder.key'
PRIVATEKEYPEM = bucket_name+'-'+'privatepem.key'
if os.path.exists(PUBLICKEY):
    f = open(PUBLICKEY,'rb')
    publicKey = f.read()
    f.close()
else:
    f = open(PUBLICKEY,'wb')
    f.write(publicKey)
    f.close()

if os.path.exists(PRIVATEKEYDER):
    f = open(PRIVATEKEYDER, 'rb')
    privateKeyDer= f.read()
    f.close()
else:
    f = open(PRIVATEKEYDER, 'wb')
    f.write(privateKeyDer)
    f.close()
if os.path.exists(PRIVATEKEYPEM):
    f = open(PRIVATEKEYPEM, 'rb')
    privateKeyPem= f.read()
    f.close()
else:
    f = open(PRIVATEKEYPEM, 'wb')
    f.write(privateKeyPem)
    f.close()
print publicKey
print privateKeyDer
print privateKeyPem
qiniuKey = PRIVATEKEYPEM

name = 'eyesoct'
bucketKey = 'eyesoct'
filetypes = ['正常', '神经上皮脱离', '色素上皮层脱离','视网膜前膜','其他']
qiniuKey2 = '2015090711111.jpg'
token = q.upload_token(bucket_name, qiniuKey2, 3600)
ret, info = put_file(token, qiniuKey2, qiniuKey)
try:
    a = str(info).split(',')[1].split(':')[1]
except Exception as e:
    a = str(e)
print a
if int(a) == 200:
    result = MySQLHelper.insertBucket(bucket_name, bucket_name, qiniuKey2, domain, filetypes)
    assert result is None

i = 0
for root,dirs,files in os.walk('anoy'):
    for img in files:
        if img.endswith('.jpg'):
            i += 1
            localfile = os.path.join(root,img)
            key,iv = EncryptHelper.encryptImg(localfile,encryptPath)
            key = key + iv
            rsaKey = RSA.importKey(publicKey)
            cipher = Cipher_pkcs1_v1_5.new(rsaKey)
            key = cipher.encrypt(key)
            key = base64.b64encode(key)
            body_value = {"bucket_name": bucket_name, "qiniu_key": img, "encrypt_key": key}
            headers = {'Content-Type': 'application/json'}
            request = urllib2.Request(url=serverHost, headers=headers, data=json.dumps(body_value))
            result = urllib2.urlopen(request).read()
            result = json.loads(result)
            print result
            if result['code']==1:
                token = q.upload_token(bucket_name, img, 3600)
                ret, info = put_file(token, img, encryptPath)
                try:
                    a = str(info).split(',')[1].split(':')[1]
                except Exception as e:
                    a = str(e)
                print a
                if not int(a) == 200:
                    pass
            if i > 10:
                break
    if i > 10:
        break