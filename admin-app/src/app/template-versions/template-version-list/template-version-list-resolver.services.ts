import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { IProductVersion } from '../../templates/template.service';
import { TemplateDesktopVersionService, TemplateOnlineVersionService } from '../template-version.services';

@Injectable({
    providedIn: 'root'
})
export class TemplateOnlineVersionListResolverService implements Resolve<IProductVersion[]> {
    constructor(private versionService: TemplateOnlineVersionService) { }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<IProductVersion[]> {
        return this.versionService.getAll();
    }
}

@Injectable({
    providedIn: 'root'
})
export class TemplateDesktopVersionListResolverService implements Resolve<IProductVersion[]> {
    constructor(private versionService: TemplateDesktopVersionService) { }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<IProductVersion[]> {
        return this.versionService.getAll();
    }
}

@Injectable({
    providedIn: 'root'
})
export class TemplateDesktopVersionListCountResolverService implements Resolve<number> {
    constructor(private versionService: TemplateDesktopVersionService) { }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<number> {
        return this.versionService.getTotal();
    }
}

@Injectable({
    providedIn: 'root'
})
export class TemplateOnlineVersionListCountResolverService implements Resolve<number> {
    constructor(private versionService: TemplateOnlineVersionService) { }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<number> {
        return this.versionService.getTotal();
    }
}