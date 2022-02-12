#!/bin/sh

# Stop on error
set -e

PATH=$PATH:/usr/sbin

#==== Aliases
alias clear_screen='printf "\033c"'
alias color_default='printf "\e[39m"'
alias color_cyan='printf "\e[36m"'
alias color_magenta='printf "\e[95m"'
alias color_green='printf "\e[32m"'
#====

#==== Utility functions
set_step_color() {
  current_step="${1}"
  control_step="${2}"
  if [ $current_step -gt $control_step ]; then
    color_green
  elif [ $control_step -gt $current_step ]; then
    color_default
  else
    color_magenta
  fi
}

show_steps() {
  step="${1}"
  clear_screen
  set_step_color $step 1
  echo "👉 Update system"
  set_step_color $step 2
  echo "👉 Create armnas user"
  set_step_color $step 3
  echo "👉 Install dependencies"
  set_step_color $step 4
  echo "👉 Install .NET v6.0.2"
  set_step_color $step 5
  echo "👉 Reinstall Armnas"
  set_step_color $step 6
  echo "👉 Setup Caddy"
  set_step_color $step 7
  echo "👉 Register admin user"
  set_step_color $step 8
  echo "👉 Install Samba"
  set_step_color $step 9
  echo "👉 Install Transmission"
  set_step_color $step 10
  echo "👉 Install SSH (optional)"
  set_step_color $step 11
  echo "👉 Install Java 11 (JRE+JDK, optional)"
  set_step_color $step 12
  echo "👉 Install Keycloak (Java needed, optional)"
  set_step_color $step 13
  echo "👉 Finish"
  color_cyan
}
#====

#==== Steps

step_1() {
  apt-get update
  apt-get upgrade -y
}

step_2() {
  # Skip step if user exists
  # If you want to add armnas user again, please run this command before starting this script:
  # sudo killall -u armnas && sudo deluser --remove-home -f armnas
  if ! getent passwd "armnas" > /dev/null 2>&1; then
	useradd -g users -d /home/armnas -s /bin/bash armnas
    echo armnas:$armnas_password | chpasswd
    adduser armnas sudo
    mkhomedir_helper armnas
	  # Dont ask password on sudo, so we can use sudo in app
	  echo "armnas ALL=(ALL) NOPASSWD: ALL" >> /etc/sudoers
  fi
}

step_3() {
  apt-get install debian-keyring acl unzip jq curl debian-archive-keyring ntfs-3g ufw apt-transport-https -y
  # Check if caddy repo was already added. Add if not
  if [ ! -f /etc/apt/sources.list.d/caddy-stable.list ]; then
    curl -1sLf 'https://dl.cloudsmith.io/public/caddy/stable/gpg.key' | sudo apt-key add -
    curl -1sLf 'https://dl.cloudsmith.io/public/caddy/stable/debian.deb.txt' | sudo tee -a /etc/apt/sources.list.d/caddy-stable.list
  fi
  apt-get update
  apt-get install caddy
}

