import { NgModule } from "@angular/core"
import { Routes, RouterModule } from "@angular/router"
import { SettingListComponent } from "./setting-list.component"
import { AuthGuard } from "../auth-guard.service"
import { SettingsComponent } from "./settings.component"
import { SettingsPageResolver, SettingCountResolver, SettingDetailResolverService } from "./setting-list-resolver.service"
import { SettingDetailComponent } from "./setting-detail.component"

export const routes: Routes = [
    {
        path: '',
        component: SettingsComponent,
        data: { animation: 'settings' },
        canActivate: [AuthGuard],
        children: [
            {
                path: '',
                component: SettingListComponent,
                resolve: {
                    settings: SettingsPageResolver,
                    total: SettingCountResolver
                }
            },
            {
                path: 'add',
                component: SettingDetailComponent,
            },
            {
                path: ':id',
                component: SettingDetailComponent,
                resolve: {
                    setting: SettingDetailResolverService
                }
            }
        ]
    }
]

@NgModule({
    imports: [
        RouterModule.forChild(routes)
    ],
    exports: [RouterModule] // necessary?
})
export class SettingsRoutingModule {

}