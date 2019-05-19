#!/usr/bin/env bash

docker system prune -f

#eval "$(aws ecr get-login --no-include-email --region us-east-1)"

docker-compose -f ./docker-compose.yml up --build --exit-code-from test

exitCode=$?

docker-compose down --remove-orphans

RED="\033[1;31m"
GREEN="\033[1;32m"

if [ $exitCode -eq 0 ]
then
   echo -e "${GREEN}Tests PASSED. "
   
else
    echo -e "${RED}Tests Failed. "
fi

exit $exitCode
