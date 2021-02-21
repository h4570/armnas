#! /bin/bash

cd /var/www/armnas/backend/WebApi
if lsof -ti tcp:5000; then
    lsof -ti tcp:5000 | xargs kill
fi
nohup /root/.dotnet/dotnet WebApi.dll --urls=http://0.0.0.0:5000/ > webapi.log 2>&1 &
echo $! > webapi.pid
