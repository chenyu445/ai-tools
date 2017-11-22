#coding:utf-8
from Crypto import Random
from Crypto.Cipher import PKCS1_v1_5 as Cipher_pkcs1_v1_5
from Crypto.Signature import PKCS1_v1_5 as Signature_pkcs1_v1_5
from Crypto.PublicKey import RSA
import base64

random_generator = Random.new().read
rsa = RSA.generate(1024,random_generator)

privateKey = rsa.exportKey()
publicKey = rsa.publickey().exportKey()


message = 'nice shoot daw;da;jgs;jgigjsorjawjgsijsohfo2fkafojwfjgag'

rsakey = RSA.importKey(publicKey)
cipher = Cipher_pkcs1_v1_5.new(rsakey)
cipher_text = base64.b64encode(cipher.encrypt(message))
print cipher_text

random_generator2 = Random.new().read
rsakey = RSA.importKey(privateKey)
cipher = Cipher_pkcs1_v1_5.new(rsakey)
text = cipher.decrypt(base64.b64decode(cipher_text), random_generator2)
print text
