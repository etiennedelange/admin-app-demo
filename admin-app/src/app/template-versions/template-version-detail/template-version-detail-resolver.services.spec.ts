import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';

import { TemplateDesktopVersionDetailResolverService, TemplateOnlineVersionDetailResolverService } from './template-version-detail-resolver.services';

describe('TemplateDesktopVersionDetailResolverService', () => {
    let service: TemplateDesktopVersionDetailResolverService;

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpClientTestingModule]
        });
        service = TestBed.inject(TemplateDesktopVersionDetailResolverService);
    });

    it('should be created', () => {
        expect(service).toBeTruthy();
    });
});

describe('TemplateOnlineVersionDetailResolverService', () => {
    let service: TemplateOnlineVersionDetailResolverService;

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpClientTestingModule]
        });
        service = TestBed.inject(TemplateOnlineVersionDetailResolverService);
    });

    it('should be created', () => {
        expect(service).toBeTruthy();
    });
});