import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { AuthGuard } from "../auth-guard.service";
import { AuditLogListComponent } from "./audit-log-list.component";
import { AuditLogCountResolver, AuditLogPageResolver } from "./audit-log-resolver.service";
import { AuditLogComponent } from "./audit-log.component";

export const routes: Routes = [
    {
        path: '',
        canActivate: [AuthGuard],
        component: AuditLogComponent,
        children: [
            {
                path: '',
                component: AuditLogListComponent,
                resolve: {
                    auditLogs: AuditLogPageResolver,
                    total: AuditLogCountResolver
                }
            }
        ]
    }
];

@NgModule({
    imports: [
        RouterModule.forChild(routes)
    ],
    exports: [RouterModule]
})

export class AuditLogRoutingModule {

}