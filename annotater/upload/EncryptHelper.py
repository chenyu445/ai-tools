#coding:utf-8
import base64
import random
from Crypto import Random
from Crypto.Cipher import AES
from Crypto.Cipher import PKCS1_v1_5 as Cipher_pkcs1_v1_5
from Crypto.Cipher import PKCS1_OAEP as pkcs1oaep
from Crypto.PublicKey import RSA


def geneAESKey():
    x = [chr(y) for y in range(1, 127)]
    key = ''.join(random.sample(x, 16))
    iv = ''.join(random.sample(x, 16))
    return key,iv

def encryptImg(imgPath,enPath):
    key,iv = geneAESKey()
    filebuffsize = 100*1024*1024
    f = open(imgPath,'rb')
    w = open(enPath,'wb')
    cryptor = AES.new(key,AES.MODE_CBC,iv)
    while True:
        g = f.read(filebuffsize)
        length = len(g)
        if length == 0:
            break
        if length < filebuffsize:
            g += '\0'*(16-length%16)
            w.write(cryptor.encrypt(g))
            break
        w.write(cryptor.encrypt(g))
    f.close()
    w.close()
    return key,iv

def decryptImg(enPath,dePath,key,iv):
    filebuffsize = 100 * 1024 * 1024
    f = open(enPath, 'rb')
    w = open(dePath, 'wb')
    cryptor = AES.new(key, AES.MODE_CBC, iv)
    while True:
        g = f.read(filebuffsize)
        length = len(g)
        if length == 0:
            break
        if length < filebuffsize:
            w.write(cryptor.decrypt(g).strip('\0'))
            break
        w.write(cryptor.decrypt(g))
    f.close()
    w.close()

def encryptEn(str,publiKey):
    rsaKey = RSA.importKey(publiKey)
    cipher = Cipher_pkcs1_v1_5.new(rsaKey)
    return cipher.encrypt(str)

def decryptDe(str,privateKey):
    rsaKey = RSA.importKey(privateKey)
    cipher = Cipher_pkcs1_v1_5.new(rsaKey)
    return cipher.decrypt(str,Random.new().read)

def enAndDeImg():
    key, iv = encryptImg('002.jpg', 'eee.jpg')
    decryptImg('eee.jpg', 'ddd.jpg', key, iv)

def enAndDeStr():
    random_generator = Random.new().read
    rsa = RSA.generate(1024,random_generator)
    privateKey = rsa.exportKey()
    publicKey = rsa.publickey().exportKey()
    message = 'nice shoot daw;da;jgs;jgigjsorjawjgsijsohfo2fkafojwfjgag'
    encrypt = encryptEn(message,publicKey)
    print encrypt
    print base64.b64encode(encrypt)
    decrypt = decryptDe(encrypt,privateKey)
    print decrypt
    print decrypt == message

def test():
    x = [chr(y) for y in range(1, 127)]
    key = ''.join(random.sample(x, 16))
    iv = ''.join(random.sample(x, 16))
    print key
    print iv
    print len(key)
    key1 = base64.b64encode(key)
    print key1
    print len(key1)
    key2 = base64.b64decode(key1)
    print key==key2
    obj = AES.new(key, AES.MODE_CBC, iv)
    message = "The answer is no"
    ciphertext = obj.encrypt(message)
    print ciphertext
    obj2 = AES.new(key, AES.MODE_CBC, iv)
    print obj2.decrypt(ciphertext)

    key = key + iv
    print len(key)
    key1 = base64.b64encode(key)
    print key1
    print len(key1)
    key3 = base64.b64decode(key1)
    print key==key3
    print key2 == key3[:16]


if __name__ == '__main__':
    #enAndDeImg()
    #enAndDeStr()
    #test()

    # sss = 'W2d4cYWvvn6+N+NN1GSGI9oTQofiwfVbgxTyHIuCZ5RUo0GMs/wLX7x+6sP9JebvewNEXGwsWNSghZdsrJmX0QOGTXoRTgGRXkPznSxOItxKthkc1Fw89ezT6k1BOen69vgiyrpUmHXANUFLvVq8fTSUIB4L9ucWRK4T1eUcxnA='
    # key2 = base64.b64decode(sss)
    # #print key2
    # #print len(key2)
    # f = open('eyesoct-private.key','rb')
    # privateKey = f.read()
    # key = decryptDe(key2,privateKey)
    # #print key
    # #print len(key)
    # decryptImg('aa','aaa.jpg',key[:16],key[16:])
   # print base64.b64encode(key[:16])
   # print base64.b64encode(key[16:])
    random_generator = Random.new().read
    rsa = RSA.generate(1024, random_generator)
    privateKey = rsa.exportKey()
    privateKey2 = rsa.exportKey('DER')

    print base64.b64encode(privateKey2)
    print privateKey

    print len(privateKey)