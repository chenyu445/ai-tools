import MySQLdb as mdb
BASENAME = 'velosiis'

conn = mdb.connect(host='localhost', user='root', passwd='12345678', port=3306)
cur = conn.cursor()
conn.select_db(BASENAME)
user = 'xiaowang'
bucketId = 2
getOwnFileSql = 'select * from imgs where owner=%s and bucketid=%s'
cur.execute(getOwnFileSql, [user,bucketId])
results = cur.fetchall()
print len(results)
result = []
count = 2
getOwnFileSql = 'select * from imgs where bucketid=%s and status is null and owner is null'
cur.execute(getOwnFileSql, [bucketId])
results = cur.fetchall()
for ownFile in results:
    img = {'fileid':ownFile[0],'qiniukey': ownFile[1], 'encryptkey': ownFile[2], 'bucketid': bucketId}
    result.append(img)
    updateOnwSql = 'update imgs set owner=%s where id=%s'
    cur.execute(updateOnwSql,[user,ownFile[0]])
    updateOnwSql = 'update imgs set owntime=now() where id=%s'
    cur.execute(updateOnwSql, [ownFile[0]])
    conn.commit()
    if len(result) ==count:
        break
conn.commit()
cur.close()
conn.close()