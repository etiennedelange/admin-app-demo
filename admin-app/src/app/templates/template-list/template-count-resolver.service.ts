import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { TemplateService } from '../template.service';

@Injectable({
    providedIn: 'root'
})
export class TemplateCountResolverService {
    constructor(private templateService: TemplateService) { }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<number> {
        return this.templateService.getTotal();
    }
}
