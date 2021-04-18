import { AuthService } from './services/auth.service';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private readonly authService: AuthService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    // Add token to req if token available
    const token = this.authService.token;
    let newHeaders = req.headers;
    if (token) newHeaders = newHeaders.append('Authorization', `Bearer ${token}`);
    const authReq = req.clone({ headers: newHeaders });

    // Get new token from req if available
    return next.handle(authReq).pipe(
      map(resp => {
        if (resp instanceof HttpResponse) {
          if (resp.headers.has('x-auth-token'))
            this.authService.saveToken(resp.headers.get('x-auth-token'));
          return resp;
        }
      })
    );

  }

}
