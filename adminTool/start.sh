isDaemon="-d"
if [ "$1" != "-d" ]; then 
  isDaemon=""
fi
./config/init_start.sh $isDaemon
