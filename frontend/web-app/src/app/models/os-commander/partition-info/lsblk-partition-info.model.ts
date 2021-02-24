import { PartitionInfo } from './partition-info.model';

export interface LsblkPartitionInfo extends PartitionInfo {
    uuid: string;
}
