import { TestBed } from '@angular/core/testing';

import { AddSupplierService } from './add-supplier.service';

describe('AddSupplierService', () => {
  let service: AddSupplierService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AddSupplierService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
