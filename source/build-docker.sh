#!/bin/bash

# Usage: ./build-docker.sh <tag>
TAG=${1:-latest}

docker build --platform linux/amd64 -t hajamohideen89/pa-event-alerts:"$TAG" .
