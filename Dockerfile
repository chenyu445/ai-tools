FROM ai-tools-server:1.0

ADD annotater/server /opt/server

ADD adminTool /opt/adminTool

ADD adminTool/tools/phantomjs-2.1.1-linux-x86_64.tar.bz2 /tmp/phantomjs-2.1.1-linux-x86_64.tar.bz2

WORKDIR /opt/adminTool

RUN cnpm install

EXPOSE 10001
EXPOSE 8004

WORKDIR /opt
ADD start.sh /opt/

ENTRYPOINT ["sh","start.sh"]

