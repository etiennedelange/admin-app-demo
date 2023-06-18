import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TemplatesComponent } from './templates.component';
import { TemplateListComponent } from './template-list/template-list.component';
import { TemplatesRoutingModule } from './templates.routing.module';
import { AppSharedModule } from '../app.shared.module';
import { TemplateDetailComponent } from './template-detail/template-detail.component';
import { FileUploadDialogComponent } from './file-upload-dialog/file-upload-dialog.component';

@NgModule({
    declarations: [
        TemplatesComponent,
        TemplateListComponent,
        TemplateDetailComponent,
        FileUploadDialogComponent
    ],
    imports: [
        CommonModule,
        TemplatesRoutingModule,
        AppSharedModule
    ]
})
export class TemplatesModule { }
