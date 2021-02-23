import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { CPUInfo } from '../models/os-commander/cpu-info.model';
import { RAMInfo } from '../models/os-commander/ram-info.model';
import { LsblkDiskInfo } from '../models/os-commander/lsblk-disk-info.model';
import { DfPartitionInfo } from '../models/os-commander/partition-info/df-partition-info.model';

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

    public async getDisksInfo(): Promise<LsblkDiskInfo[]> { // todo nazwy w komponencie
        return this.http
            .get<LsblkDiskInfo[]>(`${this.url}/disks-info`)
            .toPromise();
    }

    public async getMountedPartitionsInfo(): Promise<DfPartitionInfo[]> {
        return this.http
            .get<DfPartitionInfo[]>(`${this.url}/mounted-partitions`)
            .toPromise();
    }

    public async getMountedPartitionInfo(diskName: string): Promise<DfPartitionInfo> {
        return this.http
            .get<DfPartitionInfo>(`${this.url}/mounted-partition/${encodeURIComponent(diskName)}`)
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
