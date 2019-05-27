#!/usr/bin/env bash

docker system prune -f ## clean up any previous/orphaned objects to ensure this run executes cleanly

docker-compose up --build
