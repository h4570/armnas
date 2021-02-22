import { Component, OnDestroy, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { SystemInformationService } from 'src/app/services/system-information.service';

@Component({
  selector: 'app-sys-info',
  templateUrl: './sys-info.component.html',
  styleUrls: ['./sys-info.component.scss']
})
export class SysInfoComponent implements OnInit, OnDestroy {

  public distro = 'Loading';
  public kernel = 'Loading';
  public cpuTemp = 'Loading';
  public cpuUsage = 0;
  public ramUsage = 0;
  public osDiskUsage = 0;
  public ip = 'Loading';
  public upTime = 'Loading';
  public loading = true;

  private keepUpdating = true;
  private refershInterval = 3000;

  constructor(
    private sysInfo: SystemInformationService,
    public translate: TranslateService
  ) { }

  public async ngOnInit(): Promise<void> {
    const promises: Promise<any>[] = [];
    promises.push(this.loadStatic());
    promises.push(this.loadDynamic());
    await Promise.all(promises);
    this.loading = false;
    setTimeout(async () => await this.refresh(), this.refershInterval); // refresh after 5 sec
  }

  public async refresh(): Promise<void> {
    await this.loadDynamic();
    if (this.keepUpdating) // if not destroyed
      setTimeout(() => this.refresh(), this.refershInterval); // refresh again after 5 sec
  }

  public ngOnDestroy(): void {
    this.keepUpdating = false;
  }

  private async loadStatic(): Promise<void> {
    const promises: Promise<any>[] = [];
    promises.push(this.loadDistro());
    promises.push(this.loadKernel());
    promises.push(this.loadIP());
    await Promise.all(promises);
  }

  private async loadDynamic(): Promise<void> {
    const promises: Promise<any>[] = [];
    promises.push(this.loadCPUInfo());
    promises.push(this.loadRAMInfo());
    promises.push(this.loadDiskInfo());
    promises.push(this.loadUpTime());
    await Promise.all(promises);
  }

  private async loadDistro(): Promise<void> {
    try {
      this.distro = await this.sysInfo.getDistributionName();
    } catch {
      this.distro = 'Error';
    }
  }

  private async loadKernel(): Promise<void> {
    try {
      this.kernel = await this.sysInfo.getKernelName();
    } catch {
      this.kernel = 'Error';
    }
  }

  private async loadCPUInfo(): Promise<void> {
    try {
      const cpuInfo = await this.sysInfo.getCPUInfo();
      this.cpuTemp = `${cpuInfo.temperature}C`;
      this.cpuUsage = cpuInfo.percentageUsage;
    } catch {
      this.cpuTemp = 'Error';
      this.cpuUsage = 0;
    }
  }

  private async loadRAMInfo(): Promise<void> {
    try {
      const ramInfo = await this.sysInfo.getRAMInfo();
      const perc = ((ramInfo.totalInKB - ramInfo.freeInKB) / ramInfo.totalInKB) * 100;
      this.ramUsage = parseFloat(perc.toFixed(2));
    } catch {
      this.ramUsage = 0;
    }
  }

  private async loadDiskInfo(): Promise<void> {
    try {
      const disks = await this.sysInfo.getDisksInfo();
      const mainDisk = disks.find(c => c.isMain);
      const perc = (mainDisk.usedMemoryInMB / mainDisk.memoryInMB) * 100;
      this.osDiskUsage = parseFloat(perc.toFixed(2));
    } catch {
      this.osDiskUsage = 0;
    }
  }

  private async loadIP(): Promise<void> {
    try {
      this.ip = await this.sysInfo.getIP();
    } catch {
      this.ip = 'Error';
    }
  }

  private async loadUpTime(): Promise<void> {
    try {
      const start = await this.sysInfo.getStartTime();
      const now = new Date();
      const diff = start.difference(now, 1000 * 60 * 60);
      this.upTime = `${diff.toFixed(2)}h`;
    } catch {
      this.upTime = 'Error';
    }
  }

}
