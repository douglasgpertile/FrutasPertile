#! usr/bin/env bash

while ! docker info > /dev/null 2>&1
do
  echo "Awaiting docker engine to start"
  sleep 1
done

echo ''
echo '####################################################'
echo 'Stopping running containers (if available)...'
echo '####################################################'
echo ''
docker stop $(docker ps -aq)


echo '####################################################'
echo 'Removing containers ..'
echo '####################################################'
echo ''
docker rm $(docker ps -aq)


echo '####################################################'
echo 'Revoming docker container volumes (if any)'
echo '####################################################'
echo ''
docker volume rm $(docker volume ls -q)


echo '####################################################'
echo 'Move to docker compose context folder '
echo '####################################################'
echo ''
cd C:/src/FrutasPertile


echo '####################################################'
echo 'Starting up containers'
echo '####################################################'
echo ''
docker compose up --detach --build 


echo '####################################################'
echo 'Starting up containers'
echo '####################################################'
echo ''
start http://localhost:6980
