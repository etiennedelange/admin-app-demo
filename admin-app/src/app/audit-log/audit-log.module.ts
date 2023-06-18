import { NgModule } from "@angular/core";
import { AuditLogRoutingModule } from "./audit-log.routing.module";
import { AppSharedModule } from "../app.shared.module";
import { AuditLogListComponent } from "./audit-log-list.component";
import { AuditLogComponent } from "./audit-log.component";

@NgModule({
    imports: [
        AuditLogRoutingModule,
        AppSharedModule
    ],
    declarations: [
        AuditLogListComponent,
        AuditLogComponent
    ]
})
export class AuditLogModule { }