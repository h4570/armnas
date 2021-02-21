import { Component, OnInit } from '@angular/core';
import { SystemInformationService } from 'src/app/services/system-information.service';

@Component({
  selector: 'app-sys-info',
  templateUrl: './sys-info.component.html',
  styleUrls: ['./sys-info.component.scss']
})
export class SysInfoComponent implements OnInit {

  public distro = 'Loading';

  constructor(private sysInfo: SystemInformationService) { }

  public async ngOnInit(): Promise<void> {
    await this.load();
  }

  private async load(): Promise<void> {
    const promises: Promise<any>[] = [];
    promises.push(this.loadDistro());
    await Promise.all(promises);
  }

  private async loadDistro(): Promise<void> {
    console.log(1);
    console.log(await this.sysInfo.getDistributionName());
    this.distro = await this.sysInfo.getDistributionName();
  }

}
