import { Component, OnInit } from '@angular/core';
import { OfferService } from '../Services/offer.service';
import { IOffer } from '../SharedClassesAndTypes/IOffer';
import { DeleteOfferService } from '../Services/delete-offer.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-offers-dashboard',
  templateUrl: './offers-dashboard.component.html',
  styleUrls: ['./offers-dashboard.component.scss']
})
export class OffersDashboardComponent implements OnInit{
  Offers: IOffer[] = [];
  ErrorMessage: string = '';
  Offer!:IOffer;

  constructor(private router:Router,private offerService: OfferService, private deleteService:DeleteOfferService) {

  }

  ngOnInit(): void {
      this.offerService.GetAllOffers().subscribe({
        next:data=>{
          this.Offers=data.data,
          console.log(this.Offers)
        },
        error:err=>this.ErrorMessage=err
      })
  }

  ConfirmDelete(offerId:number){
    this.offerService.GetOfferByID(offerId).subscribe({
      next:data=>this.Offer=data.data,
      error:err=>this.ErrorMessage=err
    })
    console.log(offerId)
    if (confirm("Are you sure you want to delete this offer?")) {
      // user clicked "Yes"
      // do something, such as call an API to delete the item
      this.deleteService.DeleteOffer(this.Offer).subscribe({
        next:data=>{
          console.log(data),
          alert("Offer Deleted Successfully"),
          this.router.navigate(['/Dashboard/Offers'])
        },
        error:err=>this.ErrorMessage=err
      })
    } else {
      // user clicked "No"
      // do nothing
      // this.router.navigate(['/Dashboard'])

    }
  }
}
