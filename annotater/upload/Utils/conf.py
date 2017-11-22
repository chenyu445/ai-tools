#coding:utf-8
import configparser as cg
import sys
reload(sys)
sys.setdefaultencoding('utf-8')

class Config():
    def __init__(self,conf_path):
        self.conf = cg.ConfigParser()
        self.data = conf_path
        self.conf.read(self.data,encoding='utf-8')

    def getQiNiuAccessKey(self):
        try:
            key = self.conf.get('QINIUYUN','access_key')
            key = str(key)
        except Exception as e:
            print e
            return None
        return key

    def getQiNiuSecretKey(self):
        try:
            key = self.conf.get('QINIUYUN', 'secret_key')
            key = str(key)
        except Exception as e:
            print e
            return None
        return key

    def getQiniuDomain(self):
        try:
            key = self.conf.get('QINIUYUN', 'domain')
            key = str(key)
        except Exception as e:
            print e
            return None
        return key

    def getCreateBucketHost(self):
        try:
            key = self.conf.get('SERVER','createbuckethost')
            key = str(key)
        except Exception as e:
            print e
            return None
        return key

    def getUploadFileHost(self):
        try:
            key = self.conf.get('SERVER','uploadfilehost')
            key = str(key)
        except Exception as e:
            print e
            return None
        return key



    def addBucket(self,name,qiniuKey,domain,publicKey,privateKey,types):
        sections = self.conf.sections()
        temp = 0
        for section in sections:
            if 'BUCKET' in section:
                section = section[6:]
                if temp < int(section):
                    temp = int(section)
        temp += 1
        try:
            section = 'BUCKET%03d'%temp
            types = ','.join(types)
            self.conf.add_section(section)
            self.conf.set(section,'NAME',name)
            self.conf.set(section, 'DOMAIN', domain)
            self.conf.set(section, 'PUBLICKEY', publicKey)
            self.conf.set(section, 'QINIUKEY', qiniuKey)
            self.conf.set(section,'PRIATEKEY',privateKey)
            self.conf.set(section, 'TYPES', types)
            self.conf.write(open(self.data,'w'))
            result = section
        except Exception as e:
            result = None
        return result

    def getBucketInfoByName(self,name):
        sections = self.conf.sections()
        bucketInfo = None
        for section in sections:
            if 'BUCKET' in section:
                bucketName = self.conf.get(section,'name')
                if name == bucketName:
                    bucketInfo = self.conf.items(section)
                    bucketInfo = {str(info[0]):str(info[1]) for info in bucketInfo}
        return bucketInfo



if __name__ == '__main__':
    conf = Config('config.conf')
    info = conf.getBucketInfoByName('eyesoct')
    conf.addBucket('aaa','bbb','ccc','ddd','eee',['a','b','c','d'])
    for key in dict.keys(info):
        print info[key]
