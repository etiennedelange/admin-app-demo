import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterTestingModule } from '@angular/router/testing';
import { SnackBarServiceMock } from 'src/tests/mocks';

import { AppSidenavComponent } from './app-sidenav.component';

describe('AppSidenavComponent', () => {
    let component: AppSidenavComponent;
    let fixture: ComponentFixture<AppSidenavComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [
                HttpClientTestingModule,
                RouterTestingModule,
                BrowserAnimationsModule
            ],
            declarations: [AppSidenavComponent],
            providers: [
                { provide: MatSnackBar, useClass: SnackBarServiceMock },
                { provide: MatDialog, useValue: {} }
            ]
        })
            .compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(AppSidenavComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
