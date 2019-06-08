#!/bin/bash

docker build -t isprime_dotnetcore:4 -f ./Dockerfile_Iterations/v1/Dockerfile .
docker run -p 9021:9021 isprime_dotnetcore:4