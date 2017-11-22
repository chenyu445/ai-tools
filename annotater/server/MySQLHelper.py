#coding:utf-8
import MySQLdb as mdb
import time
import base64
import datetime
import csv
import os
from Utils import conf,LogUtils
from qiniu import Auth
import hashlib

base_name = os.path.basename(__file__)
name = base_name.split('.')[0]
mylog = LogUtils.MyLog(os.path.join('/tmp', name) + '.log')
conf_name = 'config.conf'

def str_sha256(passwd):
    sha256 = hashlib.sha256()
    sha256.update(passwd)
    return sha256.hexdigest()

class DataBaseManager():

    def __init__(self,baseName=None,enableQiniu=False):
        root_path = os.path.dirname(__file__)
        conf_path = os.path.join(root_path, 'Utils')
        self.conf_path = os.path.join(conf_path, conf_name)
        self.config = conf.Config(self.conf_path)
        self.conn = None
        self.cur = None
        try:
            self.baseName, self.host, self.user, self.passwd, self.port = self.config.getDataBaseInfo()
            self.conn = mdb.connect(host=self.host, user=self.user, passwd=self.passwd,charset='utf8', port=int(self.port))
            self.cur = self.conn.cursor()
            if enableQiniu:
                self.accessKey = self.config.getQiNiuAccessKey()
                self.secretKey = self.config.getQiNiuSecretKey()
                if self.accessKey and self.secretKey:
                    self.q = Auth(self.accessKey,self.secretKey)
                    if self.q is None:
                        mylog.error('init qiuniu key error')
                        raise AssertionError('init qiuniu key error')
                    else:
                        mylog.info('init qiniu key success')
                else:
                    mylog.error('init qiuniu key error')
                    raise AssertionError('init qiuniu key error')
            if baseName is None:
                self.conn.select_db(self.baseName)
            self.init  = True
            mylog.info('success init dataBasemManager')
        except Exception as e:
            self.init = False
            mylog.warning('fail to init dataBasemManager : %s'%str(e))

    def __del__(self):
        if self.cur:
            self.cur.close()
        if self.conn:
            self.conn.close()
        mylog.info('dataBaseManager id : %s'%str(id(self)))
        mylog.info('del dataBaseManager')

    def createDb(self):
        bucketsSql = 'create table if not exists buckets(id int PRIMARY KEY auto_increment,' \
                     'name varchar(32) not NULL unique key,' \
                     'bucketkey varchar(32) not NULL,' \
                     'qiniukey varchar(200) not NULL,' \
                     'domain varchar(200) not null,' \
                     'doubleblind int not null,' \
                     'status varchar(32) not null,' \
                     'createtime  datetime DEFAULT CURRENT_TIMESTAMP, ' \
                     'updatetime datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP) character set = utf8'

        bucketTypesSql = 'create table if not exists buckettypes(bucketid int not null,' \
                         'filetypeid int not NULL,' \
                         'createtime  datetime DEFAULT CURRENT_TIMESTAMP, ' \
                         'updatetime datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP) character set = utf8'

        fileTypesSql = 'create table if not exists filetypes(filetypeid int PRIMARY KEY auto_increment,' \
                       'filetypename varchar(32) not NULL unique key,' \
                       'createtime  datetime DEFAULT CURRENT_TIMESTAMP, ' \
                       'updatetime datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP) character set = utf8'

        fileSql = 'create table if not exists imgs(id int PRIMARY KEY auto_increment,' \
                  'qiniukey varchar(200) not NULL unique key,' \
                  'encryptkey varchar(200) not null,' \
                  'status varchar(200),' \
                  'bucketid int not null,' \
                  'owner varchar(32),' \
                  'owntime datetime,' \
                  'createtime  datetime DEFAULT CURRENT_TIMESTAMP, ' \
                  'updatetime datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,' \
                  'annotatedtimes int not null,' \
                  'picture_length int not null) character set = utf8'

        ownFileSql = 'create table if not exists ownfiles(id int PRIMARY KEY auto_increment,' \
                     'bucketid int not null,' \
                      'fileid int not NULL,' \
                      'owner varchar(200) not null,' \
                      'createtime  datetime DEFAULT CURRENT_TIMESTAMP, ' \
                      'updatetime datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP) character set = utf8'

        annotateSql = 'create table if not exists annotations(id int PRIMARY KEY auto_increment,' \
                      'fileid int not NULL,' \
                      'bucketid int not null,' \
                      'annotater varchar(32) not null,' \
                      'filetypename varchar(32) not null,' \
                      'annotation varchar(200),' \
                      'createtime  datetime DEFAULT CURRENT_TIMESTAMP, ' \
                      'updatetime datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP) character set = utf8'

        usersSql = 'create table if not exists users(id int PRIMARY KEY auto_increment,' \
                   'name varchar(32) not NULL unique key,' \
                   'passwd varchar(200) not null,' \
                   'status varchar(32) not null,' \
                   'roletype varchar(32), ' \
                   'token varchar(200),' \
                   'createtime  datetime DEFAULT CURRENT_TIMESTAMP, ' \
                   'updatetime datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP) character set = utf8'
        if self.init:
            try:
                self.cur.execute('create database if not exists %s' % self.baseName)
                self.conn.select_db(self.baseName)
                self.cur.execute(bucketsSql)
                self.cur.execute(ownFileSql)
                self.cur.execute(bucketTypesSql)
                self.cur.execute(fileTypesSql)
                self.cur.execute(fileSql)
                self.cur.execute(annotateSql)
                self.cur.execute(usersSql)
                self.conn.commit()
                mylog.info('success creating database %s'%self.baseName)
            except Exception as e:
                mylog.error('fail to create database %s   : %s' % (self.baseName,str(e)))


    def insertBucket(self,name,bucketKey,qiniuKey,domain,fileTypes,status='type',doubleBlind=1):
        flag = None
        try:
            bucketValue = [name,bucketKey,qiniuKey,domain,doubleBlind,status]
            insertBucketSql = 'insert buckets(name,bucketkey,qiniukey,domain,doubleblind,status) values(%s,%s,%s,%s,%s,%s)'
            self.cur.execute(insertBucketSql,bucketValue)
            self.conn.commit()
            bucketId = self.getBucketIdByName(name)
            insertBucketTypesSql = 'insert buckettypes(bucketid,filetypeid) values(%s,%s)'
            insertFileTypeSql = 'insert filetypes(filetypename) values(%s)'
            self.cur.executemany(insertFileTypeSql,[[f] for f in fileTypes])
            self.conn.commit()
            for fileTypeName in fileTypes:
                fileTypeId = self.getFileTypeIdByName(fileTypeName)
                self.cur.execute(insertBucketTypesSql,[bucketId,fileTypeId])
            self.conn.commit()
            mylog.info('success insert bucket %s'%name)
        except Exception as e:
            flag = str(e)+' in function insertBucket'
            mylog.error('error insert bucket %s   :%s'%(name,str(e)))
        return flag


    def getBucketIdByName(self,name):
        result = None
        try:
            getBucketIdSql = 'select * from buckets where name=%s'
            self.cur.execute(getBucketIdSql,[name])
            results = self.cur.fetchall()
            if len(results) == 1:
                result = results[0][0]
                mylog.info('success getting bucket id:%s by name : %s' %(result,name))
        except Exception as e:
            mylog.error('error get bucket id by name : %s'%str(e))
        return result

    def getFileTypeIdByName(self,name):
        result = None
        try:
            getFileTypeIdSql = 'select * from filetypes where filetypename=%s'
            self.cur.execute(getFileTypeIdSql,[name])
            results = self.cur.fetchall()
            if len(results) == 1:
                result =  results[0][0]
                mylog.info('success getting type id:%s by name : %s' % (result, name))
        except Exception as e:
            mylog.error('error get type id by name : %s' % str(e))
        return  result




    def insertFile(self,qiniuKey,encryptKey,bucketName,pictureLength):
        result = None
        try:
            bucketId = self.getBucketIdByName(bucketName)
            imgValue = [qiniuKey, encryptKey, bucketId,pictureLength]
            insertfileSql = 'insert into imgs(qiniukey,encryptkey,bucketid,annotatedtimes,picture_length) values(%s,%s,%s,0,%s)'
            self.cur.execute(insertfileSql, imgValue)
            self.conn.commit()
            mylog.info('success insert file :%s' %qiniuKey)
        except Exception as e:
            mylog.error('error insert file  : %s' % str(e))
            result = str(e)
        return result

    def delFile(self,qiniuKey):
        result = None
        try:
            delfileSql = 'delete from  imgs where qiniukey=%s'
            self.cur.execute(delfileSql, [qiniuKey])
            self.conn.commit()
            mylog.info('success delete file :%s' % qiniuKey)
        except Exception as e:
            result = str(e)
            mylog.error('fail to delete file : %s' % str(e))
        return result

    def getBucketFileByName(self,bucketName):
        result = ''
        try:
            getBucketTypesSql = 'select * from buckets where name=%s'
            self.cur.execute(getBucketTypesSql, [bucketName])
            results = self.cur.fetchall()
            if len(results) == 1:
                qiniuKey = results[0][3]
                domain = results[0][4]
                base_url = 'http://%s/%s' % (domain, qiniuKey)
                result = self.q.private_download_url(base_url, expires=3600)
                mylog.info('success get bucket file ')
            else:
                mylog.error('fail to find file in sql ')

        except Exception as e:
            mylog.error('fail to find file in sql %s:'%str(e))
        return result

    def getBucketTypesById(self,id):
        result = []
        try:
            getBucketTypesSql = 'select * from buckettypes where bucketid=%s'
            self.cur.execute(getBucketTypesSql,[id])
            results = self.cur.fetchall()
            for fileTypeId in results:
                getFiletTypesSql = 'select * from filetypes where filetypeid=%s'
                self.cur.execute(getFiletTypesSql, [fileTypeId[1]])
                types = self.cur.fetchall()
                result.append(types[0][1])
            mylog.info('success get bucket types')
        except Exception as e:
            mylog.error('fail to get bucket types :%s' % str(e))
        return  result

    def listBucket(self):
        buckets = []
        try:
            listBucketSql = 'select * from buckets'
            self.cur.execute(listBucketSql)
            results = self.cur.fetchall()
            for result in results:
                bucketId = result[0]
                buckettypes = self.getBucketTypesById(bucketId)
                nums = self.getFilesNumById(bucketId)
                result = {'id': result[0], 'name': result[1], 'domain': result[4], 'annotatedfilenum': nums[0],
                          'totalfilenum': nums[1], "status":result[6],'buckettypes': buckettypes}
                buckets.append(result)
            mylog.info('success get buckets list')
        except Exception as e:
            mylog.error('fail to get buckets list :%s' % str(e))
        return buckets

    def BucketReport(self):
        buckets = []
        try:
            listBucketSql = 'select * from buckets'
            self.cur.execute(listBucketSql)
            results = self.cur.fetchall()
            for result in results:
                bucketId = result[0]
                buckettypes = self.getBucketTypesById(bucketId)
                annotatedFileNum, totalFileNum,_= self.getFilesNumById(bucketId)
                if result[5] is None:
                    doubleblind ='disable'
                else:
                    doubleblind = result[5]
                result = {'id':result[0],'name': result[1], 'domain': result[4],'doubleblind':doubleblind, 'annotatedfilenum': annotatedFileNum,
                          'totalfilenum': totalFileNum, 'buckettypes': buckettypes}
                buckets.append(result)
            mylog.info('success get buckets info')
        except Exception as e:
            mylog.error('fail to get buckets info :%s' % str(e))
        return buckets

    def getFilesNumById(self,bucketId):
        result = [-1,-1,-1]
        try:
            doubleBlind = self.getDoubleBlindByBucketId(bucketId)
            getFileSql = 'select * from imgs where annotatedtimes>%s and bucketid=%s'
            self.cur.execute(getFileSql,[doubleBlind-1,bucketId])
            results = self.cur.fetchall()
            result[2] = len(results)
            getFileSql = 'select annotatedtimes from imgs where bucketid=%s'
            self.cur.execute(getFileSql,[bucketId])
            results = self.cur.fetchall()
            temp = 0
            for img in results:
                temp += img[0]
            result[1] = len(results)*doubleBlind
            result[0] = temp
            mylog.info('success get files num by bucketid')
        except Exception as e:
            mylog.error('fail to get file num by bucketid %s  : %s' % (bucketId,str(e)))
        return result


    def getBucketDoubleBlindByBucketID(self,bucketId):
        result = None
        try:
            getFileSql = 'select doubleblind from buckets where id=%s'
            self.cur.execute(getFileSql, [bucketId])
            results = self.cur.fetchall()
            result = results[0][0]
            mylog.info('success get bucket doubleblind')
        except Exception as e:
            mylog.error('fail to get bucket doubleblind  : %s' % str(e))
        return result

    def getBucketStatusByBucketID(self,bucketId):
        result = None
        try:
            getFileSql = 'select status from buckets where id=%s'
            self.cur.execute(getFileSql, [bucketId])
            results = self.cur.fetchall()
            result = results[0][0]
            mylog.info('success get bucket doubleblind')
        except Exception as e:
            mylog.error('fail to get bucket doubleblind  : %s' % str(e))
        return result


    def getFileByFileId(self,fileId):
        result = []
        try:
            getFileSql = 'select * from imgs where id=%s'
            self.cur.execute(getFileSql, [fileId])
            results = self.cur.fetchall()
            result = results[0]
            result = [value for value in result]
            mylog.info('success get file  by fileid :%s'%fileId)
        except Exception as e:
            mylog.error('fail to get file by fileid %s  : %s' % (fileId,str(e)))
        return result

    # def getFileByBucketId(self,bucketId):
    #     result = []
    #     try:
    #         getOwnFileSql = 'select * from imgs where bucketid=%s'
    #         self.cur.execute(getOwnFileSql, [bucketId])
    #         results = self.cur.fetchall()
    #         for ownFile in results:
    #             result.append([value for value in ownFile])
    #         mylog.info('success get files by bucketid')
    #     except Exception as e:
    #         mylog.error('fail to get file by bucketid :%s' % str(e))
    #     return result

    # def getAnnotatedFileByBucketId(self,bucketId):
    #     result = []
    #     try:
    #         doubleBlind = self.getDoubleBlindByBucketId(bucketId)
    #         getOwnFileSql = 'select * from imgs where bucketid=%s and annotatedtimes>%s'
    #         self.cur.execute(getOwnFileSql, [bucketId,doubleBlind-1])
    #         results = self.cur.fetchall()
    #         for ownFile in results:
    #             result.append([value for value in ownFile])
    #         mylog.info('success get annotated file  by  bucketid :%s' % bucketId)
    #     except Exception as e:
    #         mylog.error('fail to get annotated file by bucketid  %s  : %s' % (bucketId, str(e)))
    #     return result

    def getOwnFileUrl(self,fileId, bucketId):
        result = ''
        try:
            getBucketTypesSql = 'select * from buckets where id=%s'
            self.cur.execute(getBucketTypesSql, [bucketId])
            results = self.cur.fetchall()
            if len(results) == 1:
                domain = results[0][4]
                getFileQiniuKeySql = 'select * from imgs where id=%s'
                self.cur.execute(getFileQiniuKeySql, [fileId])
                results = self.cur.fetchall()
                if len(results) == 1:
                    qiniuKey = results[0][1]
                    base_url = 'http://%s/%s' % (domain, qiniuKey)
                    result = self.q.private_download_url(base_url, expires=3600)
                    mylog.info('success get file url')
            else:
                mylog.error('can not find file in sql')

        except Exception as e:
            mylog.error('can not find file in sql : %s'%str(e))
        return result

    def getDoubleBlindByBucketId(self,bucketId):
        result = -1
        try:
            getDoubleBlindSql = 'select doubleblind from buckets where id=%s'
            self.cur.execute(getDoubleBlindSql,[bucketId])
            results = self.cur.fetchall()
            if len(results) == 1:
                result = results[0][0]
                mylog.info('success get double-blind status  by  bucket id ')
        except Exception as e:
            mylog.error('fail to get double-blind status  by  bucket id ')
        return  result


    def getFiles2AnnotateByBucketId(self,bucketId,user,count=2):

        result = []
        try:



            getOwnFileSql = 'select * from ownfiles where owner=%s and bucketid=%s'
            self.cur.execute(getOwnFileSql, [user,bucketId])
            results = self.cur.fetchall()
            for ownFile in results:
                download_url = self.getOwnFileUrl(ownFile[2], bucketId)
                ownFileInfo = self.getFileByFileId(ownFile[2])
                img = {'fileid': ownFileInfo[0], 'qiniukey': ownFileInfo[1], 'encryptkey': ownFileInfo[2], 'bucketid': bucketId,
                       'url': download_url,'pictureLength':ownFileInfo[10]}
                result.append(img)
            mylog.info('success get %d owned files by user from table ownfiles :%s' %(len(result), user))
        except Exception as e:
            mylog.error('fail to get owned file by user  %s  : %s' % (user, str(e)))
        temp = len(result)
        if len(result) < count:
            try:
                doubleBlind = self.getDoubleBlindByBucketId(bucketId)
                getOwnFileSql = 'select * from imgs left join annotations on imgs.id=annotations.fileid ' \
                                'where (annotater is null or annotater<>%s ) and annotatedtimes<%s limit 50 '
                self.cur.execute(getOwnFileSql, [user,doubleBlind])
                results = self.cur.fetchall()
                for ownFile in results:
                    flag = False
                    for img in result:
                        if img['fileid'] == ownFile[0]:
                            flag = True
                    if flag:
                        continue
                    # getAnnotatedFileSql = 'select * from annotations where fileid=%s and annotater=%s'
                    # self.cur.execute(getAnnotatedFileSql,[ownFile[0],user])
                    # annotations = self.cur.fetchall()
                    # if len(annotations) > 0:
                    #     continue
                    getOwnFileSql = 'select owner from ownfiles where fileid=%s and bucketid=%s'
                    self.cur.execute(getOwnFileSql, [ownFile[0],bucketId])
                    ownusers = self.cur.fetchall()
                    flag = False
                    for ownuser in ownusers:
                        if ownuser[0] == user:
                            flag = True
                    if flag:
                        continue
                    if len(ownusers) + ownFile[9] < doubleBlind:
                        download_url = self.getOwnFileUrl(ownFile[0], bucketId)
                        img = {'fileid': ownFile[0], 'qiniukey': ownFile[1], 'encryptkey': ownFile[2], 'bucketid': bucketId,
                               'url': download_url,"pictureLength":ownFile[10]}
                        result.append(img)
                        insertOnwSql = 'insert into ownfiles(fileid,bucketid,owner) values(%s,%s,%s)'
                        self.cur.execute(insertOnwSql,[ownFile[0],bucketId,user])
                        self.conn.commit()
                    if len(result) > count-1:
                        break
                mylog.info('success own %d files  by  bucketid :%s' % (len(result)-temp, bucketId))
            except Exception as e:
                mylog.error('fail to own file by bucketid  %s  : %s' % (bucketId, str(e)))
        return result




        # try:
        #     getOwnFileSql = 'select * from imgs where owner=%s and bucketid=%s'
        #     self.cur.execute(getOwnFileSql, [user,bucketId])
        #     results = self.cur.fetchall()
        #     for ownFile in results:
        #         download_url = self.getOwnFileUrl(ownFile[0], bucketId)
        #         img = {'fileid': ownFile[0], 'qiniukey': ownFile[1], 'encryptkey': ownFile[2], 'bucketid': bucketId,
        #                'url': download_url}
        #         result.append(img)
        #     mylog.info('success get %d owned files  by  user :%s' %(len(result), user))
        # except Exception as e:
        #     mylog.error('fail to get owned file by user  %s  : %s' % (user, str(e)))
        # temp = len(result)
        # if len(result) < count:
        #     try:
        #         getOwnFileSql = 'select * from imgs where bucketid=%s and status is null and owner is null'
        #         self.cur.execute(getOwnFileSql, [bucketId])
        #         results = self.cur.fetchall()
        #         for ownFile in results:
        #             download_url = self.getOwnFileUrl(ownFile[0], bucketId)
        #             img = {'fileid': ownFile[0], 'qiniukey': ownFile[1], 'encryptkey': ownFile[2], 'bucketid': bucketId,
        #                    'url': download_url}
        #             result.append(img)
        #             updateOnwSql = 'update imgs set owner=%s where id=%s'
        #             self.cur.execute(updateOnwSql,[user,ownFile[0]])
        #             updateOnwSql = 'update imgs set owntime=now() where id=%s'
        #             self.cur.execute(updateOnwSql, [ownFile[0]])
        #             self.conn.commit()
        #             if len(result) ==count:
        #                 break
        #         mylog.info('success own %d files  by  bucketid :%s' % (len(result)-temp, bucketId))
        #     except Exception as e:
        #         mylog.error('fail to own file by bucketid  %s  : %s' % (bucketId, str(e)))
        # return result

    def uploadAnnotation(self,fileId,user,fileTypeName,annotation):

        result = True
        try:
            deleteOwnfileSql = 'delete from  ownfiles where fileid=%s and owner=%s'
            self.cur.execute(deleteOwnfileSql,[fileId,user])
            updateFileSql = 'update imgs set annotatedtimes=annotatedtimes+1 where id=%s'
            self.cur.execute(updateFileSql, [fileId])
            self.conn.commit()
            self.cur.execute('select * from imgs where id=%s',[fileId])
            files = self.cur.fetchall()
            if len(files) == 1:
                checkSql = 'select * from annotations where fileid=%s and annotater=%s'
                self.cur.execute(checkSql, [fileId, user])
                checkresults = self.cur.fetchall()
                if len(checkresults) > 0:
                    raise  AssertionError('this file is annotated by user %s,please use update instead'%user)
                updateAnnotationSql = 'insert into annotations(fileid,bucketid,annotater,filetypename,annotation) values(%s,%s,%s,%s,%s)'
                self.cur.execute(updateAnnotationSql,[fileId,files[0][4],user,fileTypeName,annotation])
                self.conn.commit()
            mylog.info('success upload annotation fileid :%s' %fileId)
        except Exception as e:
            mylog.error('fail to upload annotation fileid %s  : %s' % (fileId, str(e)))
            result = False
        return result

    def updateAnnotation(self,fileId,user,fileTypeName,annotation):

        result = True
        try:
            self.cur.execute('select * from annotations where fileid=%s and annotater=%s',[fileId,user])
            files = self.cur.fetchall()
            if len(files) == 1:
                updateAnnotationSql = 'update annotations set filetypename=%s where id=%s'
                self.cur.execute(updateAnnotationSql,[fileTypeName,files[0][0]])
                updateAnnotationSql = 'update annotations set annotation=%s where id=%s'
                self.cur.execute(updateAnnotationSql, [annotation, files[0][0]])
                updateAnnotationSql = 'update annotations set updatetime=now() where id=%s'
                self.cur.execute(updateAnnotationSql, [files[0][0]])
                self.conn.commit()
                mylog.info('success update annotation files  %s  by user %s' % (fileId, user))
            else:
                result = False
                mylog.error('can not find annotated file by fileid %s and annotater %s'%(fileId, user))
        except Exception as e:
            mylog.error('fail update annotated file by fileid %s and annotater  %s  : %s' % (fileId,user, str(e)))
            result = False
        return result


    def getOwnFilesByName(self,user):
        result = []
        try:
            getOwnFileSql = 'select * from ownfiles where owner=%s'
            self.cur.execute(getOwnFileSql, [user])
            results = self.cur.fetchall()
            for ownFile in results:
                result.append([value for value in ownFile])
            mylog.info('success get owned file by user :%s' % user)
        except Exception as e:
            mylog.error('fail to get owned file by user %s  : %s' % (user, str(e)))
        return result

    def clearOwnFileByUser(self,user):
        result = True
        try:
            deleteOwnfileSql = 'delete from  ownfiles where owner=%s'
            self.cur.execute(deleteOwnfileSql, [user])
            self.conn.commit()
            mylog.info('success clearing owned file by user :%s' % user)
        except Exception as e:
            mylog.error('fail to clear owned file by user %s  : %s' % (user, str(e)))
            result =False
        return result

    def getToken(self,name,passwd):
        head = time.strftime('%Y-%m-%d-%H-%M-%S')
        sstr = '{"name":%s,"passwd":%s,"time":%s}'%(name,passwd,head)
        return base64.b64encode(sstr)

    def createUser(self,user,passwd,status,roleType):
        result = True
        try:
            passwd = str_sha256(passwd)
            passwd = str_sha256(passwd)
            createUserSql = 'insert into users(name,passwd,status,roletype) values(%s,%s,%s,%s)'
            self.cur.execute(createUserSql, [user, passwd,status,roleType])
            self.conn.commit()
            mylog.info('success creating  user :%s' % user)
        except Exception as e:
            mylog.error('fail to create user %s  : %s' % (user, str(e)))
            result = False
        return result

    def userValidate(self,user,passwd):
        result=''
        try:
            userValidateSql = 'select * from users where name=%s and passwd=%s'
            passwd = str_sha256(passwd)
            self.cur.execute(userValidateSql, [user,passwd])
            results = self.cur.fetchall()
            if len(results)==1:
                result = self.getToken(user,passwd)
                updateTokenSql = 'update users set token=%s  where name=%s'
                self.cur.execute(updateTokenSql,[result,user])
                updateTokenSql = 'update users set updatetime=now()  where name=%s'
                self.cur.execute(updateTokenSql, [user])
                self.conn.commit()
                mylog.info('success validate user and passwd')
            else:
                mylog.error('fail to find user and passwd in sql')
        except Exception as e:
            mylog.error('fail to validate user and passwd %s' % str(e))
        return result

    def userValidateByToken(self,token):

        #通过token查询数据库，返回唯一值则判断时间是否超时
        result = ''
        try:
            userValidateSql = 'select * from users where token=%s'
            self.cur.execute(userValidateSql, [token])
            results = self.cur.fetchall()
            if len(results) == 1:
                tokenTime = results[0][7]
                delta = datetime.timedelta(minutes=60*24)
                serverTime = datetime.datetime.now()
                if tokenTime > serverTime-delta:
                    status = results[0][3]
                    if status=='inuse':
                        result = results[0][1]
                        mylog.info('success validating user %s by token' %results[0][1])
                    else:
                        mylog.error('user not inuse at the moment' % results[0][1])
                else:
                    mylog.error('token out of time')
            else:
                mylog.error('can not find token in sql')
        except Exception as e:
            mylog.error('fail to  validate  by token')
        return result

    def getUserInfoByName(self,user):
        result = ''
        try:
            userValidateSql = 'select * from users where name=%s'
            self.cur.execute(userValidateSql, [user])
            results = self.cur.fetchall()
            if len(results) == 1:
                filesNum = self.getFilesNumByUser(results[0][1])
                result = {"id":results[0][0],'name':results[0][1],'status':results[0][3],'roletype':results[0][4],'filesnum':filesNum}
                mylog.info('success get userInfo by name %s ' % user)
            else:
                mylog.error('can not find user %s in sql' % user)

        except Exception as e:
            mylog.error('fail to find user %s  : %s' % (user,str(e)))
        return result

    def editUser(self,user,inuse):
        result = False
        try:
            if not inuse:
                userValidateSql = 'select * from users where name=%s and status="inuse"'
                self.cur.execute(userValidateSql, [user])
                results = self.cur.fetchall()
                if len(results) == 1:
                    userValidateSql = 'update users set status="notuse" where name=%s'
                    self.cur.execute(userValidateSql, [user])
                    self.clearOwnFileByUser(user)
                    self.conn.commit()
                    result = True
                    mylog.info('success turn user %s to notuse' %user)
            else:
                userValidateSql = 'select * from users where name=%s and status="notuse"'
                self.cur.execute(userValidateSql, [user])
                results = self.cur.fetchall()
                if len(results) == 1:
                    userValidateSql = 'update users set status="inuse" where name=%s'
                    self.cur.execute(userValidateSql, [user])
                    self.conn.commit()
                    result = True
                    mylog.info('success turn user %s to inuse' % user)

        except Exception as e:
            mylog.error('fail to turn user %s status' % user)
        return result

    def doubleBlindBucket(self,bucketid,doubleBlind):
        result = False
        try:
            updateDoubleBlindSql = 'update buckets set doubleblind=%s where id=%s'
            self.cur.execute(updateDoubleBlindSql, [doubleBlind,bucketid])
            self.conn.commit()
            result = True
            mylog.info('success turn bucketid %s to double blind %s' %(bucketid,doubleBlind))
        except Exception as e:
            mylog.error('fail to turn bucketid %s to double blind %s : %s' %(bucketid,doubleBlind,str(e)))
        return result

    def getFilesNumByUser(self,user):
        result = -1
        try:
            userValidateSql = 'select * from annotations where annotater=%s'
            self.cur.execute(userValidateSql, [user])
            results = self.cur.fetchall()
            result = len(results)
            mylog.info('success get user %s  annotate %s files' % (user,result))
        except Exception as e:
            mylog.error('fail to get user %s  annotated files  %s' % (user, str(e)))
        return result

    def getBucketFileNumByUser(self,user):
        result = []
        try:
            listbucketSql = 'select * from buckets'
            self.cur.execute(listbucketSql)
            buckets = self.cur.fetchall()
            for bucket in buckets:
                bucketId = bucket[0]
                bucketName = bucket[1]
                userValidateSql = 'select * from annotations where annotater=%s and bucketid=%s'
                self.cur.execute(userValidateSql, [user,bucketId])
                results = self.cur.fetchall()
                fileNum = len(results)
                result.append({bucketName:fileNum})
            mylog.info('success get bucket files num bu user')
        except Exception as e:
            mylog.info('fail to  get bucket files num bu user : %s'%str(e))
        return result

    def writeTotalReport(self,outPath):
        #=======================
        result = []
        try:
            myFile = open(outPath, 'wb')
            myWriter = csv.writer(myFile)
            row = ['fileName', 'bucketName', 'annotater', 'type', 'desc','result']
            myWriter.writerow(row)
            listimgsSql = 'select * from imgs'
            self.cur.execute(listimgsSql)
            imgs = self.cur.fetchall()
            for img in imgs:
                imgId = img[0]
                annotationSql = 'select * from annotations where fileid=%s'
                self.cur.execute(annotationSql, [imgId])
                results = self.cur.fetchall()
                annoNum = len(results)
                if annoNum > 0 :
                    pass

        except Exception as e:
            print e
        return result

    def getBucketFileNumByBucketName(self,bucketName):
        result = []
        try:
            bucketId = self.getBucketIdByName(bucketName)
            listusersSql = 'select * from users'
            self.cur.execute(listusersSql)
            users = self.cur.fetchall()
            for user in users:
                userName = user[1]
                userValidateSql = 'select * from annotations where annotater=%s and bucketid=%s'
                self.cur.execute(userValidateSql, [userName,bucketId])
                results = self.cur.fetchall()
                fileNum = len(results)
                result.append({'name':userName,'filenum':fileNum})
            mylog.info('success get bucket files num by user')

        except Exception as e:
            mylog.info('fail to get bucket files num by user :%s'%str(e))
        return result

    def listUsers(self):
        result =[]
        try:
            userValidateSql = 'select * from users'
            self.cur.execute(userValidateSql)
            results = self.cur.fetchall()
            for user in results:
                filesNum = self.getFilesNumByUser(user[1])
                user = {'id':user[0],'name':user[1],'status':user[3],'roletype':user[4],'taskNum':filesNum}
                result.append(user)
            mylog.info('success list users info')

        except Exception as e:
            mylog.info('faio to get users info : %s'%str(e))
        return result

    def testInsertBucket(self):
        name = 'bucket'
        bucketKey = 'bucketKey'
        privateKey = 'privatekey'
        domain = 'www.baidu.com'
        filetypes = ['type1','type2','type3','type4']
        self.insertBucket(name,bucketKey,privateKey,domain,filetypes)

