import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';

import { TemplateDetailResolverService } from './template-detail-resolver.service';

describe('TemplateDetailResolverService', () => {
    let service: TemplateDetailResolverService;

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpClientTestingModule]
        });
        service = TestBed.inject(TemplateDetailResolverService);
    });

    it('should be created', () => {
        expect(service).toBeTruthy();
    });
});
