import { LsblkDiskInfo } from 'src/app/models/os-commander/lsblk-disk-info.model';
import { getLsblkPartitionInfoViewModels, LsblkPartitionInfoView } from './partition-info/lsblk-partition-info.view-model';

export const getLsblkDiskInfoViewModels = (models: LsblkDiskInfo[]): LsblkDiskInfoView[] => {
    const result: LsblkDiskInfoView[] = [];
    models.forEach(model => {
        const viewModel = model as LsblkDiskInfoView;
        viewModel.partitions = getLsblkPartitionInfoViewModels(model.partitions);
        result.push(viewModel);
    });
    return result;
};

export interface LsblkDiskInfoView extends LsblkDiskInfo {
    partitions: LsblkPartitionInfoView[];
}
