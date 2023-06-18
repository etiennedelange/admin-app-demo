import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { ITemplate, TemplateService } from '../template.service';

@Injectable({
    providedIn: 'root'
})
export class TemplatePageResolverService implements Resolve<ITemplate[]> {
    constructor(private templateService: TemplateService) { }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<ITemplate[]> {
        return this.templateService.get(0, 10, '', 'asc');
    }
}
