import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { User } from '../models/user.model';

export enum LoginResponse {
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

    /** Login user. If call is successfull JWT token is saved in local storage */
    public async login(user: User): Promise<LoginResponse> {
        return new Promise<LoginResponse>(async (res) => {
            try {
                const uri = `user/login`;
                await this.http
                    .post<any>(`${environment.urls.api}` + uri, user)
                    .toPromise();
                res(LoginResponse.success);
            } catch (err) {
                if (err instanceof HttpErrorResponse) {
                    if (err.status === 460) res(LoginResponse.loginFailed);
                    else res(LoginResponse.unknownHttpError);
                } else res(LoginResponse.unknownError);
            }
        });
    }

}
