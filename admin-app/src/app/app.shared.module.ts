import { NgModule } from "@angular/core";
import { CommonModule } from '@angular/common';
import { AppMaterialModule } from "./app.material.module";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FlexLayoutModule } from '@angular/flex-layout';
import { AppFormFieldComponent } from "./common/app-form-field/app-form-field.component";
import { httpInterceptorProviders } from './http-interceptors';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AddButtonComponent } from './common/add-button/app-add-button.component';
import { AppDateAdapterProvider, DateFormatProviders } from './date-format-configuration';
import { DragAndDropComponent } from './common/app-drag-and-drop/app-drag-and-drop.component';
import { NgxFileDropModule } from "ngx-file-drop";
import { BackButtonComponent } from "./common/app-back-button/app-back-button.component";
import { AppButtonPrimaryComponent } from "./common/app-button-primary/app-button-primary.component";
import { AppButtonSecondaryComponent } from "./common/app-button-secondary/app-button-secondary.component";
import { AppButtonComponent } from "./common/app-button/app-button.component";
import { PluckPipe } from "./common/pipes/pluck.pipe";
import { SnackBarComponent } from "./common/snack-bar.component/snack-bar.component";
import { BoolStringPipe } from "./common/pipes/boolstring.pipe";
import { DragDropModule } from '@angular/cdk/drag-drop'

@NgModule({
    imports: [
        FormsModule,
        AppMaterialModule,
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        FlexLayoutModule,
        NgxFileDropModule,
        DragDropModule
    ],
    declarations: [
        AppFormFieldComponent,
        AddButtonComponent,
        DragAndDropComponent,
        BackButtonComponent,
        AppButtonPrimaryComponent,
        AppButtonSecondaryComponent,
        AppButtonComponent,
        PluckPipe,
        SnackBarComponent,
        BoolStringPipe
    ],
    exports: [
        FormsModule,
        AppMaterialModule,
        CommonModule,
        ReactiveFormsModule,
        FlexLayoutModule,
        AppFormFieldComponent,
        AddButtonComponent,
        DragAndDropComponent,
        BackButtonComponent,
        AppButtonPrimaryComponent,
        AppButtonSecondaryComponent,
        AppButtonComponent,
        PluckPipe,
        SnackBarComponent,
        BoolStringPipe,
        DragDropModule
    ],
    providers: [
        httpInterceptorProviders,
        MatSnackBar,
        DateFormatProviders,
        AppDateAdapterProvider,
    ]
})
export class AppSharedModule {
    constructor() {
    }
}