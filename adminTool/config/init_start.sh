
export NODE_ENV='production'
export PORT=10001
export API_URL='http://127.0.0.1:8004'

isDaemon=$1

if [ "$isDaemon" = "-d" ]; then
  echo "it will run as a daemon in the backgroud..."
  npm run start  > log.txt & 
else
  npm run start
fi 
