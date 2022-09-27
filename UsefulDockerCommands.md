
  docker container rm -f $(docker container ls -a -q);
  docker image rm -f $(docker image ls -q);
  docker volume rm $(docker volume ls -q);
  docker network rm $(docker network ls -q);
  docker container rm -f $(docker container ls -a -q);
  docker container rm -f $(docker container ls -a -q)
  docker network rm $(docker network ls -q);
  docker volume rm $(docker volume ls -q);
