import { Injectable } from '@angular/core';
import { Router, Resolve, RouterStateSnapshot, ActivatedRouteSnapshot } from '@angular/router';
import { AttorneyService, IAttorney } from "./attorney.service";
import { Observable } from 'rxjs';

@Injectable({
    providedIn: "root"
})
export class AttorneyResolver implements Resolve<IAttorney[]> {
    constructor(private attorneyService: AttorneyService, private router: Router) { }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<IAttorney[]> {
        return this.attorneyService.get(0, 10, '', 'asc');
    }
}

@Injectable({
    providedIn: "root"
})
export class AttorneyCountResolver implements Resolve<number> {
    constructor(private attorneyService: AttorneyService, private router: Router) { }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<number> {
        return this.attorneyService.getTotal();
    }
}

@Injectable({
    providedIn: "root"
})
export class AttorneyDetailResolverService implements Resolve<IAttorney> {
    constructor(private attorneyService: AttorneyService, private router: Router) { }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<IAttorney> {
        let id = route.paramMap.get("id");
        return this.attorneyService.getSingle(+id);
    }
}