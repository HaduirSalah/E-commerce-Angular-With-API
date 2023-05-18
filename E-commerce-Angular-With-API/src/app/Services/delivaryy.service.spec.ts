import { TestBed } from '@angular/core/testing';

import { DelivaryyService } from './delivaryy.service';

describe('DelivaryyService', () => {
  let service: DelivaryyService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DelivaryyService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
