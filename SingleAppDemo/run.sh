#!/bin/bash

docker build -t isprime_dotnetcore -f ./Dockerfile_Iterations/v5/Dockerfile .
docker run -p 9021:9021 isprime_dotnetcore 