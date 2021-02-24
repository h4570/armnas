import { LsblkPartitionInfo } from 'src/app/models/os-commander/partition-info/lsblk-partition-info.model';

export const getLsblkPartitionInfoViewModels = (models: LsblkPartitionInfo[]): LsblkPartitionInfoView[] => {
    const result: LsblkPartitionInfoView[] = [];
    models.forEach(model => {
        const viewModel = model as LsblkPartitionInfoView;
        viewModel.displayName = undefined;
        viewModel.isInEditMode = false;
        result.push(viewModel);
    });
    return result;
};

export interface LsblkPartitionInfoView extends LsblkPartitionInfo {
    /** Edit mode can be triggered after clicking on edit button in tweak mode. */
    isInEditMode: boolean;
    /** Display name which was set in edit mode. */
    displayName: string;
}
