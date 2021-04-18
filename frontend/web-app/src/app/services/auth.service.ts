import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable()
export class AuthService {

    constructor(
        private readonly jwtHelper: JwtHelperService
    ) { }

    /** Set's JWT auth token in localStorage */
    public saveToken(token: string): void {
        localStorage.setItem('auth-token', token);
    }

    /** Check's if auth token exists and if is not expired */
    public get isAuthenticated(): boolean {
        if (this.token)
            return !this.jwtHelper.isTokenExpired(this.token);
        else return false;
    }

    /** Returns auth token from localStorage */
    public get token(): string | null {
        return localStorage.getItem('auth-token');
    }

}