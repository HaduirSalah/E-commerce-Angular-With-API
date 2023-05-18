import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { DeleteSupplierService } from '../Services/delete-supplier.service';
import { SupplierService } from '../Services/supplier.service';

@Component({
  selector: 'app-delete-supplier',
  templateUrl: './delete-supplier.component.html',
  styleUrls: ['./delete-supplier.component.scss']
})
export class DeleteSupplierComponent implements OnInit{
  supplierId: any;
  Supplier: any;
  ErrorMessage: string = "";

  constructor(private router:Router ,private activatedRoute: ActivatedRoute, private deleteService: DeleteSupplierService, private supplierService: SupplierService) { }

  ngOnInit(): void {
    this.activatedRoute.paramMap.subscribe((params: ParamMap) => {
      this.supplierId = params.get("id")
    }),
      this.supplierService.GetSupplierByID(this.supplierId).subscribe({
        next: (data) => this.Supplier = data.data,
        error: (erorr: string) => this.ErrorMessage = erorr
      })
  }

  confirmDelete() {
    if (confirm("Are you sure you want to delete this item?")) {
      // user clicked "Yes"
      // do something, such as call an API to delete the item
      this.deleteService.DeleteSupplier(this.Supplier).subscribe({
        next:data=>{
          console.log(data)
          alert("Deleted")
        },
        error:err=>this.ErrorMessage=err
      })
    } else {
      // user clicked "No"
      // do nothing
      this.router.navigate(['/Dashboard'])

    }
  }
}
