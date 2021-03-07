import { LsblkPartitionInfo } from './partition-info/lsblk-partition-info.model';

export interface LsblkDiskInfo {
    name: string;
    memoryInMB: number;
    partitions: LsblkPartitionInfo[];
}
