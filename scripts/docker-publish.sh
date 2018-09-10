#!/bin/bash
DOCKER_ENV=''
DOCKER_TAG=''

case "$TRAVIS_BRANCH" in
  "master")
    DOCKER_ENV=production
    DOCKER_TAG=latest
    ;;
  "develop")
    DOCKER_ENV=development
    DOCKER_TAG=dev
    ;;    
esac

docker login -u $DOCKER_USERNAME -p $DOCKER_PASSWORD
docker build -t dshop.services.products:$DOCKER_TAG .
docker tag dshop.services.products:$DOCKER_TAG $DOCKER_USERNAME/dshop.services.products:$DOCKER_TAG
docker push $DOCKER_USERNAME/dshop.services.products:$DOCKER_TAG