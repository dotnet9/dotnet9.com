#!/bin/bash

echo "获取当前容器是否存在-----------------------------------------------------------------"
containerps=$(docker ps -f name=blog.ssr -q)
containerstop=$(docker ps -a -f name=blog.ssr -q)
for alpha in "$containerps";do
    if [ "$alpha" == "" ];then
    echo "检查是否存在停止的容器-------------------------------------------------"
        for alpha1 in "$containerstop";do
          if [ "$alpha1" == "" ];then
          echo "不存指定容器-----------------------------------"
          else
          echo "存在停止了的 然后直接删除-----------开始------------------"
          docker rm $alpha1
          echo "存在停止了的 然后直接删除-----------完成------------------"
        fi
       done
    else
    echo "存在-停止运行 然后删除----------------------开始-----------------"
    docker stop $alpha
    docker rm $alpha
     echo "存在-停止运行 然后删除---------------------完成------------------"
    fi
done

echo "获取当前镜像是否存在-----------------------------------------------------------------"
dockerlist=$(docker images blog.ssr:latest -q)
for alpha2 in "$dockerlist";do
  if [ "$alpha2" == "" ];then
     echo "不存在指定镜像-------------------------------------------------" 
  else
       echo "存在当前指定的镜像 删除镜像--------------开始-----------------------------------"
      docker rmi $alpha2
     echo "存在当前指定的镜像 删除镜像--------------完成-----------------------------------"
  fi
done

echo $WORKSPACE/src/frontend/vite-ssr-blog
cd $WORKSPACE/src/frontend/vite-ssr-blog

#拷贝文件
cp /data/www/config/.env $WORKSPACE/src/frontend/vite-ssr-blog

echo "-----------生成镜像----------"
docker build -t blog.ssr .

echo "-------------运行--------------"
docker run -d -p 5200:5200 --restart=always  --name blog.ssr blog.ssr