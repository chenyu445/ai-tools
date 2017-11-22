if [ "$1" = "-d" ]; then 
  echo "it will run as a daemon in the backgroud..."
  python HttpServerHelper.py > log.txt &
else
  python HttpServerHelper.py
fi
