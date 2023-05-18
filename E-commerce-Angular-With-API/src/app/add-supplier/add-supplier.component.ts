import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Supplier } from '../SharedClassesAndTypes/Supplier';
import { AddSupplierService } from '../Services/add-supplier.service';

@Component({
  selector: 'app-add-supplier',
  templateUrl: './add-supplier.component.html',
  styleUrls: ['./add-supplier.component.scss']
})
export class AddSupplierComponent implements OnInit{
  supplierForm!: FormGroup;
  // supplierModel = new Supplier('', '', false, 0, '');
  ErrorMessage: string = '';
  constructor(private addNewSupplier:AddSupplierService,private fb: FormBuilder, private router: Router) {

  }

  ngOnInit() {
    this.supplierForm = this.fb.group(
      {
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
    )
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
    this.addNewSupplier.AddSupplier(this.supplierForm.value).subscribe({
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
