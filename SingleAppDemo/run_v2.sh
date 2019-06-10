#!/bin/bash

docker build -t isprime_dotnetcore:2 -f ./Dockerfile_Iterations/v2/Dockerfile .
docker run -p 9021:9021 isprime_dotnetcore:2