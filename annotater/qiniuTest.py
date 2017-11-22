#coding:utf-8
import urllib2
from qiniu import Auth, put_file,BucketManager

access_key = 'BrU07WtuzvGD3S1gyWTD55L6A65y188SJMSCVsiQ'
secret_key = 'eeUJK36KFZDwWjWrb8TsuIxadFxOoGXRcj0-0c_B'
bucket_name = 'eyesoct'
domain = 'ovy6ljhqz.bkt.clouddn.com'


key = 'test.jpg'

local_img = 'encrypt.jpg'

q = Auth(access_key,secret_key)


token = q.upload_token(bucket_name, key, 3600)

ret, info = put_file(token, key, local_img)

print ret
print info
print type(ret)
print type(info)
#print help(info)
print info.status_code
print '======= end of upload test ==========='

bucket = BucketManager(q)
ret, info = bucket.stat(bucket_name, key)

print ret
print info
print type(ret)
print type(info)
print '======= end of status test 1 ==========='

ret, info = bucket.delete(bucket_name, key)

print ret
print info
print type(ret)
print type(info)
print '======= end of delete test ==========='

ret, info = bucket.stat(bucket_name, key)

print ret
print info
print type(ret)
print type(info)
print '======= end of status test 1 ==========='