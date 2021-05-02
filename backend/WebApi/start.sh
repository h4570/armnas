#! /bin/bash
# .
cd /var/www/armnas/backend/WebApi
if lsof -ti tcp:5070; then
    lsof -ti tcp:5070 | xargs kill
fi
nohup /home/armnas/.dotnet/dotnet WebApi.dll --urls=http://0.0.0.0:5070/ > webapi.log 2>&1 &
echo $! > webapi.pid

