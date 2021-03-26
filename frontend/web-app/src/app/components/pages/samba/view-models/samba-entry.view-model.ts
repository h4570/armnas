import { SambaEntry } from 'src/app/models/os-commander/samba/samba-entry.model';
import { Utility } from 'src/utility';

export class SambaEntryViewModel {

    public isInEditMode: boolean;

    private _original: SambaEntry;
    private _modified: SambaEntry;
    private _id: number;

    public get id(): number {
        return this._id;
    }

    public get model(): SambaEntry {
        return this.isInEditMode ? this._modified : this._original;
    }

    public set model(value: SambaEntry) { this._modified = value; }

    constructor(model: SambaEntry) {
        this._original = model;
        this._id = Utility.getRandomId();
        this._modified = JSON.parse(JSON.stringify(this._original));
        this.isInEditMode = false;
    }

}
