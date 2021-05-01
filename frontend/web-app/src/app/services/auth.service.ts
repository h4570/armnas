import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { BehaviorSubject } from 'rxjs';

@Injectable()
export class AuthService {

    public onIsAuthenticatedChange: BehaviorSubject<boolean>;

    constructor(
        private readonly jwtHelper: JwtHelperService,
        private readonly router: Router
    ) {
        this.onIsAuthenticatedChange = new BehaviorSubject<boolean>(this.isAuthenticated);
    }

    /** Returns auth token from localStorage */
    public get token(): string | null {
        return localStorage.getItem('auth-token');
    }

    /** Set's JWT auth token in localStorage */
    public saveToken(token: string): void {
        localStorage.setItem('auth-token', token);
        this.onIsAuthenticatedChange.next(this.isAuthenticated);
    }

    /** Check's if auth token exists and if is not expired */
    public get isAuthenticated(): boolean {
        if (this.token)
            return !this.jwtHelper.isTokenExpired(this.token);
        return false;
    }

    public logout(): void {
        localStorage.setItem('auth-token', '');
        this.onIsAuthenticatedChange.next(this.isAuthenticated);
        this.router.navigateByUrl('/login');
    }

}
