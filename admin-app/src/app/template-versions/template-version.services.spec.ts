import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';

import { TemplateDesktopVersionService, TemplateOnlineVersionService } from './template-version.services';

describe('TemplateDesktopVersionService', () => {
    let service: TemplateDesktopVersionService;

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpClientTestingModule]
        });
        service = TestBed.inject(TemplateDesktopVersionService);
    });

    it('should be created', () => {
        expect(service).toBeTruthy();
    });
});

describe('TemplateOnlineVersionService', () => {
    let service: TemplateOnlineVersionService;

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpClientTestingModule]
        });
        service = TestBed.inject(TemplateOnlineVersionService);
    });

    it('should be created', () => {
        expect(service).toBeTruthy();
    });
});