step_4() {
  # Skip step .dotnet directory exist
  # If you want to install .NET again, just remove all dotnet archives, .dotnet directory 
  # and two dotnet lines from .bashrc in /home/armnas/ before running this script
  if [ ! -d /home/armnas/.dotnet ]; then
    cd /home/armnas

    mkdir .dotnet

    case $architecture in
      arm64) wget https://download.visualstudio.microsoft.com/download/pr/51b8de3c-7f68-46cd-96dd-de822855d744/cd8ed6f4342d294df04720006d0eba61/aspnetcore-runtime-6.0.2-linux-arm64.tar.gz -O aspnet.tar.gz;;
      arm32) wget https://download.visualstudio.microsoft.com/download/pr/a7e77f1a-db9d-403f-a611-f925cea0e6f3/af5baacfa05d023671f08bf14f98bcb2/aspnetcore-runtime-6.0.2-linux-arm.tar.gz -O aspnet.tar.gz;;
      x64) wget https://download.visualstudio.microsoft.com/download/pr/26078483-7a4c-4113-8c39-eab5ee24f10f/3c2d24a5c71179af652cc92e8b57eb5f/aspnetcore-runtime-6.0.2-linux-x64.tar.gz -O aspnet.tar.gz;;
    esac
    tar zxf aspnet.tar.gz -C .dotnet
    rm -rf aspnet.tar.gz
  
    if $install_sdk_runtime ; then
      case $architecture in
        arm64) wget https://download.visualstudio.microsoft.com/download/pr/8555e038-2eb9-4042-a3dc-7453ec204a56/bb90dd8837905df3c4351ea2e9ee313d/dotnet-runtime-6.0.2-linux-arm64.tar.gz -O runtime.tar.gz;;
        arm32) wget https://download.visualstudio.microsoft.com/download/pr/fbc7f55f-9a43-498c-a7f0-ad589c64d7d4/e48b95570140a2d9ba460ef9c98f6b1c/dotnet-runtime-6.0.2-linux-arm.tar.gz -O runtime.tar.gz;;
        x64) wget https://download.visualstudio.microsoft.com/download/pr/eec7db31-0187-4751-88ec-53ea526d4d35/d183e3973cc9a4b988cd4d1184db433f/dotnet-runtime-6.0.2-linux-x64.tar.gz -O runtime.tar.gz;;
      esac
      tar zxf runtime.tar.gz -C .dotnet
      rm -rf runtime.tar.gz
    
      case $architecture in
        arm64) wget https://download.visualstudio.microsoft.com/download/pr/93dd8d1e-f2af-45b1-8e86-9b8c3d58f4d2/b3fc3ef9da1db691043387fcb56f4d6f/dotnet-sdk-6.0.102-linux-arm64.tar.gz -O sdk.tar.gz;;
        arm32) wget https://download.visualstudio.microsoft.com/download/pr/2509d293-6a9f-4108-8fe1-10e78341c5df/18f693729320bdbb5e8d936460dd0e2b/dotnet-sdk-6.0.102-linux-arm.tar.gz -O sdk.tar.gz;;
        x64) wget https://download.visualstudio.microsoft.com/download/pr/e7acb87d-ab08-4620-9050-b3e80f688d36/e93bbadc19b12f81e3a6761719f28b47/dotnet-sdk-6.0.102-linux-x64.tar.gz -O sdk.tar.gz;;
      esac
      tar zxf sdk.tar.gz -C .dotnet
      rm -rf sdk.tar.gz
    fi
    
    chown -R armnas /home/armnas/.dotnet
    
    echo "export DOTNET_ROOT=/home/armnas/.dotnet" >> /home/armnas/.bashrc
    echo 'export PATH=$PATH:$DOTNET_ROOT' >> /home/armnas/.bashrc

    DOTNET_ROOT="/home/armnas/.dotnet"
  fi
}

step_5() {

  if [ ! -d /var/www ]; then
    mkdir /var/www
  fi

  if [ ! -d /var/www/armnas ]; then
    mkdir /var/www/armnas
  fi
  
  if [ -d /var/www/armnas/frontend/web-app ]; then
    rm -rf /var/www/armnas/frontend
  fi
  mkdir /var/www/armnas/frontend

  if [ -d /var/www/armnas/backend/WebApi ]; then
    /bin/su -c "/var/www/armnas/backend/WebApi/stop.sh" - armnas
    mv /var/www/armnas/backend/WebApi/armnas.db /var/www/armnas/
    rm -rf /var/www/armnas/backend
	  mkdir /var/www/armnas/backend
	  mkdir /var/www/armnas/backend/WebApi
	  mv /var/www/armnas/armnas.db /var/www/armnas/backend/WebApi/
  else
    mkdir /var/www/armnas/backend
	  mkdir /var/www/armnas/backend/WebApi
  fi

  # --- Angular
  cd /var/www/armnas/frontend/
  mkdir web-app
  cd web-app
  curl -s https://api.github.com/repos/h4570/armnas/releases/latest \
  | grep "browser_download_url.*web-app.zip" \
  | cut -d : -f 2,3 \
  | tr -d \" \
  | wget -i -
  unzip web-app.zip
  rm -rf web-app.zip
  
  # Replace urls in Angular
  sed -i "s/api.armnas.site/$web_api_ip_domain/g" main.*.js
  sed -i "s/armnas.site/$web_app_ip_domain/g" main.*.js

  # --- .NET
  
  cd /var/www/armnas/backend/WebApi
  curl -s https://api.github.com/repos/h4570/armnas/releases/latest \
  | grep "browser_download_url.*web-api.zip" \
  | cut -d : -f 2,3 \
  | tr -d \" \
  | wget -i -
  
  if [ -f /var/www/armnas/backend/WebApi/armnas.db ]; then
    mv armnas.db /var/www/armnas/
    unzip web-api.zip
    rm -rf armnas.db
    mv /var/www/armnas/armnas.db ./
  else
    unzip web-api.zip
  fi
  rm -rf web-api.zip
  
  chmod +x start.sh
  chmod +x stop.sh
  
  # Replace salt
  sed -i "s/<SALT_INSERTED_BY_INSTALLER>/$salt/g" appsettings.json
  
  # Generate and replace private key
  privateKey="$(tr -dc A-Za-z0-9 </dev/urandom | head -c 20)"
  sed -i "s/<KEY_GENERATED_BY_INSTALLER>/$privateKey/g" appsettings.json

  chown -R armnas /var/www/armnas

  # Setup crontab for armnas if not configured
  if [ ! -f /var/spool/cron/crontab/armnas ]; then
    echo "@reboot /var/www/armnas/backend/WebApi/start.sh" | crontab -u armnas -
  elif ! crontab -u armnas -l | grep -c '@reboot /var/www/armnas/backend/WebApi/start.sh'; then
    echo -e "$(crontab -u armnas -l)\n@reboot /var/www/armnas/backend/WebApi/start.sh" | crontab -u armnas -
  fi
  
  # Run as armnas
  /bin/su -c "/var/www/armnas/backend/WebApi/start.sh" - armnas

}

