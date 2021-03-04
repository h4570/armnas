import { LsblkPartitionInfo } from 'src/app/models/os-commander/partition-info/lsblk-partition-info.model';

export const getLsblkPartitionInfoViewModels = (models: LsblkPartitionInfo[]): LsblkPartitionInfoView[] => {
    const result: LsblkPartitionInfoView[] = [];
    models.forEach(model => {
        const viewModel = new LsblkPartitionInfoView();
        viewModel.uuid = model.uuid;
        viewModel.name = model.name;
        viewModel.mountingPoint = model.mountingPoint;
        viewModel.isMain = model.isMain;
        viewModel.memoryInMB = model.memoryInMB;

        viewModel.displayName = undefined;
        viewModel.isInEditMode = false;
        result.push(viewModel);
    });
    return result;
};

export class LsblkPartitionInfoView implements LsblkPartitionInfo {
    public uuid: string;
    public name: string;
    public mountingPoint: string;
    public isMain: boolean;
    public memoryInMB: number;

    // ---

    /** Edit mode can be triggered after clicking on edit button in tweak mode. */
    public isInEditMode: boolean;
    /** Usefull to know if we will edit or create new entry in db.  */
    public dbId: number;
    public displayName: string;
    public isFreezed: boolean;

    private _cachedDisplayName: string;
    public get cachedDisplayName(): string { return this._cachedDisplayName; };
    /** Set current display name to cached display name */
    public updateCachedDisplayName(): void { this._cachedDisplayName = this.displayName; }

}
