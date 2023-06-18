import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { ITemplate, TemplateService } from '../template.service';

@Injectable({
    providedIn: 'root'
})
export class TemplateDetailResolverService implements Resolve<ITemplate> {
    constructor(private templateService: TemplateService) { }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<ITemplate> {
        let id = route.paramMap.get('id');
        return this.templateService.getSingle(+id);
    }
}
