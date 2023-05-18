import { TestBed } from '@angular/core/testing';

import { DeleteSupplierService } from './delete-supplier.service';

describe('DeleteSupplierService', () => {
  let service: DeleteSupplierService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DeleteSupplierService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
