import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { MatSnackBar } from '@angular/material/snack-bar';
import { SnackBarServiceMock } from 'src/tests/mocks';

import { ReportListComponent } from './report-list.component';

describe('ReportListComponent', () => {
    let component: ReportListComponent;
    let fixture: ComponentFixture<ReportListComponent>;

    beforeEach(waitForAsync(() => {
        TestBed.configureTestingModule({
            imports: [HttpClientTestingModule],
            declarations: [ReportListComponent],
            providers: [
                { provide: MatSnackBar, useClass: SnackBarServiceMock }
            ]
        }).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(ReportListComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
