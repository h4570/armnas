[![Raspbian ARM32](https://img.shields.io/badge/Raspbian-ARM32-informational.svg)](Raspbian_ARM32)
[![Hardkernel odroid](https://img.shields.io/badge/Armbian-ARM32-informational.svg)](Armbian_ARM32)
[![Debian x64](https://img.shields.io/badge/Debian-x64-informational.svg)](Debian_x64)
[![ARM64](https://img.shields.io/badge/ARM64-informational.svg)](ARM64)&nbsp;
[![i18n EN](https://img.shields.io/badge/i18n-English-orange.svg)](English)
[![i18n PL](https://img.shields.io/badge/i18n-Polish-orange.svg)](Polish)&nbsp;
[![5 minutes install](https://img.shields.io/badge/Install_time-5_minutes-blueviolet.svg)](5MinutesInstall)
<br />
<p align="center">

  <img src="http://apgcglz.cluster028.hosting.ovh.net/github/armnas/main.gif" alt="showcase" width="600px" height="auto">  

  <h1 align="center">Armnas</h1>

  <p align="center">
    Simple NAS web dashboard designed for ARM devices
    <br />
    <a href="https://github.com/h4570/armnas/blob/main/docs/installation.md"><strong>Installation guide »</strong></a>
    <br />
    <br />
    <a href="https://github.com/h4570/armnas/issues">Report Bug</a>
    ·
    <a href="https://github.com/h4570/armnas/issues">Request Feature</a>
  </p>
</p>  

[![Stargazers repo roster for h4570/armnas](https://reporoster.com/stars/h4570/armnas)](https://github.com/h4570/armnas/stargazers)

## Table of Contents  

* [About the Project](#about-the-project) 
  * [Description](#about-project) 
  * [Features list](#features-list) 
  * [Branches](#branches) 
* [Installation](#installation) 
* [Features](#features) 
  * [Messages](#messages) 
* [Development](#development) 
* [Contributing](#contributing) 
* [Application logs](#application-logs) 
* [Questions and answers](#questions-and-answers) 
* [Translation](#translation) 
* [License](#license) 
* [Built With](#built-with) 

## About the Project

### Description
Armnas is a lightweight web NAS dashoard designed for ARM devices. It was created for people who do not want to install another operating system in order to have a NAS dashboard. The installation script is very simple and the average installation time is around 5 minutes. Uninstallation is even easier and takes less than 1 minute. With Armnas, you can buy a Raspberry Pi, a portable drive and enjoy a home NAS for <$100 dollars. Every release is tested on Raspberry pi 4 (Raspbian ARM32), Odroid XU4 (Armbian ARM32) and VMWare (Debian x64).  
Languages: English, Polish.  

[![CI](https://github.com/h4570/armnas/actions/workflows/ci-master.yml/badge.svg)](https://github.com/h4570/armnas/actions/workflows/ci-master.yml)
[![Coverage Status](https://coveralls.io/repos/github/h4570/armnas/badge.svg?branch=)](https://coveralls.io/github/h4570/armnas)
[![License](https://img.shields.io/badge/License-Apache%202.0-blue.svg)](https://opensource.org/licenses/Apache-2.0)
[![.NET 6.0.2](https://img.shields.io/badge/.NET-6.0.2-informational.svg)](https://dotnet.microsoft.com/en-us/)
[![Angular 11](https://img.shields.io/badge/Angular-11-informational.svg)](https://angular.io/)

### Features list
* [System info](#system-info)   
* Quick unmount/mount of [partitions](#partitions)  
* [Partitions](#partitions) automount (on startup)  
* [Samba](#samba) configuration  
* [Torrent client](#transmission) (transmission)  
* [Cron jobs](#cron) configuration  
* [Messages](#messages) system  
* [Keycloak](#keycloak) (optional)

### Branches
* **main**: main releases - tested on **Armbian** (ARM32), **Raspbian** (ARM32) and **Debian** x64.
* **develop**: daily builds - can be unstable.

## Installation  
If you want to install armnas, please run theses two commands:  
`sudo wget https://raw.githubusercontent.com/h4570/armnas/main/install.sh`  
`sudo chmod +x install.sh && sudo ./install.sh`  
If you want to get more details about installation/uninstallation and update procedure, please go to [Installation guide](https://github.com/h4570/armnas/blob/main/docs/installation.md). 

## Uninstallation  
If you want to uninstall armnas, please run theses two commands:  
`sudo wget https://raw.githubusercontent.com/h4570/armnas/main/uninstall.sh`  
`sudo chmod +x uninstall.sh && sudo ./uninstall.sh`  

## Features

### System info 
System information with latest CPU temperature, RAM usage, etc.  
Refresh interval can be set in **Tweak mode**.  
<img src="https://raw.githubusercontent.com/h4570/armnas/master/docs/pics/system-info.png" alt="System info" width="400px" height="auto">

### Partitions 
Partitions module is used to mount/unmount your HDDs, pendrives, etc. very quickly.  
Partitions can be also set in **automount** mode, which will mount you partitions at startup.  
Custom names of the partitions can be set in **Tweak mode**.  
<img src="https://raw.githubusercontent.com/h4570/armnas/master/docs/pics/partitions.png" alt="Partitions" width="400px" height="auto">

### Cron 
Cron jobs allows you to **schedule your custom commands**, programs or bash scripts.  
<img src="https://raw.githubusercontent.com/h4570/armnas/master/docs/pics/cron.png" alt="Cron" width="400px" height="auto">

### Samba 
Samba is used to share some directories or mounted partitions via SMB protocole. You can easily **mount these shares in your Windows machine**.  
<img src="https://raw.githubusercontent.com/h4570/armnas/master/docs/pics/samba.png" alt="Samba" width="400px" height="auto">  
<img src="https://raw.githubusercontent.com/h4570/armnas/master/docs/pics/smb-shares.png" alt="Samba shares" width="400px" height="auto">

### Transmission 
Transmission is a simple torrent client with web dashboard.      
Incomplete directory and complete directory can be set in **Tweak mode**.  
<img src="https://raw.githubusercontent.com/h4570/armnas/master/docs/pics/transmission.png" alt="Transmission" width="400px" height="auto">

### Messages 
Messages system is used to **add notifications directly from your bash scripts, programs and bash terminal**.  
For example, you can create backup script and send the result (success/fail) to Armnas. After it, Armnas will show new notification in navbar area for you.  
Please check [Messages](https://github.com/h4570/armnas/blob/main/docs/messages.md) to get more details.  
<img src="https://raw.githubusercontent.com/h4570/armnas/master/docs/pics/messages.png" alt="Messages" width="300px" height="auto">  

### Keycloak 
Keycloak is an Open Source Identity and Access Management solution for modern Applications and Services.  
[More information](https://github.com/keycloak/keycloak)

## Development 

Armans actively uses bash to execute commands on the target system. To facilitate the development of Armnas, an SSH module has been added. There is logic in the code that checks the current system. When it is Windows, Armnas automatically tries to execute commands on target system via SSH.  
Please check [Development guide](https://github.com/h4570/armnas/blob/main/docs/development.md) to get more details.  

## Contributing 

Contributions are what make the open source community such an amazing place to be learn, inspire, and create. Any contributions you make are **greatly appreciated**. 

1. Fork the Project 
2. Create your Feature Branch (`git checkout -b feature/amazing-feature`)  
3. Commit your Changes (`git commit -m 'Add some amazing-feature'`), please try to use commit in [Angular commit convention](https://github.com/angular/angular/blob/master/CONTRIBUTING.md#-commit-message-format) style  
4. Push to the Branch (`git push origin feature/amazing-feature`)  
5. Open a Pull Request  

## Application logs  

When you encounter a problem in the application, you can double-check what happened in the log file.  
This file is located in: `/var/www/armnas/backend/WebApi/webapi.log`  
The file is cleaned before each Armnas backend run!  

## Questions and answers  

You can find list of the most common problems in [Questions and answers](https://github.com/h4570/armnas/blob/main/docs/q&a.md) file.

## Translation 

If you want to contribute and translate Armans to another language, here is the todo list.
1. Create file with your language translations in: `/frontend/web-app/src/assets/i18n/xx_XX.json` 
2. Add translation for name of your language in (navbar->language): `/frontend/web-app/src/assets/i18n/xx_XX.json`
and other languages
3. Add button with your language in (line 87): `/frontend/web-app/src/app/components/navbar/navbar.component.html` 

## License  

Distributed under the Apache License 2.0 License. See `LICENSE` for more information. 

## Built With  

* [.NET 8.0.11](https://dotnet.microsoft.com/en-us/)  
* [Angular 11](https://angular.io/)  
* [Caddy](https://caddyserver.com/)  
* [oData](https://www.odata.org/)  
* [Swagger](https://swagger.io/)  

Check my blog at <a href="https://hasto.pl">hasto.pl</a>  
Project Link: [https://github.com/h4570/armnas](https://github.com/h4570/armnas)  
Sandro Sobczyński - sandro.sobczynski@gmail.com  
[![LinkedIn][linkedin-shield]](https://linkedin.com/in/sandro-sobczyński-28820b15a)  

[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=flat-square&logo=linkedin&colorB=555  
