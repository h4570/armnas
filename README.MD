[![Full CI](https://github.com/h4570/armnas/actions/workflows/ci.yml/badge.svg)](https://github.com/h4570/armnas/actions/workflows/ci.yml)

# Work in progress!

### Roadmap
- Installation
  - Check if login works with password entered in installer
  - Check how to set transmission folders  
`
chgrp debian-transmission /mnt/usbel/Torrents/mytrasnmission/incomplete
https://askubuntu.com/questions/268606/transmission-daemon-permission-denied-even-if-debian-transmission-has-root-grou
`
  - Name install script as install.sh
  - Check & fix multiple run on install.sh
  - Create uninstall.sh
  - Fix bugs on Debian x64
- Documentation
  - How to add message (with example)
  - How to install Armbian
    - If running on Armbian  
`add-apt-repository ppa:transmissionbt/ppa -y`
- Create cool readme with test/lint/quality badges
