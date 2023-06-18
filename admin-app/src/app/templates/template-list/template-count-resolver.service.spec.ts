import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';

import { TemplateCountResolverService } from './template-count-resolver.service';

describe('TemplateCountResolverService', () => {
    let service: TemplateCountResolverService;

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpClientTestingModule]
        });
        service = TestBed.inject(TemplateCountResolverService);
    });

    it('should be created', () => {
        expect(service).toBeTruthy();
    });
});
