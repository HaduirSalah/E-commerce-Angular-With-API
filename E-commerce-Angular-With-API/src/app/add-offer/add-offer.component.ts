import { Component, OnInit } from '@angular/core';
import { AddOfferService } from '../Services/add-offer.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-offer',
  templateUrl: './add-offer.component.html',
  styleUrls: ['./add-offer.component.scss']
})
export class AddOfferComponent implements OnInit{
  offerForm!: FormGroup;
  // offerModel = new Offer('', new Date() , new Date() , 0);
  ErrorMessage: string = '';
  constructor(private addNewOffer:AddOfferService,private fb: FormBuilder, private router: Router) {

  }

  ngOnInit() {
    this.offerForm = this.fb.group(
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
        startDate: this.fb.control(
          '',
          [
            Validators.required
          ]
        ),
        endDate: this.fb.control(
          '',
          [
            Validators.required
          ]
        ),
        offerPersentage: this.fb.control(
          '',
          [
            Validators.required
          ]
        )
      }
    )
  }

  get name() {
    return this.offerForm.get('name');
  }
  get startDate() {
    return this.offerForm.get('startDate');
  }
  get endDate() {
    return this.offerForm.get('endDate');
  }
  get offerPersentage() {
    return this.offerForm.get('offerPersentage');
  }

  submitData() {
    this.addNewOffer.AddOffer(this.offerForm.value).subscribe({
      next: data => console.log(data),
      error: error => console.log(error)
    });
    console.log(this.offerForm.value)
    this.router.navigate(['/Dashboard/Offers'])
  }

}
