import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { Provider } from '@angular/core';
import { AuthenticationInterceptor } from './shared/services/authentication.interceptor';
import { ErrorInterceptor } from './shared/services/error.interceptor';
import { LoadingBarInterceptor } from './shared/services/loading-bar.interceptor';

export const httpInterceptorProviders: Provider[] = [
    { provide: HTTP_INTERCEPTORS, useClass: AuthenticationInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: LoadingBarInterceptor, multi: true }
]