import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { Observable } from "rxjs";
import { Injectable } from "@angular/core";
import { AuditLogService, IAuditLog } from "./audit-log.service";

@Injectable({
    providedIn: "root"
})
export class AuditLogPageResolver implements Resolve<IAuditLog[]> {
    constructor(private auditLogService: AuditLogService) { }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): IAuditLog[] | Observable<IAuditLog[]> | Promise<IAuditLog[]> {
        return this.auditLogService.get(0, 10, '', 'desc');
    }
}


@Injectable({
    providedIn: "root"
})
export class AuditLogCountResolver implements Resolve<number> {
    constructor(private auditLogService: AuditLogService) { }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<number> {
        return this.auditLogService.getTotal();
    }
}