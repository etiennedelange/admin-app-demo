import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormBuilder } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, convertToParamMap } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';
import { of } from 'rxjs';
import { FormBuilderMock, SnackBarServiceMock } from 'src/tests/mocks';
import { TemplateDetailComponent } from './template-detail.component';

describe('TemplateDetailComponent', () => {
    let component: TemplateDetailComponent;
    let fixture: ComponentFixture<TemplateDetailComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [
                HttpClientTestingModule,
                RouterTestingModule
            ],
            declarations: [
                TemplateDetailComponent
            ],
            providers: [
                { provide: FormBuilder, useClass: FormBuilderMock },
                { provide: MatSnackBar, useClass: SnackBarServiceMock },
                { provide: MatDialog, useValue: {} },
                {
                    provide: ActivatedRoute, useValue: {
                        data: of({
                            allDesktopVersions: [],
                            allOnlineVersions: []
                        })
                    }
                }
            ]
        }).compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(TemplateDetailComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
