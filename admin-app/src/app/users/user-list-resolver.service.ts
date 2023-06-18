import { Injectable } from '@angular/core';
import { Router, Resolve, RouterStateSnapshot, ActivatedRouteSnapshot } from '@angular/router';
import { UserService, IUser } from "./user.service";
import { Observable, of } from 'rxjs';
import { IResetPassword } from '../shared/services/authentication.service';

@Injectable({
    providedIn: "root"
})
export class UserListResolver implements Resolve<IUser[]> {
    constructor(private userService: UserService, private router: Router) { }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<IUser[]> {
        return this.userService.get(0, 10, '', 'asc');
    }
}

@Injectable({
    providedIn: "root"
})
export class UserCountResolver implements Resolve<number> {
    constructor(private userService: UserService, private router: Router) { }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<number> {
        return this.userService.getTotal();
    }
}

@Injectable({
    providedIn: "root"
})
export class UserDetailResolverService implements Resolve<IUser> {
    constructor(private userService: UserService, private router: Router) { }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<IUser> {
        let id = route.paramMap.get("id");
        return this.userService.getSingle(+id);
    }
}

@Injectable({
    providedIn: "root"
})
export class ResetPasswordResolver implements Resolve<IResetPassword> {
    constructor(private userService: UserService) { }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<IResetPassword> {
        let id = route.queryParamMap.get("id");
        return this.userService.getUsername(+id);
    }
}