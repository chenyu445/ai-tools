import base64
import time
import datetime


def getToken(name,passwd):
    head = time.strftime('%Y-%m-%d-%H')
    sstr = '{"name":%s,"passwd":%s,"time":%s}'%(name,passwd,head)
    return base64.b64encode(sstr)

if __name__ == '__main__':
    print getToken('dwadafa','dwadadad')
    print time.strftime('%Y-%m-%d-%H-%M-%S')

    a=(16 - 20)%24
    print a
    a = datetime.datetime.now()
    b = datetime.timedelta(minutes=300)
    c =a-b
    print a
    print b
    print a-b
    print c < a
    d = (a,b,c)
    print type(d)
    print len(d)

