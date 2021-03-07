import { PartitionInfo } from './partition-info.model';

export interface DfPartitionInfo extends PartitionInfo {
    usedMemoryInMB: number;
}
