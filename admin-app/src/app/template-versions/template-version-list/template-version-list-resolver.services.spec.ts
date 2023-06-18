import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { TemplateDesktopVersionListResolverService, TemplateOnlineVersionListResolverService } from './template-version-list-resolver.services';

describe('TemplateDesktopVersionResolverService', () => {
    let service: TemplateDesktopVersionListResolverService;

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpClientTestingModule]
        });
        service = TestBed.inject(TemplateDesktopVersionListResolverService);
    });

    it('should be created', () => {
        expect(service).toBeTruthy();
    });
});

describe('TemplateOnlineVersionResolverService', () => {
    let service: TemplateOnlineVersionListResolverService;

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpClientTestingModule]
        });
        service = TestBed.inject(TemplateOnlineVersionListResolverService);
    });

    it('should be created', () => {
        expect(service).toBeTruthy();
    });
});