step_6() {
  cd /etc/caddy

  # Numbered backup
  cp -v --backup=t --force Caddyfile Caddyfile
      
  echo \
"{
    auto_https disable_redirects
}

http://$web_app_ip_domain {
    root * /var/www/armnas/frontend/web-app
    encode gzip
    file_server
    try_files {path} {path}/ /index.html
}

http://$web_api_ip_domain {
    reverse_proxy localhost:5070
}" > /etc/caddy/Caddyfile
      
  systemctl reload caddy
  ufw allow 80
}

step_7() {
  # Give a chance to caddy and .NET to wake up
  sleep 10s

  # Add main user to armnas
  curl --header "Content-Type: application/json" \
    --request POST \
    --data "{\"id\":0,\"login\":\"admin\",\"password\":\"$armnas_password\"}" \
    http://$web_api_ip_domain/user/register
}

step_8() {
  echo "samba-common samba-common/workgroup string  WORKGROUP" | debconf-set-selections
  echo "samba-common samba-common/dhcp boolean true" | debconf-set-selections
  echo "samba-common samba-common/do_debconf boolean true" | debconf-set-selections
  apt-get install samba -y
  ufw allow samba
  setfacl -R -m u:armnas:rwx /etc/samba
}

step_9() {
  apt-get install -y transmission-cli transmission-common transmission-daemon
  systemctl stop transmission-daemon
  usermod -aG debian-transmission armnas
  echo "$( jq '."incomplete-dir-enabled"=true' /etc/transmission-daemon/settings.json )" > /etc/transmission-daemon/settings.json
  echo "$( jq '."rpc-authentication-required"=false' /etc/transmission-daemon/settings.json )" > /etc/transmission-daemon/settings.json
  echo "$( jq ".\"rpc-host-whitelist\"=\"$web_app_ip_domain\"" /etc/transmission-daemon/settings.json )" > /etc/transmission-daemon/settings.json
  echo "$( jq '."rpc-whitelist-enabled"=false' /etc/transmission-daemon/settings.json )" > /etc/transmission-daemon/settings.json
  ufw allow 9091
  chown debian-transmission /etc/transmission-daemon/settings.json 
  chmod 755 /etc/transmission-daemon/settings.json
  # This unfortunately not work, because transmission service is changing 
  # directory permissions every start :/
  # setfacl -R -m u:armnas:rwx /etc/transmission-daemon
  systemctl enable transmission-daemon
  systemctl start transmission-daemon
}

step_10() {
  if [ "$install_ssh" = true ]; then
    apt-get install -y openssh-server
  fi
}

step_11() {
  if [ "$install_java" = true ]; then
    apt-get install openjdk-11-jre openjdk-11-jdk -y
	java_folder=$(find /usr/lib/jvm -name "java-11-openjdk*")
	echo "JAVA_HOME=$java_folder/" >> /etc/environment
	. /etc/environment
  fi
}

