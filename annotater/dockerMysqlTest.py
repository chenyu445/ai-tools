#coding:utf:8

import MySQLdb as mdb

baseName = 'test'
conn = mdb.connect('10.10.59.251', 'root', '12345678',charset='utf8', port=3300)
cur = conn.cursor()
cur.execute('create database if not exists %s' % baseName)
conn.select_db(baseName)
testSql = 'create table if not exists tttt(id int PRIMARY KEY auto_increment,' \
                     'name varchar(32) not NULL unique key,' \
                     'ddd varchar(32) not null) character set = utf8'
cur.execute(testSql)
conn.select_db('test')

# filetypes = ['正常', '神经上皮脱离', '色素上皮层脱离', '视网膜前膜', '其他']
# insertSql = 'insert into tttt(name,ddd) values(%s,%s)'
# cur.execute(insertSql,[filetypes[0],filetypes[1]])
# #cur.execute(insertSql,[filetypes[0],filetypes[1]])
# conn.commit()
# cur.close()
# conn.close()
cur.execute('select * from tttt')
results = cur.fetchall()
print results[0][1]