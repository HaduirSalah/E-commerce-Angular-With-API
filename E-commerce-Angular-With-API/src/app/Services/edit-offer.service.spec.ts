import { TestBed } from '@angular/core/testing';

import { EditOfferService } from './edit-offer.service';

describe('EditOfferService', () => {
  let service: EditOfferService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EditOfferService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
