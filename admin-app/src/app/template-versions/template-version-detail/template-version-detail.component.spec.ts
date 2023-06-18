import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormBuilder } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { RouterTestingModule } from '@angular/router/testing';
import { FormBuilderMock, SnackBarServiceMock } from 'src/tests/mocks';

import { TemplateVersionDetailComponent } from './template-version-detail.component';

describe('TemplateVersionDetailComponent', () => {
    let component: TemplateVersionDetailComponent;
    let fixture: ComponentFixture<TemplateVersionDetailComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [
                RouterTestingModule,
                HttpClientTestingModule
            ],
            declarations: [
                TemplateVersionDetailComponent
            ],
            providers: [
                { provide: FormBuilder, useClass: FormBuilderMock },
                { provide: MatSnackBar, useClass: SnackBarServiceMock }
            ]
        }).compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(TemplateVersionDetailComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
