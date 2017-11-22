#coding:utf-8
import hashlib
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

def str_sha256(passwd):
    sha256 = hashlib.sha256()
    sha256.update(passwd)
    return sha256.hexdigest()

if __name__ == '__main__':
    # file_path = 'eyesoct-private.key'
    # #file_path = 'local.jpg'
    # #file_path = 'testMd5.log'
    # f2 = open(file_path,'rb')
    # print md5_for_file(file_path)
    # data = f2.read()
    # data = data.strip('\n')
    # print len(data)
    # print type(data)
    # md5 = hashlib.md5(data)
    # print md5.hexdigest()
    passwd = 'wang2pass'
    print str_sha256(passwd)
    print str_sha256(str_sha256(passwd)),'---------'
