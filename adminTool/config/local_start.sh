
#export NODE_ENV='production'
export PORT=10001
export API_URL='http://10.10.59.251:8003'

isDaemon=$1

if [ "$isDaemon" = "-d" ]; then
  echo "it will run as a daemon in the backgroud..."
  npm run start  > log.txt & 
else
  npm run start
fi 
