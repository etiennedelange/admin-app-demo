import { TestBed } from '@angular/core/testing';
import { MatSnackBar } from '@angular/material/snack-bar';
import { SnackBarServiceMock } from 'src/tests/mocks';

import { SnackBarService } from './snack-bar.service';

describe('SnackBarServiceService', () => {
    let service: SnackBarService;

    beforeEach(() => {
        TestBed.configureTestingModule({
            providers: [
                { provide: MatSnackBar, useClass: SnackBarServiceMock }
            ]
        });
        service = TestBed.inject(SnackBarService);
    });

    it('should be created', () => {
        expect(service).toBeTruthy();
    });
});
