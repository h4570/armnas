# ===--- Variables ---===

$TARGET = "root@192.168.0.155"

# ---
# You can remove passwords from these commands, via installing your ssh key on target machine.
# -> cat id_rsa.pub | ssh root@$TARGET_IP 'mkdir -p ~/.ssh && cat >> ~/.ssh/authorized_keys'
$SSH = 'plink.exe'
$SSH_DEFAULT_ARGS = '-batch', '-pw', 'raspberry'
$SCP = 'pscp.exe'
$SCP_DEFAULT_ARGS = '-r', '-P', '22', '-pw', 'raspberry'
# ----

# ===--- Deploy frontend ---===

Write-Host "Building Angular (frontend)" -ForegroundColor DarkGreen
Set-Location frontend/web-app
ng build --configuration=production --output-hashing=all --output-path=./dist > $null
Set-Location ..
Set-Location ..

Write-Host "Deploying Angular (frontend)" -ForegroundColor Magenta
& $SSH $SSH_DEFAULT_ARGS $TARGET, 'rm -rf /var/www/armnas/frontend/web-app/*' > $null
& $SCP $SCP_DEFAULT_ARGS './frontend/web-app/dist/\*', $TARGET':/var/www/armnas/frontend/web-app' > $null

# ===--- Deploy backend ---===

Write-Host "Building Web API (backend)" -ForegroundColor DarkGreen
Set-Location backend/WebApi
$GIT_TAG = git describe --tags
$GIT_TAG = $GIT_TAG.Substring(1)
dotnet publish -c Release /p:version=${GIT_TAG} --output "./publish" > $null
Set-Location ..
Set-Location ..

Write-Host "Deploying Web API (backend)" -ForegroundColor Magenta
& $SSH $SSH_DEFAULT_ARGS $TARGET, '. /var/www/armnas/backend/WebApi/stop.sh' > $null # stop backend
& $SSH $SSH_DEFAULT_ARGS $TARGET, 'rm -rf /var/www/armnas/backend/WebApi/*' > $null
& $SCP $SCP_DEFAULT_ARGS './backend/WebApi/publish/\*', $TARGET':/var/www/armnas/backend/WebApi' > $null
Remove-Item -LiteralPath "./backend/WebApi/publish" -Force -Recurse 
& $SSH $SSH_DEFAULT_ARGS $TARGET, 'chmod +x /var/www/armnas/backend/WebApi/start.sh' > $null
& $SSH $SSH_DEFAULT_ARGS $TARGET, 'chmod +x /var/www/armnas/backend/WebApi/stop.sh' > $null

& $SSH $SSH_DEFAULT_ARGS $TARGET, '. /var/www/armnas/backend/WebApi/start.sh' > $null # start backend

# ===--- Finish ---===

Write-Host "Done!" -ForegroundColor DarkGreen
