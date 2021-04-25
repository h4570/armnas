#!/bin/sh

#==== Aliases
alias clear_screen='printf "\033c"'
alias color_default='printf "\e[39m"'
alias color_cyan='printf "\e[36m"'
alias color_yellow='printf "\e[93m"'
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
    color_yellow
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
  echo "👉 Install .NET Core"
  set_step_color $step 5
  echo "👉 Setup Caddy"
  set_step_color $step 6
  echo "👉 Reinstall Armnas"
  set_step_color $step 7
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
  if getent passwd "armnas" > /dev/null 2>&1; then
    return 1
  fi
  # If you want to add armnas user again, please run this command before starting this script:
  # sudo killall -u armnas && sudo deluser --remove-home -f armnas
  
  useradd armnas
  echo armnas:$armnas_password | chpasswd
  adduser armnas sudo
  mkhomedir_helper armnas
}

step_3() {
  apt-get install debian-keyring debian-archive-keyring apt-transport-https -y
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
  if [ -d /home/armnas/.dotnet ]; then
    return 1
  fi
  # If you want to install .NET again, just remove all dotnet archives, .dotnet directory 
  # and two dotnet lines from .bashrc in /home/armnas/ before running this script
  
  cd /home/armnas

  mkdir .dotnet
  
  wget https://download.visualstudio.microsoft.com/download/pr/6d8c577b-d6a2-4110-a105-58f578f136db/236c018b3ee005d47fdcb5e9960eaf1f/aspnetcore-runtime-5.0.3-linux-arm.tar.gz
  tar zxf aspnetcore-runtime-5.0.3-linux-arm.tar.gz -C .dotnet
  rm -rf aspnetcore-runtime-5.0.3-linux-arm.tar.gz
 
  if $install_sdk_runtime ; then
    wget https://download.visualstudio.microsoft.com/download/pr/94f3d0cd-6ccc-4eac-bac5-7fd1396581d5/b51a89d445f3fb7b2a795f0119fc0575/dotnet-runtime-5.0.3-linux-arm.tar.gz
    wget https://download.visualstudio.microsoft.com/download/pr/cd11b0d1-8d79-493f-a702-3ecbadb040aa/d24855458a90944d251dd4c68041d0b7/dotnet-sdk-5.0.103-linux-arm.tar.gz
    tar zxf dotnet-runtime-5.0.3-linux-arm.tar.gz -C .dotnet
    tar zxf dotnet-sdk-5.0.103-linux-arm.tar.gz -C .dotnet
    rm -rf dotnet-runtime-5.0.3-linux-arm.tar.gz
    rm -rf dotnet-sdk-5.0.103-linux-arm.tar.gz
  fi
  
  chown -R armnas /home/armnas/.dotnet
  
  echo "export DOTNET_ROOT=/home/armnas/.dotnet" >> /home/armnas/.bashrc
  echo "export PATH=\$PATH:\$DOTNET_ROOT" >> /home/armnas/.bashrc

  source /home/armnas/.bashrc
}


step_5() {
  # Skip step if armnas_is_configured file exist
  if [ -f /etc/caddy/armnas_is_configured ]; then
    return 1
  fi
  # If you want to setup Caddy again replace Caddyfile with Caddyfile.bak
  # and remove armnas_is_configured file
 
# cd /etc/caddy
# cp Caddyfile Caddyfile.bak
# nano /etc/caddy/Caddyfile
# systemctl reload caddy
# ufw allow 80
# touch armnas_is_configured
}

step_6() {
  echo 'Hello!'
# if not exists /var/www/armnas
# mkdir /var/www/armnas

# if exists /var/www/armnas/frontend/
# rm -rf /var/www/armnas/frontend/
# mkdir /var/www/armnas/frontend/

# if exists /var/www/armnas/backend/
# rm -rf /var/www/armnas/backend/ (EXCEPT armnas.db)
# mkdir /var/www/armnas/backend/

# Download angular
# replace ip in angular

# Download WebApi
# Replace salt
# Generate private key
# Replace private key

# if not exists @reboot /var/www/armnas/backend/WebApi/start.sh
# add @reboot /var/www/armnas/backend/WebApi/start.sh
}

#==== Start

#== Get variables

# Check if script is running with root privileges
if [ `id -u` -ne 0 ] ; then echo "Please run as root" ; exit 1 ; fi

color_green
echo "=========================="
echo "Armnas installation script"
echo "=========================="

color_yellow

while true; do
  read -p 'IP or domain name: ' ip_domain
  if [ ${#ip_domain} -ge 3 ]; then 
    break;
  else 
    color_cyan
    echo "Please provide ip/domain that is at least 3 characters long."
    color_yellow;
  fi
done

while true; do
  stty -echo
  printf "Armnas user password: "
  read armnas_password
  stty echo
  printf "\n"
  if [ ${#armnas_password} -ge 6 ]; then 
    break;
  else 
    color_cyan
    echo "Please provide password that is at least 6 characters long."
    color_yellow;
  fi
done

color_cyan
echo "Please remember provided salt if you want to update Armnas in future!"
color_yellow
while true; do
  read -p "Armnas web passwords salt (10 chars length): " salt
  if [ ${#salt} -ge 10 ]; then 
    break;
  else 
    color_cyan
    echo "Please provide salt that is at least 20 characters long."
	color_yellow;
  fi
done

while true; do
  read -p "Install .NET Core SDK/Runtime which is needed only for armnas development? (y/N): " yn
  case $yn in
    [Yy]* ) install_sdk_runtime=true; break;;
    [Nn]* ) install_sdk_runtime=false; break;;
    * ) 
	  color_cyan
	  echo "Please answer yes or no."
      color_yellow;;
  esac
done

#== Run steps

show_steps 1
#step_1

show_steps 2
#step_2

show_steps 3
#step_3

show_steps 4
#step_4

show_steps 5
step_5

#show_steps 6
#step_6

#=====

show_steps 8
color_default
