import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { AddOfferService } from '../Services/add-offer.service';
import { Offer } from '../SharedClassesAndTypes/Offer';
import { EditOfferService } from '../Services/edit-offer.service';
import { OfferEdit } from '../SharedClassesAndTypes/OfferEdit';
import { OfferService } from '../Services/offer.service';

@Component({
  selector: 'app-edit-offer',
  templateUrl: './edit-offer.component.html',
  styleUrls: ['./edit-offer.component.scss']
})
export class EditOfferComponent implements OnInit{
  offerForm!: FormGroup;
  // offerModel = new OfferEdit(0,'', new Date() , new Date() , 0);
  ErrorMessage: string = '';
  offerId: any;
  Offer: any;
  constructor(private offerService:OfferService,private activatedRoute:ActivatedRoute,private editOffer:EditOfferService,private fb: FormBuilder, private router: Router) {

  }

  ngOnInit() {
    this.offerForm = this.fb.group(
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
    ),
    this.activatedRoute.paramMap.subscribe((params: ParamMap) => {
      this.offerId = params.get("id");
    }),
    this.offerService.GetOfferByID(this.offerId).subscribe({
      next: (data) => this.Offer = data.data,
      error: (erorr: string) => this.ErrorMessage = erorr
    })
  }

  get id() {
    return this.offerForm.get('id');
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
    this.editOffer.EditOffer(this.offerForm.value).subscribe({
      next: data => console.log(data),
      error: error => console.log(error)
    });
    console.log(this.offerForm.value)
    this.router.navigate(['/Dashboard/Offers'])
  }
}
