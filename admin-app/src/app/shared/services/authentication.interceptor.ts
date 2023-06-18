import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthenticationService } from './authentication.service';

/** Intercept http request and set authorization token in the headers **/
@Injectable({
    providedIn: "root"
})
export class AuthenticationInterceptor implements HttpInterceptor {
    constructor(private auth: AuthenticationService) {
    }

    // Intercept API calls and append JWT token
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const authReq = req.clone({
            setHeaders: {
                Authorization: `Bearer ${this.auth.getAuthorizationToken()}`
            }
        });
        return next.handle(authReq);
    }
}