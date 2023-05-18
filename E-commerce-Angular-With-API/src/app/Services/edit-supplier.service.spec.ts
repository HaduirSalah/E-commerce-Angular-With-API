import { TestBed } from '@angular/core/testing';

import { EditSupplierService } from './edit-supplier.service';

describe('EditSupplierService', () => {
  let service: EditSupplierService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EditSupplierService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
