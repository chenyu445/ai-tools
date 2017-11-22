#!/bin/sh

#生成docker image的版本,如果不传第三个参数,默认是1.0
version="1.0"
if [ "$1" != "" ]; then
  version="$1"
fi

docker_img_name="ai-tools-server"

#项目的文件
code_folder="ai-tools"

#项目的分支
code_branch="release"

#docker运行时的名字
docker_name="ai-tools"

#server的配置文件夹,映射到的docker中
server_config_folder="/home/ai-tools/ai-tools-config/server"
#adminTools的配置文件夹,映射到的docker中
adminTool_config_folder="/home/ai-tools/ai-tools-config/adminTool"

if [ ! -d "$code_folder" ]; then 
  echo "开始从远程clone代码:"$code_branch"分支..."
  git clone http://207.226.142.102:8001/dp/ai-tools.git $code_folder
  cd $code_folder
  git checkout $code_branch
  if [ ! -d "$code_folder" ]; then 
    echo "clone代码出错,请检查git用户名密码和git server..."
    exit 1
  fi
  echo "clone代码已完成..."
else
  echo "开始从远程更新代码:"$code_branch"..."
  cd $code_folder
  git checkout $code_branch
  git pull 
  echo "更新代码已完成..."
fi 
sum_count=`ls |wc -l`
if [ "$sum_count" = "0" ]; then 
  echo "代码更新出错,请检查git用户名密码和git server..."
  exit 1
fi

img_count=`docker images | grep $docker_img_name | wc -l`
if [ "$img_count" = "0" ]; then 
  echo "开始build docker镜像"$docker_img_name",版本号:"$version"..."
  docker build -t $docker_img_name:$version -f Dockerfile-base .
  echo "docker镜像"$docker_img_name" build已经完成"
else
  echo "开始build docker镜像"$docker_img_name",版本号:"$version"..."
  docker build -t $docker_img_name:$version .
  echo "docker镜像"$docker_img_name" build已经完成"
fi

docker_count=`docker ps -a | grep $docker_name | wc -l`
if [ "$docker_count" != "0" ]; then 
  echo "开始停止旧版本Container"
  docker stop $docker_name && docker rm -f $docker_name
  echo "完成停止旧版本Container"
fi

echo "更新[配置文件夹"
cd ..
mv $server_config_folder/config.conf $server_config_folder/config.conf.back
cp -rf $code_folder/annotater/server/Utils/* $server_config_folder/ 
mv $server_config_folder/config.conf.back $server_config_folder/config.conf

mv $adminTool_config_folder/init_start.sh $adminTool_config_folder/init_start.sh.back
cp -rf $code_folder/adminTool/config/* $adminTool_config_folder
mv $adminTool_config_folder/init_start.sh.back $adminTool_config_folder/init_start.sh
echo "完成更新配置文件夹"

echo "启动docker..."
docker run -d --restart=always --name $docker_name -v $server_config_folder:/opt/server/Utils -v $adminTool_config_folder:/opt/adminTool/config -p 8300:8004 -p 10001:10001 $docker_img_name:$version
echo "docker启动已完成..."
