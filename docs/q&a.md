- .NET Crashing on Hardkernel's Ubuntu
- Add info: Devices must have partitions, partitions must have UUID (create partition with parted and format `mkfs.ntfs -f /dev/sdb1`)
**For Armbian users**, please add transmission repo (only once):  
`add-apt-repository ppa:transmissionbt/ppa -y`  
**For Raspbian users**, please set default iptables **and reboot**:
`update-alternatives --set iptables /usr/sbin/iptables-legacy`  