### Q1: Installation script failed! I want to try again.
#### A: 
```
sudo chmod +x ./uninstall.sh && sudo ./uninstall.sh
sudo chmod +x ./install.sh && sudo ./install.sh
```

<hr><br />

### Q2: `ERROR: Couldn't determine iptables version` (MUST DO: Raspbian)
#### A: Run this, before installing **and reboot**:
`
sudo update-alternatives --set iptables /usr/sbin/iptables-legacy
`

<hr><br />

### Q3: Installation script stuck on Transmission install! (MUST DO: Armbian)
#### A: Run this, before installing:
`
add-apt-repository ppa:transmissionbt/ppa -y
`

<hr><br />

### Q4: I have Hardkernel's ODROID device with Hardkernel's Ubuntu, and Armnas backend is crashing.
#### A: Install Armbian, because I found that .NET is very unstable on Hardkernel's Ubuntu distro (Segmentation fault errors).

<hr><br />

### Q5: I plugged in USB drive/HDD and it is not shown in Armnas
#### A: Do you have partitions in your drive? If not, create them via parted/gparted. Dont forget to also format these partitions - for example (NTFS) it will be `mkfs.ntfs -f /dev/XXX`

<hr><br />

### Q6: I have weird errors in powershell script. (`dev-scripts/..`)
```
/var/www/armnas/backend/WebApi/start.sh: line 2: $'\r': command not found
/var/www/armnas/backend/WebApi/start.sh: line 3: cd: $'/var/www/armnas/backend/WebApi\r': No such file or directory
/var/www/armnas/backend/WebApi/start.sh: line 9: syntax error: unexpected end of file
```
#### A: Be sure that all ***.sh** scripts in your LOCAL machine (they are copied) are in Unix LF file format.

<hr><br />
