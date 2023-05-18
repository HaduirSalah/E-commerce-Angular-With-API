import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { Supplier } from '../SharedClassesAndTypes/Supplier';
import { EditSupplierService } from '../Services/edit-supplier.service';
import { SupplierService } from '../Services/supplier.service';
import { SupplierEdit } from '../SharedClassesAndTypes/SupplierEdit';

@Component({
  selector: 'app-edit-supplier',
  templateUrl: './edit-supplier.component.html',
  styleUrls: ['./edit-supplier.component.scss']
})
export class EditSupplierComponent implements OnInit{
  supplierForm!: FormGroup;
  // supplierModel = new SupplierEdit(0,'', '', false, 0, '');
  ErrorMessage: string = '';
  supplierId: any;
  Supplier: any;

  constructor(private supplierService:SupplierService,private activatedRoute:ActivatedRoute,private editSupplier:EditSupplierService,private fb: FormBuilder, private router: Router) {

  }

  ngOnInit() {
    this.supplierForm = this.fb.group(
      {
        id: this.fb.control(
          ''
        ),
        name: this.fb.control(
          '',
          [
            Validators.required,
            Validators.pattern('[a-z A-Z 0-9]+'),
            Validators.minLength(3),
            Validators.maxLength(50),
          ]
        ),
        ssn: this.fb.control(
          '',
          [
            Validators.required,
            Validators.pattern('^[0-9]{14}$'),
          ]
        ),
        verifecationState: this.fb.control(
          true,
          [
            Validators.required
          ]
        ),
        totalSales: this.fb.control(
          '',
          [
            Validators.required
          ]
        ),
        accountNumber: this.fb.control(
          '',
          [
            Validators.pattern('^[0-9]{20}$')
          ]
        ),
      }
    ),
    this.activatedRoute.paramMap.subscribe((params: ParamMap) => {
      this.supplierId = params.get("id");
    }),
    this.supplierService.GetSupplierByID(this.supplierId).subscribe({
      next: (data) => this.Supplier = data.data,
      error: (erorr: string) => this.ErrorMessage = erorr
    })
  }

  get id() {
    return this.supplierForm.get('id');
  }
  get name() {
    return this.supplierForm.get('name');
  }
  get ssn() {
    return this.supplierForm.get('ssn');
  }
  get verifecationState() {
    return this.supplierForm.get('verifecationState');
  }
  get totalSales() {
    return this.supplierForm.get('totalSales');
  }
  get accountNumber() {
    return this.supplierForm.get('accountNumber');
  }

  submitData() {
    this.editSupplier.EditSupplier(this.supplierForm.value).subscribe({
      next: data => console.log(data),
      error: error => console.log(error)
    });
    console.log(this.supplierForm.value)
    this.router.navigate(['/Dashboard/Suppliers'])
  }


  verifecationStateSelected(event: any) {
    var boolValue = JSON.parse(event.target.value);
    this.verifecationState?.setValue(boolValue, { onlySelf: true, });
    console.log(" this.verifecationState   " + boolValue)
  }
}
