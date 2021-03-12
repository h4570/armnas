import { SambaEntry } from 'src/app/models/os-commander/samba/samba-entry.model';

export class SambaEntryViewModel {

    public isInEditMode: boolean;

    private _original: SambaEntry;
    private _modified: SambaEntry;

    public get model(): SambaEntry {
        return this.isInEditMode ? this._modified : this._original;
    }

    public set model(value: SambaEntry) { this._modified = value; }

    constructor(model: SambaEntry) {
        this._original = model;
        this._modified = JSON.parse(JSON.stringify(this._original));
        this.isInEditMode = false;
    }

}
