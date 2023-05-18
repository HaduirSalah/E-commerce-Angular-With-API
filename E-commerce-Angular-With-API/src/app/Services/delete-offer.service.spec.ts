import { TestBed } from '@angular/core/testing';

import { DeleteOfferService } from './delete-offer.service';

describe('DeleteOfferService', () => {
  let service: DeleteOfferService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DeleteOfferService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
