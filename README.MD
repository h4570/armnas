[![Full CI](https://github.com/h4570/armnas/actions/workflows/ci.yml/badge.svg)](https://github.com/h4570/armnas/actions/workflows/ci.yml)

# Work in progress!

### Roadmap
- Installation
  - Check how to set transmission folders  
`
chgrp debian-transmission /mnt/usbel/Torrents/mytrasnmission/incomplete
https://askubuntu.com/questions/268606/transmission-daemon-permission-denied-even-if-debian-transmission-has-root-grou
`
  - Test Armbian
  - Test Raspbian
- Documentation
  - How to add message (with example)
  - How to check logs
  - Add info: Devices must have partitions, partitions must have UUID (create partition with parted and format `mkfs.ntfs -f /dev/sdb1`)
  - How to install Armbian
    - If running on Armbian  
`add-apt-repository ppa:transmissionbt/ppa -y`
- Create cool readme with test/lint/quality badges