if __name__ == '__main__':



    dbm = DataBaseManager('velosiis')
    dbm.createDb()
    #dbm.testInsertBucket()
    dbm.createUser('eyesoct', '20170930', 'inuse', 'annotater')
    dbm.createUser('WHU', '20170930', 'inuse', 'annotater')
    dbm.createUser('renminyiyuan', '20170930', 'inuse', 'annotater')
    dbm.createUser('velosiis', '20170930', 'inuse', 'webadmin')
    del dbm

    # createDb(BASENAME)
    # #print getBucketIdByName('aaa')
    # testInsertBucket()
    # # print listBucket()
    # # insertFile('qiniuKey','encryptKey','bucket')
    # # img = getFileByFileId(1)
    # # print img[3]
    # # print type(img[3])
    # # print img
    # #print getFiles2AnnotateByBucketId(1,'haha',2)
    # # updateAnnotation(1,'haha','type1','dadada')
    # # print listBucket()
    # #clearOwnFileByUser('haha')
    #
    #
    # token = 'eyJuYW1lIjp4aWFvd2FuZywicGFzc3dkIjp3YW5nMnBhc3MsInRpbWUiOjIwMTctMDktMDctMTctNTUtMzN9'
    #print userValidate('xiaowang','wang2pass')
    #print userValidateByToken(token)
    #print getUserInfoByName('xiaowang')