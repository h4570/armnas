import { CronEntry } from 'src/app/models/os-commander/cron/cron-entry.model';
import { Partition } from 'src/app/models/partition.model';
import { Utility } from 'src/utility';
import { isValidCron } from 'cron-validator'

export class CronEntryViewModel {

    public model: CronEntry;

    private _isDeleted: boolean;
    private _isArmansMountingPoint: boolean;
    /** Partition name */
    private _isArmnasMountingPointFor: string;
    private _isInCronAlready: boolean;
    private _original: CronEntry;
    private _id: number;
    private _isCronValid: boolean;

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

    public get isCronValid(): boolean {
        return this._isCronValid;
    }

    public get isInCronAlready(): boolean {
        return this._isInCronAlready;
    }

    public get isArmansMountingPoint(): boolean {
        return this._isArmansMountingPoint;
    }

    public get isArmnasMountingPointFor(): string {
        return this._isArmnasMountingPointFor;
    }

    public get isArmansStartScript(): boolean {
        return this.model.command === '/var/www/armnas/backend/WebApi/start.sh';
    }

    public onCronChange(newCronValue: string) {
        this.checkIfCronIsValid(newCronValue);
    }

    public checkIfCronIsValid(cron = this.model.cron): void {
        this._isCronValid = this.isNonStandardCron(cron) || isValidCron(cron);
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
                    this._isArmnasMountingPointFor = partition.displayName;
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

    private isNonStandardCron(text: string): boolean {
        switch (text.toLowerCase()) {
            case "@yearly": return true;
            case "@annually": return true;
            case "@monthly": return true;
            case "@weekly": return true;
            case "@daily": return true;
            case "@hourly": return true;
            case "@reboot": return true;
            default: return false
        };
    }

}
