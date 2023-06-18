import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TemplateVersionsComponent } from './template-versions.component';
import { TemplateVersionListComponent } from './template-version-list/template-version-list.component';
import { AuthGuard } from '../auth-guard.service';
import { TemplateDesktopVersionListCountResolverService, TemplateDesktopVersionListResolverService, TemplateOnlineVersionListCountResolverService, TemplateOnlineVersionListResolverService } from './template-version-list/template-version-list-resolver.services';
import { TemplateVersionDetailComponent } from './template-version-detail/template-version-detail.component';
import { TemplateDesktopVersionDetailResolverService, TemplateOnlineVersionDetailResolverService } from './template-version-detail/template-version-detail-resolver.services';

export const routes: Routes = [
    {
        path: '',
        canActivate: [AuthGuard],
        component: TemplateVersionsComponent,
        children: [
            {
                path: '',
                component: TemplateVersionListComponent,
                resolve: {
                    desktopTemplatesVersions: TemplateDesktopVersionListResolverService,
                    onlineTemplatesVersions: TemplateOnlineVersionListResolverService,
                    desktopTemplateVersionTotal: TemplateDesktopVersionListCountResolverService,
                    onlineTemplateVersionTotal: TemplateOnlineVersionListCountResolverService
                },
            },
            {
                path: 'desktop/add',
                component: TemplateVersionDetailComponent
            },
            {
                path: 'online/add',
                component: TemplateVersionDetailComponent
            },
            {
                path: 'desktop/:id',
                component: TemplateVersionDetailComponent,
                resolve: {
                    templateVersion: TemplateDesktopVersionDetailResolverService
                }
            },
            {
                path: 'online/:id',
                component: TemplateVersionDetailComponent,
                resolve: {
                    templateVersion: TemplateOnlineVersionDetailResolverService
                }
            }
        ]
    }
]

@NgModule({
    declarations: [],
    imports: [
        RouterModule.forChild(routes)
    ],
    exports: [RouterModule]
})
export class TemplateVersionsRoutingModule { }
