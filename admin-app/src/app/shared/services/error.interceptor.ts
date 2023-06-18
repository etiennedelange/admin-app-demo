import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpEvent, HttpHandler, HttpRequest, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthenticationService } from './authentication.service';

/** Handle HttpErrors. Redirect to login page on 401 error. **/
@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(private auth: AuthenticationService) {
    }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req).pipe(
            catchError((err: HttpErrorResponse) => {
                if (err.status == 401) {
                    this.auth.logout();
                    return next.handle(req);
                } else {
                    return throwError(err);
                }
            })
        );
    }
}