step_12() {
  if [ "$install_keycloak" = true ]; then 
    if [ ! -d /home/armnas/keycloak ]; then
          cd /home/armnas
	  wget https://github.com/keycloak/keycloak/releases/download/15.0.1/keycloak-15.0.1.zip -O keycloak.zip
          unzip keycloak.zip
          rm -rf keycloak.zip
  	  mv keycloak* keycloak
	  
	  sed -i "s/keycloak.armnas.site/$keycloak_ip_domain/g" /var/www/armnas/frontend/web-app/main.*.js
	  
	  cd keycloak
          echo "/home/armnas/keycloak/bin/standalone.sh -b 0.0.0.0 -Djboss.socket.binding.port-offset=100" > start.sh
	  chmod +x start.sh
	  if ! crontab -u armnas -l | grep -c '@reboot /home/armnas/keycloak/start.sh'; then
            echo -e "$(crontab -u armnas -l)\n@reboot /home/armnas/keycloak/start.sh" | crontab -u armnas -
	  fi
	  
          cd bin/
	  ./add-user-keycloak.sh -u $keycloak_username

	  chown -R armnas /home/armnas/keycloak
          chmod 755 -R /home/armnas/keycloak/*
    fi
  fi
}

#==== Start

#== Get variables

# Check if script is running with root privileges
if [ `id -u` -ne 0 ] ; then echo "Please run as root" ; exit 1 ; fi
myip="$(hostname -I | xargs | awk '{gsub(/ /,", ")}1')"

color_green
echo "=========================="
echo "Armnas installation script"
echo "=========================="
echo " "
echo "Enter domain names only if you have configured the DNS server! Please use 192.168... otherwise!"
echo " "
color_magenta

while true; do
  color_cyan
  echo "Example: $myip OR armnas.com"
  color_magenta
  read -p 'Webpage IP or domain name: ' web_app_ip_domain
  if [ ${#web_app_ip_domain} -ge 3 ]; then 
    break;
  else 
    color_cyan
    echo "Please provide ip/domain that is at least 3 characters long."
    color_magenta;
  fi
done

while true; do
  color_cyan
  echo "Example: $myip:9970 OR api.armnas.com"
  color_magenta
  read -p 'Webpage API IP or domain name: ' web_api_ip_domain
  if [ ${#web_api_ip_domain} -ge 3 ]; then 
    break;
  else 
    color_cyan
    echo "Please provide ip/domain that is at least 3 characters long."
    color_magenta;
  fi
done

while true; do
  color_magenta
  read -p 'Architecture: x64/Arm64/Arm32: ' architecture
  case $architecture in
    [Aa][Rr][Mm]64* ) architecture="arm64"; break;;
    [Aa][Rr][Mm]32* ) architecture="arm32"; break;;
	[Xx]64* ) architecture="x64"; break;;
    * ) 
	  color_cyan
	  echo "Please answer x64 or Arm64 or Arm32"
      color_magenta;;
  esac
done

while true; do
  stty -echo
  printf "Armnas admin password: "
  read armnas_password
  stty echo
  printf "\n"
  if [ ${#armnas_password} -ge 6 ]; then 
    break;
  else 
    color_cyan
    echo "Please provide password that is at least 6 characters long."
    color_magenta;
  fi
done

color_cyan
echo "Please remember provided salt if you want to update Armnas in future!"
color_magenta
while true; do
  read -p "Armnas web passwords salt (10 chars length): " salt
  if [ ${#salt} -ge 10 ]; then 
    break;
  else 
    color_cyan
    echo "Please provide salt that is at least 10 characters long."
	color_magenta;
  fi
done

while true; do
  read -p "Install .NET SDK/Runtime? Not needed by Armnas. (y/N): " yn
  case $yn in
    [Yy]* ) install_sdk_runtime=true; break;;
    [Nn]* ) install_sdk_runtime=false; break;;
    * ) 
	  color_cyan
	  echo "Please answer yes or no."
      color_magenta;;
  esac
done

while true; do
  read -p "Install SSH server? (y/N): " yn
  case $yn in
    [Yy]* ) install_ssh=true; break;;
    [Nn]* ) install_ssh=false; break;;
    * ) 
	  color_cyan
	  echo "Please answer yes or no."
      color_magenta;;
  esac
done

while true; do
  read -p "Install Java 11 JRE+JDK? (y/N): " yn
  case $yn in
    [Yy]* ) install_java=true; break;;
    [Nn]* ) install_java=false; break;;
    * ) 
	  color_cyan
	  echo "Please answer yes or no."
      color_magenta;;
  esac
done

while true; do
  read -p "Install Keycloak? Is so, you will be asked for password, so be vigilant! (y/N): " yn
  case $yn in
    [Yy]* ) install_keycloak=true; install_java=true; break;;
    [Nn]* ) install_keycloak=false; break;;
    * ) 
	  color_cyan
	  echo "Please answer yes or no."
      color_magenta;;
  esac
done

if [ "$install_keycloak" = true ]; then 

  while true; do
  color_magenta
  read -p 'Keycloak user name: ' keycloak_username
  if [ ${#keycloak_username} -ge 4 ]; then 
    break;
  else 
    color_cyan
    echo "Please provide username that is at least 4 characters long."
    color_magenta;
  fi
  done
  
  while true; do
  color_cyan
  echo "Example: $myip:8180 OR keycloak.armnas.com"
  color_magenta
  read -p 'Keycloak IP or domain name: ' keycloak_ip_domain
  if [ ${#keycloak_ip_domain} -ge 3 ]; then 
    break;
  else 
    color_cyan
    echo "Please provide ip/domain that is at least 3 characters long."
    color_magenta;
  fi
  done
  
fi

#== Run steps

show_steps 1
step_1

show_steps 2
step_2

show_steps 3
step_3

show_steps 4
step_4

show_steps 5
step_5

show_steps 6
step_6

show_steps 7
step_7

show_steps 8
step_8

show_steps 9
step_9

show_steps 10
step_10

show_steps 11
step_11

show_steps 12
step_12

#=====

show_steps 999
color_default
