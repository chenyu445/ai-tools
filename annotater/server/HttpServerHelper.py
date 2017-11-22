#coding:utf8
import json
import os

import tornado.httpserver
import tornado.ioloop
import tornado.options
import tornado.web

import MySQLHelper
mylog = MySQLHelper.mylog


class BaseHandler(tornado.web.RequestHandler):

    def get(self):
        print("get")

    def set_default_headers(self):
        self.set_header("Access-Control-Allow-Origin", "*")
        self.set_header("Access-Control-Allow-Headers",
                        "Content-Type, Content-Length, Authorization, Accept, X-Requested-With, X-File-Name, yourHeaderFeild")
        self.set_header("Access-Control-Allow-Methods", " POST, GET, OPTIONS, HEAD")
        self.set_header("X-Powered-By", "3.2.1")
        self.set_header("Content-Type", "application/x-www-form-urlencoded")

    def post(self):
        print("post")

    def options(self):
        self.set_status(204)
        self.finish()

    def write_to_client(self, message):
        self.write(json.dumps(message))
        self.set_header("Content-Type", "application/json")

class CreateBucketHandler(BaseHandler):
    '''
    data: name ,bucketKey,types
    表中添加bucket
    表中添加新的分组
    '''

    def get(self):
        message = {"status": 1, "message": 'test for get model', "data": ""}
        self.write_to_client(message)

    def post(self):
        data = self.request.body
        self.process(data)

    def process(self, data):
        dbm = None
        try:
            dbm = MySQLHelper.DataBaseManager()
            mylog.info('param for /api/createBucket : %s' % str(data))
            data = json.loads(data)
            bucket_name = data['name']
            qiniuKey = data['qiniukey']
            domain = data['domain']
            filetypes = data['filetypes']
            result = dbm.insertBucket(bucket_name, bucket_name, qiniuKey, domain, filetypes)
            if result is None:
                message = {"code": 1, "msg": "success", "data":''}
            else:
                message = {"code": 0, "msg": result, "data": ""}
        except Exception as e:
            message = {"code": 0, "msg": str(e), "data": ""}
        if dbm:
            mylog.info('respon for /api/createBucket : %s'%str(message))
            del dbm
        self.write_to_client(message)

class ListBucketsHandler(BaseHandler):
    '''
    data page size

    resp :
    bucket_id,name,bucketKey
    '''

    def get(self):
        message = {"status": 1, "message": 'test for get model', "data": ""}
        self.write_to_client(message)

    def post(self):
        data = self.request.body
        self.process(data)

    def process(self, data):
        dbm = None
        try:
            dbm = MySQLHelper.DataBaseManager()
            mylog.info('param for /api/listBucket : %s' % str(data))
            data = json.loads(data)
            token = data['token']
            result = dbm.userValidateByToken(token)
            if result == '':
                message = {"code": 0, "msg": "toekn error", "data": ''}
            else:
                result = dbm.listBucket()
                print result
                if result == []:
                    message = {"code": 0, "msg": "users empty", "data": ''}
                else:
                    message = {"code": 1, "msg": "success", "data": {"buckets": result}}
        except Exception as e:
            message = {"code": 0, "msg": str(e), "data": ""}
        if dbm:
            mylog.info('respon for /api/listBucket : %s' % str(message))
            del dbm
        self.write_to_client(message)


class GetBucketFileHandler(BaseHandler):

    def get(self):
        message = {"status": 1, "message": 'test for get model', "data": ""}
        self.write_to_client(message)

    def post(self):
        data = self.request.body
        print data
        self.process(data)

    def process(self, data):
        dbm = None
        try:
            dbm = MySQLHelper.DataBaseManager(enableQiniu=True)
            mylog.info('param for /api/getBucketFile : %s' % str(data))
            data = json.loads(data)
            token = data['token']
            result = dbm.userValidateByToken(token)
            if result == '':
                message = {"code": 0, "msg": "toekn error", "data": ''}
            else:
                bucket_name = data['name']
                result = dbm.getBucketFileByName(bucket_name)
                if not result =='':
                    message = {"code": 1, "msg": "success", "data":{"url":result}}
                else:
                    message = {"code": 0, "msg": 'sql error', "data": ''}
        except Exception as e:
            message = {"code": 0, "msg": str(e), "data": ""}
        if dbm:
            mylog.info('respon for /api/getBucketFile : %s' % str(message))
            del dbm
        self.write_to_client(message)


