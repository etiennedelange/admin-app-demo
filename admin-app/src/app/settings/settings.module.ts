import { NgModule } from "@angular/core";
import { SettingsRoutingModule } from "./settings.routing.module";
import { AppSharedModule } from "../app.shared.module";
import { SettingListComponent } from "./setting-list.component";
import { SettingsComponent } from "./settings.component";
import { SettingDetailComponent } from './setting-detail.component';

@NgModule({
    imports: [
        SettingsRoutingModule,
        AppSharedModule
    ],
    declarations: [
        SettingsComponent,
        SettingListComponent,
        SettingDetailComponent
    ],
    exports: [
    ]
})
export class SettingsModule { }