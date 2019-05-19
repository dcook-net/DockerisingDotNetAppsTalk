#!/bin/bash

docker build -t isprime_dotnetcore -f Dockerfile_v5 .
docker run -p 9021:9021 isprime_dotnetcore 