class UploadFileInfoHandler(BaseHandler):

    '''
    data bucket_id,qiniu_key,encrypt_key
    file表中添加一个新的file
    resp ok
    '''

    def get(self):
        message = {"status": 1, "message": 'test for get model', "data": ""}
        self.write_to_client(message)

    def post(self):
        data = self.request.body
        print data
        self.process(data)

    def process(self, data):
        dbm = None
        try:
            dbm = MySQLHelper.DataBaseManager()
            mylog.info('param for /api/uploadFileInfo : %s' % str(data))
            data = json.loads(data)
            qiniu_key = data['qiniu_key']
            encrypt_key = data['encrypt_key']
            bucket_name = data['bucket_name']
            picture_length = data['picture_length']
            result = dbm.insertFile(qiniu_key,encrypt_key,bucket_name,picture_length)
            if result is None:
                message = {"code": 1, "msg": "success", "data":''}
            else:
                message = {"code": 0, "msg": result, "data": ''}
        except Exception as e:
            message = {"code": 0, "msg": str(e), "data": ""}
        if dbm:
            mylog.info('respon for /api/uploadFileInfo : %s' % str(message))
            del dbm
        self.write_to_client(message)


# class delFileInfoHandler(BaseHandler):
#     '''
#     data bucket_id,qiniu_key,encrypt_key
#     file表中添加一个新的file
#     resp ok
#     '''
#
#     def get(self):
#         message = {"status": 1, "message": 'test for get model', "data": ""}
#         self.write_to_client(message)
#
#     def post(self):
#         data = self.request.body
#         print data
#         self.process(data)
#
#     def process(self, data):
#         try:
#             data = json.loads(data)
#             qiniu_key = data['qiniu_key']
#             encrypt_key = data['encrypt_key']
#             bucket_name = data['bucket_name']
#             result = MySQLHelper.insertFile(qiniu_key, encrypt_key, bucket_name)
#             if result is None:
#                 message = {"code": 1, "msg": "success", "data": ''}
#             else:
#                 message = {"code": 0, "msg": result, "data": ''}
#         except Exception as e:
#             message = {"code": 0, "msg": str(e), "data": ""}
#         self.write_to_client(message)


class LogInHandler(BaseHandler):
    '''
    data name passwd
    表中查询，判断是否弃用
    resp token,
    '''

    def get(self):
        message = {"code": 1, "msg": 'test for get model', "data": ""}
        self.write_to_client(message)

    def post(self):
        data = self.request.body
        self.process(data)

    def process(self, data):
        dbm = None
        try:
            dbm = MySQLHelper.DataBaseManager()
            mylog.info('param for /api/login : %s' % str(data))
            data = json.loads(data)
            name = data['name']
            passwd = data['passwd']
            result = dbm.userValidate(name,passwd)
            if not result == '':
                message = {"code": 1, "msg": "success", "data": {"token":result}}
            else:
                message = {"code": 0, "msg":  '', "data": ""}
        except Exception as e:
            message = {"code": 0, "msg": str(e), "data": ""}
        if dbm:
            mylog.info('respon for /api/login : %s' % str(message))
            del dbm
        self.write_to_client(message)


class OwnFileHandler(BaseHandler):
    '''
    data : bucket_id count
    给不超过count个的file加上own属性，返回count个file
    resp (file_id,qiniu_key,encrypt_key)
    '''

    def get(self):
        message = {"status": 1, "message": 'test for get model', "data": ""}
        self.write_to_client(message)

    def post(self):
        data = self.request.body
        self.process(data)

    def process(self, data):
        dbm = None
        try:
            dbm = MySQLHelper.DataBaseManager(enableQiniu=True)
            mylog.info('param for /api/ownFile : %s' % str(data))
            data = json.loads(data)
            token = data['token']
            result = dbm.userValidateByToken(token)
            if result == '':
                message = {"code": 0, "msg": "toekn error", "data": ''}
            else:
                count = 2
                if 'count' in dict.keys(data):
                    count = int(data['count'])
                user = result
                bucketid = data['bucketid']
                result =dbm.getFiles2AnnotateByBucketId(bucketid,user,count)
                if result == []:
                    message = {"code": 0, "msg": "files empty", "data": ''}
                else:
                    message = {"code": 1, "msg": "success", "data": {"imgs": result}}
        except Exception as e:
            message = {"code": 0, "msg": str(e), "data": ""}
        if dbm:
            mylog.info('respon for /api/ownFile : %s' % str(message))
            del dbm
        self.write_to_client(message)

