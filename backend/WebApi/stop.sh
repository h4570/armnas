#! /bin/bash

cd /var/www/armnas/backend/WebApi
PID_FILE=webapi.pid
if test -f "$PID_FILE"; then
    kill -9 `cat $PID_FILE`
    rm $PID_FILE
fi
