#!/bin/sh

# Check if script is running with root privileges
if [ `id -u` -ne 0 ] ; then echo "Please run as root" ; exit 1 ; fi

#==== Aliases
alias color_default='printf "\e[39m"'
alias color_magenta='printf "\e[95m"'
#====

killall -u armnas || true
deluser -f armnas || true
color_magenta
echo "Removed armnas user"
color_default

color_magenta
grep -v 'armnas ALL=(ALL) NOPASSWD: ALL' /etc/sudoers > temp && mv temp /etc/sudoers
echo "Removed armnas from sudoers (nopasswd)"
color_default

rm -rf /var/www/armnas || true
color_magenta
echo "Removed /var/www/armnas"
color_default

rm -rf /home/armnas || true
color_magenta
echo "Removed /home/armnas"
color_default