class UpLoadAnnotationHandler(BaseHandler):
    '''
    data :file_id,mask_info
    file的own置为0，标注置为已标注，
    filemask表中添加记录
    resp ()
    '''

    def get(self):
        message = {"code": 1, "msg": 'test for get model', "data": ""}
        self.write_to_client(message)

    def post(self):
        data = self.request.body
        self.process(data)

    def process(self, data):
        dbm = None
        try:
            dbm = MySQLHelper.DataBaseManager()
            mylog.info('param for /api/uploadAnnotation : %s' % str(data))
            data = json.loads(data)
            token = data['token']
            result = dbm.userValidateByToken(token)
            if result == '':
                message = {"code": 0, "msg": "toekn error", "data": ''}
            else:
                fileId = data['fileid']
                user = result
                fileTypeName = data['filetypename']
                annotation = data['annotation']
                result = dbm.uploadAnnotation(fileId,user,fileTypeName,annotation)
                print result
                if not result:
                    message = {"code": 0, "msg": "sql error", "data": ''}
                else:
                    message = {"code": 1, "msg": "success", "data": ''}
        except Exception as e:
            message = {"code": 0, "msg": str(e), "data": ""}
        if dbm:
            mylog.info('respon for /api/uploadAnnotation : %s' % str(message))
            del dbm
        self.write_to_client(message)

class UpDateAnnotationHandler(BaseHandler):
    '''
    data :file_id,mask_info
    file的own置为0，标注置为已标注，
    filemask表中添加记录
    resp ()
    '''

    def get(self):
        message = {"code": 1, "msg": 'test for get model', "data": ""}
        self.write_to_client(message)

    def post(self):
        data = self.request.body
        self.process(data)

    def process(self, data):
        dbm = None
        try:
            dbm = MySQLHelper.DataBaseManager()
            mylog.info('param for /api/updateAnnotation : %s' % str(data))
            data = json.loads(data)
            token = data['token']
            result = dbm.userValidateByToken(token)
            if result == '':
                message = {"code": 0, "msg": "toekn error", "data": ''}
            else:
                fileId = data['fileid']
                user = result
                fileTypeName = data['filetypename']
                annotation = data['annotation']
                result = dbm.updateAnnotation(fileId,user,fileTypeName,annotation)
                print result
                if not result:
                    message = {"code": 0, "msg": "sql error", "data": ''}
                else:
                    message = {"code": 1, "msg": "success", "data": ''}
        except Exception as e:
            message = {"code": 0, "msg": str(e), "data": ""}
        if dbm:
            mylog.info('respon for /api/updateAnnotation : %s' % str(message))
            del dbm
        self.write_to_client(message)



class ListUsersHandler(BaseHandler):
    '''
    data :
    返回所有用户的状态权限等
    resp (user_id,name,status,role_id)
    '''

    def get(self):
        message = {"status": 1, "message": 'test for get model', "data": ""}
        self.write_to_client(message)

    def post(self):
        data = self.request.body
        self.process(data)

    def process(self, data):
        dbm = None
        try:
            dbm = MySQLHelper.DataBaseManager()
            mylog.info('param for /api/listUsers : %s' % str(data))
            data = json.loads(data)
            token = data['token']
            result = dbm.userValidateByToken(token)
            if result == '':
                message = {"code": 0, "msg": "toekn error", "data": ''}
            else:
                result = dbm.listUsers()
                if result == []:
                    message = {"code": 0, "msg": "users empty", "data": ''}
                else:
                    message = {"code": 1, "msg": "success", "data": {"users":result}}
        except Exception as e:
            message = {"code": 0, "msg": str(e), "data": ""}
        if dbm:
            mylog.info('respon for /api/listUsers : %s' % str(message))
            del dbm
        self.write_to_client(message)


class EditUserHandler(BaseHandler):
    '''
    data : userid
    启用停用账户，user表中切换status，如果是禁用，需要清空own标记
    resp (user_id,name,status,role_id)
    '''

    def get(self):
        message = {"code": 1, "msg": 'test for get model', "data": ""}
        self.write_to_client(message)

    def post(self):
        data = self.request.body
        self.process(data)

    def process(self, data):
        dbm = None
        try:
            dbm = MySQLHelper.DataBaseManager()
            mylog.info('param for /api/editUser : %s' % str(data))
            data = json.loads(data)
            token = data['token']
            result = dbm.userValidateByToken(token)
            if result == '':
                message = {"code": 0, "msg": "toekn error", "data": ''}
            else:
                name = data['name']
                inuse = data['status']
                inuse = inuse=='inuse'
                result = dbm.editUser(name,inuse)
                if result:
                    userInfo = dbm.getUserInfoByName(name)
                    message = {"code": 1, "msg": "success", "data": userInfo}
                else:
                    message = {"code": 0, "msg": 'sql error', "data": ""}
        except Exception as e:
            message = {"code": 0, "msg": str(e), "data": ""}
        if dbm:
            mylog.info('respon for /api/editUser : %s' % str(message))
            del dbm
        self.write_to_client(message)


