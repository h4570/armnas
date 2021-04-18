import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { User } from '../models/user.model';

export enum RegisterResponse {
    success,
    loginFailed,
    unknownHttpError,
    unknownError
}

@Injectable()
export class UserService {

    constructor(
        private readonly http: HttpClient
    ) { }

    /** Register user in floiir. If call is successfull JWT token is saved in local storage */
    public async login(user: User): Promise<RegisterResponse> {
        return new Promise<RegisterResponse>(async (res) => {
            try {
                const uri = `user/login`;
                await this.http
                    .post<any>(`${environment.urls.api}` + uri, user)
                    .toPromise();
                res(RegisterResponse.success);
            } catch (err) {
                if (err instanceof HttpErrorResponse) {
                    if (err.status === 460) res(RegisterResponse.loginFailed);
                    else res(RegisterResponse.unknownHttpError);
                } else res(RegisterResponse.unknownError);
            }
        });
    }

}
