import { Component } from '@angular/core';
import { ISupplier } from '../SharedClassesAndTypes/ISupplier';
import { SupplierService } from '../Services/supplier.service';
import { DeleteSupplierService } from '../Services/delete-supplier.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-suppliers',
  templateUrl: './suppliers.component.html',
  styleUrls: ['./suppliers.component.scss']
})
export class SuppliersComponent {
  suppliers: ISupplier[] = [];
  ErrorMessage: string = '';
  Supplier!: ISupplier;

  constructor(private router: Router, private deleteService: DeleteSupplierService, private supplierService: SupplierService) {

  }

  ngOnInit(): void {
    this.supplierService.GetAllSuppliers().subscribe({
      next: data =>
        //console.log(data);
        this.suppliers = data.data,
      error: err => this.ErrorMessage = err
    })
  }

  ConfirmDelete(supplierID: any) {
    this.supplierService.GetSupplierByID(supplierID).subscribe({
      next: data => this.Supplier = data.data,
      error: err => this.ErrorMessage = err
    })
    console.log(supplierID)
    if (confirm("Are you sure you want to delete this Supplier?")) {
      // user clicked "Yes"
      // do something, such as call an API to delete the item
      this.deleteService.DeleteSupplier(this.Supplier).subscribe({
        next: data => {
          console.log(data),
            alert("Product Deleted Successfully"),
            this.router.navigate(['/Dashboard/Suppliers'])
        },
        error: err => this.ErrorMessage = err
      })
    } else {
      // user clicked "No"
      // do nothing
      // this.router.navigate(['/Dashboard'])

    }
  }
}
