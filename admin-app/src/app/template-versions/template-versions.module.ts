import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TemplateVersionsRoutingModule } from './template-versions.routing.module';
import { AppSharedModule } from '../app.shared.module';
import { TemplateVersionsComponent } from './template-versions.component';
import { TemplateVersionListComponent } from './template-version-list/template-version-list.component';
import { TemplateVersionDetailComponent } from './template-version-detail/template-version-detail.component';
import { TemplateVersionTableComponent } from './template-version-table/template-version-table.component';

@NgModule({
    declarations: [
        TemplateVersionsComponent,
        TemplateVersionListComponent,
        TemplateVersionDetailComponent,
        TemplateVersionTableComponent
    ],
    imports: [
        TemplateVersionsRoutingModule,
        AppSharedModule
    ]
})
export class TemplateVersionsModule {

}
