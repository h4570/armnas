import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { smoothHeight } from 'src/app/components/shared/animations';
import { AppService } from 'src/app/services/app.service';
import { SystemInformationService } from 'src/app/services/system-information.service';
import { REFRESH_OFF_VALUE } from '../home.component';

@Component({
  selector: 'app-sys-info',
  templateUrl: './sys-info.component.html',
  styleUrls: ['./sys-info.component.scss'],
  animations: [smoothHeight]
})
export class SysInfoComponent implements OnInit, OnDestroy {

  @Input() public refreshInterval: number;

  public distro = this.translate.instant('home.loading');
  public kernel = this.translate.instant('home.loading');
  public cpuTemp = 0;
  public cpuUsage = 0;
  public ramUsage = 0;
  public osDiskUsage = 0;
  public ip = this.translate.instant('home.loading');
  public upTime = this.translate.instant('home.loading');
  public loading = true;

  private isNgDestroyed = false;

  constructor(
    public readonly appService: AppService,
    private readonly sysInfoService: SystemInformationService,
    public readonly translate: TranslateService
  ) { }

  public async ngOnInit(): Promise<void> {
    const promises: Promise<any>[] = [];
    promises.push(this.loadStatic());
    promises.push(this.loadDynamic());
    await Promise.all(promises);
    this.loading = false;
    setTimeout(async () => await this.refresh(), this.refreshInterval);
  }

  public async refresh(): Promise<void> {
    if (!this.isNgDestroyed && this.refreshInterval !== REFRESH_OFF_VALUE)
      await this.loadDynamic();
    setTimeout(() => this.refresh(), this.refreshInterval);
  }

  public ngOnDestroy(): void {
    this.isNgDestroyed = true;
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
      this.distro = await this.sysInfoService.getDistributionName();
    } catch {
      this.distro = this.translate.instant('common.error');
    }
  }

  private async loadKernel(): Promise<void> {
    try {
      this.kernel = await this.sysInfoService.getKernelName();
    } catch {
      this.kernel = this.translate.instant('common.error');
    }
  }

  private async loadCPUInfo(): Promise<void> {
    try {
      const cpuInfo = await this.sysInfoService.getCPUInfo();
      this.cpuTemp = cpuInfo.temperature;
      this.cpuUsage = cpuInfo.percentageUsage;
    } catch {
      this.cpuTemp = 0;
      this.cpuUsage = 0;
    }
  }

  private async loadRAMInfo(): Promise<void> {
    try {
      const ramInfo = await this.sysInfoService.getRAMInfo();
      const perc = (ramInfo.usedInMB / ramInfo.totalInMB) * 100;
      this.ramUsage = parseFloat(perc.toFixed(2));
    } catch {
      this.ramUsage = 0;
    }
  }

  private async loadDiskInfo(): Promise<void> {
    try {
      const disks = await this.sysInfoService.getMountedPartitionsInfo();
      const mainDisk = disks.find(c => c.isMain);
      const perc = (mainDisk.usedMemoryInMB / mainDisk.memoryInMB) * 100;
      this.osDiskUsage = parseFloat(perc.toFixed(2));
    } catch {
      this.osDiskUsage = 0;
    }
  }

  private async loadIP(): Promise<void> {
    try {
      this.ip = await this.sysInfoService.getIP();
    } catch {
      this.ip = this.translate.instant('common.error');
    }
  }

  private async loadUpTime(): Promise<void> {
    try {
      const start = await this.sysInfoService.getStartTime();
      const now = new Date();
      const diff = start.difference(now, 1000 * 60);
      this.upTime = `${Math.floor(diff / 60)}h ${diff % 60}m`;
    } catch {
      this.upTime = this.translate.instant('common.error');
    }
  }

}
