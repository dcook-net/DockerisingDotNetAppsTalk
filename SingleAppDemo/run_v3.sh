#!/bin/bash

docker build -t isprime_dotnetcore:3 -f ./Dockerfile_Iterations/v3/Dockerfile .
docker run -p 9021:9021 isprime_dotnetcore:3 