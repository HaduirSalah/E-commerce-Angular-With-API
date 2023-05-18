import { TestBed } from '@angular/core/testing';

import { TokenInterceptorerviceService } from './token-interceptorervice.service';

describe('TokenInterceptorerviceService', () => {
  let service: TokenInterceptorerviceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TokenInterceptorerviceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
