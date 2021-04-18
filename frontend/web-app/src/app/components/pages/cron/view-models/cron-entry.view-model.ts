import { CronEntry } from 'src/app/models/os-commander/cron/cron-entry.model';
import { Partition } from 'src/app/models/partition.model';
import { Utility } from 'src/utility';

export class CronEntryViewModel {

    public model: CronEntry;

    private _isDeleted: boolean;
    private _isArmansMountingPoint: boolean;
    private _isInCronAlready: boolean;
    private _original: CronEntry;
    private _id: number;

    constructor(model: CronEntry, isInCronAlready: boolean) {
        this._original = model;
        this._id = Utility.getRandomId();
        this.model = JSON.parse(JSON.stringify(this._original));
        this._isInCronAlready = isInCronAlready;
    }

    public get isDeleted(): boolean {
        return this._isDeleted;
    }

    public get wasModified(): boolean {
        return JSON.stringify(this._original) !== JSON.stringify(this.model);
    }

    public get isInCronAlready(): boolean {
        return this._isInCronAlready;
    }

    public get isArmansMountingPoint(): boolean {
        return this._isArmansMountingPoint;
    }

    public get isArmansStartScript(): boolean {
        return this.model.command === '/var/www/armnas/backend/WebApi/start.sh';
    }

    public markAsDeleted(): void {
        this._isDeleted = true;
    }

    public updateIsArmansMountingPoint(partitions: Partition[]): void {
        const cmdInLower = this.model.command.toLowerCase();
        const haveMntArmnasInPath = cmdInLower.includes('/mnt/armnas');
        const haveMountByUuidInPath = cmdInLower.includes('mount -t auto /dev/disk/by-uuid/');
        if (haveMntArmnasInPath && haveMountByUuidInPath) {
            for (const partition of partitions) {
                const havePartitionName = cmdInLower.includes(partition.displayName.toKebabCase());
                if (havePartitionName) {
                    this._isArmansMountingPoint = true;
                    break;
                }
            }
        }
    }

    public get originalModel(): CronEntry {
        return this._original;
    }

    public get id(): number {
        return this._id;
    }

}
