echo "starting http server..."
cd /opt/server/
sh start.sh -d
#python HttpServerHelper.py > log.txt & 
echo "started http server..."

echo "starting web ui..."
cd /opt/adminTool/
sh start.sh

#echo "satrting npm..."
#npm run start
#npm run start  > log.txt &
