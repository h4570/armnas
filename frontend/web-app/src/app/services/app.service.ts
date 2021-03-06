import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Injectable()
export class AppService {

    public isInTweakMode: boolean;

    private _version: string | undefined;

    constructor(private readonly http: HttpClient) { this.isInTweakMode = false; }

    public get version(): string | undefined {
        return this._version;
    }

    /** Update build and version from API */
    public async updateVersionAndBuild(): Promise<void> {
        const appInfo = await this.http
            .get<{ version: string; build: string }>(environment.urls.api + 'app-info')
            .toPromise();
        this._version = appInfo.version;
    }

    /** @returns Language choosed by user or English if not changed. */
    public getUserLanguage(): string {
        try {
            const result = localStorage.getItem('language');
            if (result)
                return result;
        } catch { }
        return 'en-US';
    }


}
