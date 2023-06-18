import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { FormBuilder } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { RouterTestingModule } from '@angular/router/testing';
import { FormBuilderMock, SnackBarServiceMock } from 'src/tests/mocks';

import { UserActivationComponent } from './user-activation.component';

describe('UserActivationComponent', () => {
    let component: UserActivationComponent;
    let fixture: ComponentFixture<UserActivationComponent>;

    beforeEach(waitForAsync(() => {
        TestBed.configureTestingModule({
            imports: [
                RouterTestingModule,
                HttpClientTestingModule
            ],
            declarations: [
                UserActivationComponent
            ],
            providers: [
                { provide: FormBuilder, useClass: FormBuilderMock },
                { provide: MatSnackBar, useClass: SnackBarServiceMock }
            ]
        }).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(UserActivationComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
