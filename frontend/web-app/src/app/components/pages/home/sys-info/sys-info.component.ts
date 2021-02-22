import { Component, OnDestroy, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { SystemInformationService } from 'src/app/services/system-information.service';

export const REFRESH_OFF_VALUE = 10000;

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
  public refreshInterval = 5000;
  public refreshOffValue = REFRESH_OFF_VALUE;

  private isNgDestroyed = false;
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
    this.loadRefreshSettings();
    setTimeout(async () => await this.refresh(), this.refreshInterval);
  }

  public async refresh(): Promise<void> {
    if (this.isNgDestroyed || this.refreshInterval === REFRESH_OFF_VALUE)
      return;
    await this.loadDynamic();
    setTimeout(() => this.refresh(), this.refreshInterval);
  }

  public ngOnDestroy(): void {
    this.isNgDestroyed = true;
  }

  public onRefreshIntervalChange(): void {
    this.saveRefreshSettings();
  }

  public formatRefreshIntervalLabel(value: number) {
    if (value === REFRESH_OFF_VALUE) return 'off';
    return (value / 1000).toFixed(1) + 's';
  }

  private saveRefreshSettings(): void {
    localStorage.setItem('sys-info-refresh-interval', this.refreshInterval.toString());
  }

  private loadRefreshSettings(): void {
    const refInterval = localStorage.getItem('sys-info-refresh-interval');
    if (refInterval)
      this.refreshInterval = parseInt(refInterval, 10);
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
      const perc = ((ramInfo.totalInMB - ramInfo.freeInMB) / ramInfo.totalInMB) * 100;
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
      const diffInMin = start.difference(now, 1000 * 60);
      this.upTime = `${Math.floor(diffInMin / 60)}h ${diffInMin % 60}m`;
    } catch {
      this.upTime = 'Error';
    }
  }

}
