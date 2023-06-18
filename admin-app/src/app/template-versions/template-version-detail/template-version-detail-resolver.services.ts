import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { IProductVersion } from 'src/app/templates/template.service';
import { TemplateDesktopVersionService, TemplateOnlineVersionService } from '../template-version.services';

@Injectable({
    providedIn: 'root'
})
export class TemplateDesktopVersionDetailResolverService {
    constructor(private versionService: TemplateDesktopVersionService) { }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<IProductVersion> {
        let id = route.paramMap.get('id');
        return this.versionService.getSingle(+id);
    }
}

@Injectable({
    providedIn: 'root'
})
export class TemplateOnlineVersionDetailResolverService {
    constructor(private versionService: TemplateOnlineVersionService) { }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<IProductVersion> {
        let id = route.paramMap.get('id');
        return this.versionService.getSingle(+id);
    }
}