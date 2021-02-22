import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { CPUInfo } from '../models/os-commander/cpu-info.model';
import { DiskInfo } from '../models/os-commander/disk-info.model';
import { RAMInfo } from '../models/os-commander/ram-info.model';

@Injectable()
export class SystemInformationService {

    private url = `${environment.urls.api}system-information`;

    constructor(
        private readonly http: HttpClient
    ) { }

    public async getDistributionName(): Promise<string> {
        return this.http
            .get(`${this.url}/distribution`, { responseType: 'text' })
            .toPromise();
    }

    public async getKernelName(): Promise<string> {
        return this.http
            .get(`${this.url}/kernel`, { responseType: 'text' })
            .toPromise();
    }

    public async getCPUInfo(): Promise<CPUInfo> {
        return this.http
            .get<CPUInfo>(`${this.url}/cpu-info`)
            .toPromise();
    }

    public async getRAMInfo(): Promise<RAMInfo> {
        return this.http
            .get<RAMInfo>(`${this.url}/ram-info`)
            .toPromise();
    }

    public async getDisksInfo(): Promise<DiskInfo[]> {
        return this.http
            .get<DiskInfo[]>(`${this.url}/disks-info`)
            .toPromise();
    }

    public async getDiskInfo(diskName: string): Promise<DiskInfo> {
        return this.http
            .get<DiskInfo>(`${this.url}/disk-info/${encodeURIComponent(diskName)}`)
            .toPromise();
    }

    public async getIP(): Promise<string> {
        return this.http
            .get(`${this.url}/ip`, { responseType: 'text' })
            .toPromise();
    }

    public async getStartTime(): Promise<Date> {
        return this.http
            .get(`${this.url}/start-time`, { responseType: 'text' })
            .toPromise()
            .then(c => new Date(c));
    }

}