class DoubleBlindHandler(BaseHandler):
    '''
    data : userid
    启用停用账户，user表中切换status，如果是禁用，需要清空own标记
    resp (user_id,name,status,role_id)
    '''

    def get(self):
        message = {"code": 1, "msg": 'test for get model', "data": ""}
        self.write_to_client(message)

    def post(self):
        data = self.request.body
        self.process(data)

    def process(self, data):
        dbm = None
        try:
            dbm = MySQLHelper.DataBaseManager()
            mylog.info('param for /api/bucketDoubleBlind : %s' % str(data))
            data = json.loads(data)
            token = data['token']
            result = dbm.userValidateByToken(token)
            if result == '':
                message = {"code": 0, "msg": "toekn error", "data": ''}
            else:
                bucketid = data['bucketid']
                enable = data['status']
                enable = int(enable)
                result = dbm.doubleBlindBucket(bucketid,enable)
                if result:
                    status = dbm.getBucketDoubleBlindByBucketID(bucketid)
                    message = {"code": 1, "msg": "success", "data": {"status":status}}
                else:
                    message = {"code": 0, "msg": 'sql error', "data": ""}
        except Exception as e:
            message = {"code": 0, "msg": str(e), "data": ""}
        if dbm:
            mylog.info('respon for /api/bucketDoubleBlind : %s' % str(message))
            del dbm
        self.write_to_client(message)

class CreateUserHandler(BaseHandler):
    '''
    data : userid
    启用停用账户，user表中切换status，如果是禁用，需要清空own标记
    resp (user_id,name,status,role_id)
    '''

    def get(self):
        message = {"code": 1, "msg": 'test for get model', "data": ""}
        self.write_to_client(message)

    def post(self):
        data = self.request.body
        self.process(data)

    def process(self, data):
        dbm = None
        try:
            dbm = MySQLHelper.DataBaseManager()
            mylog.info('param for /api/createUser : %s' % str(data))
            data = json.loads(data)
            token = data['token']
            result = dbm.userValidateByToken(token)
            if result == '':
                message = {"code": 0, "msg": "toekn error", "data": ''}
            else:
                name = data['name']
                status = data['status']
                passwd = data['passwd']
                roletype = data['roletype']
                result = dbm.createUser(name,passwd,status,roletype)
                if result:
                    userInfo = dbm.getUserInfoByName(name)
                    message = {"code": 1, "msg": "success", "data": userInfo}
                else:
                    message = {"code": 0, "msg": 'sql error', "data": ""}
        except Exception as e:
            message = {"code": 0, "msg": str(e), "data": ""}
        if dbm:
            mylog.info('respon for /api/createUser : %s' % str(message))
            del dbm
        self.write_to_client(message)



class ClearOwnHandler(BaseHandler):
    '''
    data : user_id
    file表中 own项 清空用户的own
    resp ()
    '''

    def get(self):
        message = {"code": 1, "msg": 'test for get model', "data": ""}
        self.write_to_client(message)

    def post(self):
        data = self.request.body
        self.process(data)

    def process(self, data):
        dbm = None
        try:
            dbm = MySQLHelper.DataBaseManager()
            mylog.info('param for /api/clearOwnFile : %s' % str(data))
            data = json.loads(data)
            token = data['token']
            result = dbm.userValidateByToken(token)
            if result == '':
                message = {"code": 0, "msg": "toekn error", "data": ''}
            else:
                name = data['name']
                result = dbm.clearOwnFileByUser(name)
                if result:
                    message = {"code": 1, "msg": "success", "data": ''}
                else:
                    message = {"code": 0, "msg": 'sql error', "data": ""}
        except Exception as e:
            message = {"code": 0, "msg": str(e), "data": ""}
        if dbm:
            mylog.info('respon for /api/clearOwnFile : %s' % str(message))
            del dbm
        self.write_to_client(message)


