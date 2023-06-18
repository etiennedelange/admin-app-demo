import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { FormBuilder } from '@angular/forms';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';
import { of } from 'rxjs';
import { FormBuilderMock, SnackBarServiceMock } from 'src/tests/mocks';

import { AttorneyDetailComponent } from './attorney-detail.component';

describe('AttorneyDetailComponent', () => {
    let component: AttorneyDetailComponent;
    let fixture: ComponentFixture<AttorneyDetailComponent>;

    beforeEach(waitForAsync(() => {
        TestBed.configureTestingModule({
            imports: [
                HttpClientTestingModule,
                RouterTestingModule,
                MatAutocompleteModule
            ],
            declarations: [
                AttorneyDetailComponent,
            ],
            providers: [
                { provide: FormBuilder, useClass: FormBuilderMock },
                { provide: MatSnackBar, useClass: SnackBarServiceMock },
                { provide: MatDialog, useValue: {} },
                {
                    provide: ActivatedRoute, useValue: {
                        data: of({
                            allSettings: []
                        })
                    }
                }
            ]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(AttorneyDetailComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
