import { Injectable } from "@angular/core";
import { CanActivate, CanLoad, Router, Route, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthenticationService } from "./shared/services/authentication.service";
import { UserRole } from './shared/user-role';

@Injectable({
    providedIn: "root"
})
export class AuthGuard implements CanActivate, CanLoad {
    constructor(
        private authService: AuthenticationService,
        private router: Router) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        var loggedIn = this.checkLogin(state.url);

        if (!loggedIn) {
            return false;
        }

        if (this.authService.hasRole(UserRole.Administrator)) {
            return true;
        }

        if (state.url === '/home') {
            return true;
        }

        switch (state.url) {
            case '/attorneys':
                return this.authService.hasSomeRoles([UserRole.ManageAttorneys, UserRole.ViewAttorneys]);
            case '/attorneys/add':
                return this.authService.hasRole(UserRole.ManageAttorneys);
            case '/settings':
                return this.authService.hasSomeRoles([UserRole.ManageAttorneys, UserRole.ViewAttorneys]);
            case '/settings/add':
                return this.authService.hasRole(UserRole.ManageAttorneys);
            case '/users':
                return this.authService.hasSomeRoles([UserRole.ManageUsers, UserRole.ViewUsers]);
            case '/users/add':
                return this.authService.hasRole(UserRole.ManageUsers);
            case '/reports':
                return this.authService.hasSomeRoles([UserRole.ManageAttorneys, UserRole.ViewAttorneys]);
            case '/templatefiles':
                return this.authService.hasSomeRoles([UserRole.ManageTemplates, UserRole.ManageTemplates]);
            case '/templateversions':
                return this.authService.hasSomeRoles([UserRole.ManageTemplates, UserRole.ViewTemplates]);
            case '/auditlogs':
                return this.authService.hasRole(UserRole.Administrator);
            default:
                return false;
        }
    }

    canLoad(route: Route): boolean {
        let url = `/${route.path}`;
        return this.checkLogin(url);
    }

    checkLogin(url: string): boolean {
        if (this.authService.isLoggedIn())
            return true;

        this.authService.redirectUrl = url;

        this.router.navigate(["/login"]);
        return false;
    }
}

// Guard passes if user is NOT logged in (use with care)
@Injectable({
    providedIn: "root"
})
export class NotAuthGuard implements CanActivate {
    constructor(private authService: AuthenticationService, private router: Router) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        if (this.authService.isLoggedIn()) {
            this.router.navigate([""]);
            return false;
        }
        return true;
    }
}