# class UsersReportHandler(BaseHandler):
#     '''
#     data : user_id
#     所有用户标注数，
#     resp (user_id,count)
#     '''
#
#     def get(self):
#         message = {"status": 1, "message": 'test for get model', "data": ""}
#         self.write_to_client(message)
#
#     def post(self):
#         data = self.request.body
#         self.process(data)
#
#     def process(self, data):
#         try:
#             data = json.loads(data)
#             message = {"status": 0, "message": "success", "data": ''}
#         except Exception as e:
#             message = {"status": 1, "message": str(e), "data": ""}
#         self.write_to_client(message)

class BucketsReportHandler(BaseHandler):
    '''
    data : user_id
    所有buckets标注数，
    resp (bucket_id,count1，count2)
    '''

    def get(self):
        message = {"status": 1, "message": 'test for get model', "data": ""}
        self.write_to_client(message)

    def post(self):
        data = self.request.body
        self.process(data)

    def process(self, data):
        dbm =None
        try:
            dbm = MySQLHelper.DataBaseManager()
            mylog.info('param for /api/bucketsReport : %s' % str(data))
            data = json.loads(data)
            token = data['token']
            result = dbm.userValidateByToken(token)
            if result == '':
                message = {"code": 0, "msg": "toekn error", "data": ''}
            else:
                result = dbm.BucketReport()
                if result == []:
                    message = {"code": 0, "msg": "users empty", "data": ''}
                else:
                    message = {"code": 1, "msg": "success", "data": {"buckets": result}}
        except Exception as e:
            message = {"code": 0, "msg": str(e), "data": ""}
        if dbm:
            mylog.info('respon for /api/bucketsReport : %s' % str(message))
            del dbm
        self.write_to_client(message)

class ExportReportHandler(BaseHandler):
    '''
    data : user_id
    所有buckets标注数，
    resp (bucket_id,count1，count2)
    '''

    def get(self):
        message = {"status": 1, "message": 'test for get model', "data": ""}
        self.write_to_client(message)

    def post(self):
        data = self.request.body
        self.process(data)

    def process(self, data):
        dbm = None
        try:
            dbm = MySQLHelper.DataBaseManager()
            mylog.info('param for /api/exportReport : %s' % str(data))
            data = json.loads(data)
            token = data['token']
            result = dbm.userValidateByToken(token)
            if result == '':
                message = {"code": 0, "msg": "toekn error", "data": ''}
            else:
                result = dbm.writeTotalReport('totalReport.csv')
                if result == '':
                    message = {"code": 0, "msg": "users empty", "data": ''}
                else:
                    if "OCT_DOWNLOAD" in os.environ:
                        result = os.path.join(os.environ["OCT_DOWNLOAD"], result)
                    else:
                        result = result
                    message = {"code": 1, "msg": "success", "data": {"url": result}}
        except Exception as e:
            message = {"code": 0, "msg": str(e), "data": ""}
        if dbm:
            dbm = MySQLHelper.DataBaseManager()
            mylog.info('respon for /api/bucketsReport : %s' % str(message))
            del dbm
        self.write_to_client(message)


def web():
    tornado.options.parse_command_line()
    url = [(r'/api/createBucket', CreateBucketHandler),(r'/api/bucketDoubleBlind',DoubleBlindHandler),(r'/api/getBucketFile',GetBucketFileHandler),
           (r'/api/listBucket',ListBucketsHandler),(r'/api/uploadFileInfo',UploadFileInfoHandler),(r'/api/login',LogInHandler),
           (r'/api/ownFile',OwnFileHandler),(r'/api/uploadAnnotation',UpLoadAnnotationHandler),
           (r'/api/updateAnnotation',UpDateAnnotationHandler),(r'/api/userList',ListUsersHandler),(r'/api/editUser',EditUserHandler),
           (r'/api/clearUserOwn',ClearOwnHandler),(r'/api/bucketReport',BucketsReportHandler),
           (r'/api/createUser',CreateUserHandler),(r'/api/exportReport',ExportReportHandler),]

    application = tornado.web.Application(handlers=url)
    http_server = tornado.httpserver.HTTPServer(application)
    dbm = MySQLHelper.DataBaseManager()
    port = dbm.config.getPort()
    host = dbm.config.getHost()
    del dbm
    http_server.listen(port)

    print("server is running at {0}:{1}".format(host,port))
    tornado.ioloop.IOLoop.instance().start()


if __name__ == "__main__":
    web()