
```bash
  docker container rm -f $(docker container ls -a -q)
  docker image rm -f $(docker image ls -q)
  docker volume rm $(docker volume ls -q)
  docker network rm $(docker network ls -q)
  docker container rm -f $(docker container ls -a -q)
  docker container rm -f $(docker container ls -a -q)
  docker network rm $(docker network ls -q)
  docker volume rm $(docker volume ls -q)
```

docker ps -a -> show all containers and info -a optional gives you more info
docker exec -it $containerID sh. -> bum bum onto the container
docker inspect $containerID -> shows container info like envs, starting command, network settings
/etc/confluent/docker/run -> most likely that is to rerun the container after you hop on it

hop on api container and you can install stuff on the container like "apk update && apk add curl" and then you can curl -I --location --request GET 'preferences:8882/private/liveliness' to see if your stub/service is returning the proper status code

Some cleanup commands:
docker container stop $(docker container ls -aq) -> stop all running containers

these three will remove all containers and prune all networks:
docker container rm $(docker container ls -aq)
docker network prune -f
docker volume rm $(docker volume ls -qf dangling=true) 

docker rmi $(docker images -a -q) -f -> say bye bye to all your images

docker commit $CONTAINER_ID $image_name -> saves current state of a container as an image
docker run -ti --entrypoint=sh $image_name -> rerun the image you saved with your previous command

docker run -it --entrypoint=sh --network=$networkName $image_name -

docker container stop $(docker container ls -aq)
docker container rm $(docker container ls -aq)
docker network prune -f
#docker rmi $(docker images -a -q) -f
docker volume rm $(docker volume ls -qf dangling=true)
esc and when wq! to save it
chmod +x dockerRemove


docker rmi $(docker images -a -q) -f
