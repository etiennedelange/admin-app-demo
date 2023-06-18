import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http"
import { Router } from "@angular/router";
import { Observable, BehaviorSubject, Subject } from 'rxjs';
import { tap, map } from 'rxjs/operators';
import { UserRole } from '../user-role';
import { SidenavService } from "src/app/common/sidenav.service";

@Injectable({
    providedIn: "root"
})
export class AuthenticationService {
    redirectUrl: string;
    private userAuthenticated: BehaviorSubject<IAuthenticatedUser>;
    private loginState: Subject<boolean>;

    constructor(
        private http: HttpClient,
        private router: Router,
        private sidenavService: SidenavService
    ) {
        this.userAuthenticated = new BehaviorSubject({
            roles: [],
            username: this.getUsername(),
            fullName: this.getUserFullName()
        });

        this.loginState = new Subject();
    }

    public login(username: string, password: string): Observable<boolean> {
        return this.http
            .post<IUser>('api/authentication/login', {
                username: username,
                password: password
            })
            .pipe(
                tap((result: IAuthenticatedUser) => {
                    localStorage.setItem('auth_token', result.token);
                    localStorage.setItem('username', username);
                    localStorage.setItem('fullname', result.fullName);
                    localStorage.setItem('user', JSON.stringify(result));

                    this.userAuthenticated.next(result);
                    this.loginState.next(true);
                    this.sidenavService.toggleLeftSidenav(true);
                    this.sidenavService.toggleLastKnownStateForRightSidenav();
                }),
                map(result => !!result.token)
            );
    }

    public logout() {
        localStorage.removeItem('auth_token');
        localStorage.removeItem('username');
        localStorage.removeItem('fullname');
        localStorage.removeItem('user');

        this.sidenavService.closeSideNavs(true);

        this.loginState.next(false);

        this.router.navigate(['/login']);
    }

    public register(id: string, token: string, password: string): Observable<Object> {
        return this.http.post("api/authentication/register", {
            id: id,
            token: token,
            password: password
        });
    }

    public changePassword(username: string, currentPassword: string, newPassword: string): Observable<Object> {
        return this.http.post("api/authentication/changepassword", {
            username: username,
            currentPassword: currentPassword,
            newPassword: newPassword
        });
    }

    public sendResetPasswordToken(username: string): Observable<Object> {
        return this.http.post("api/authentication/sendresetpasswordtoken", {
            username: username
        });
    }

    public resetPassword(resetPassword: IResetPassword): Observable<Object> {
        return this.http.post("api/authentication/resetPassword", {
            id: resetPassword.id,
            token: resetPassword.token,
            newPassword: resetPassword.newPassword
        });
    }


    public isLoggedIn(): boolean {
        return !!localStorage.getItem('auth_token');
    }

    public getUsername(): string {
        return localStorage.getItem('username');
    }

    public getUserFullName(): string {
        return localStorage.getItem('fullname');
    }

    public getToken(): string {
        return localStorage.getItem('auth_token');
    }

    public getAuthorizationToken() {
        return localStorage.getItem('auth_token');
    }

    public authenticationChanged(): Observable<IAuthenticatedUser> {
        return this.userAuthenticated.asObservable();
    }

    public loginStateChanged(): Observable<boolean> {
        return this.loginState.asObservable();
    }

    private getUser(): IAuthenticatedUser {
        return JSON.parse(localStorage.getItem('user')) as IAuthenticatedUser;
    }

    /**
    * Returns true if user has the specified role
     */
    public hasRole(role: string): boolean {
        let user = this.getUser();
        return user?.roles?.find((val) => val === role) !== undefined;
    }

    /**
    * Returns true if user has all of the specified roles
     */
    public hasAllRoles(roles: UserRole[]): boolean {
        let user = this.getUser();
        return roles.every((role) => {
            return user?.roles?.find((val) => val === role) !== undefined;
        });
    }

    /**
    * Returns true if user has at least one of the specified roles
     */
    public hasSomeRoles(roles: UserRole[]): boolean {
        let user = this.getUser();
        return roles.some((role) => {
            return user?.roles?.find((val) => val === role) !== undefined;
        });
    }

    public updateLocalUser(newUser: IAuthenticatedUser): void {
        let user = this.getUser();
        user.username = newUser.username;
        user.roles = newUser.roles;
        localStorage.setItem('user', JSON.stringify(user));
    }
}

export interface IUser {
    username: string;
    token?: string;
    roles?: string[];
}

export interface IResetPassword {
    id: string;
    token?: string;
    username?: string;
    newPassword?: string;
}

export class IAuthenticatedUser {
    username?: string;
    fullName?: string;
    token?: string;
    roles?: string[];
}