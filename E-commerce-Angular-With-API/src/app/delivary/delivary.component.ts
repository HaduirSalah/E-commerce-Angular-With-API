import { Component } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { DelivaryyService } from '../Services/delivaryy.service';
import { IDelivery } from '../SharedClassesAndTypes/IDelivery';
import { IOrder } from '../SharedClassesAndTypes/IOrder';

@Component({
  selector: 'app-delivary',
  templateUrl: './delivary.component.html',
  styleUrls: ['./delivary.component.scss']
})
export class DelivaryComponent {
  applicationusridOfDeivary:any;
  delivary!:any;
  orders!:IOrder[];
  TokenOrders:any;


  constructor(private _ActivatedRoute:ActivatedRoute , private _DelivaryyService:DelivaryyService)
  {

  }
  ngOnInit(): void {
    this._ActivatedRoute.paramMap.subscribe((param:ParamMap)=>{
                     this.applicationusridOfDeivary = param.get("delivaryID");
                    console.log(this.applicationusridOfDeivary);




    });

    if (this.applicationusridOfDeivary!=null)
    {
      this._DelivaryyService.getdelivarybyid(this.applicationusridOfDeivary).subscribe({
        next: (data: any) =>
        {
          this.delivary=data.data;
          this._DelivaryyService.getallAllowedOrders().subscribe({
            next: (data) =>{
               this.orders=data.data;
               console.log(data.data);
            },

            error: (data) =>{
               console.log(data);
              }
          })
          this._DelivaryyService.getassiengdedorede(this.delivary.id).subscribe({
            next: (data) =>{
              console.log(data);
              this.TokenOrders=data.data;
            },
            error: (err) =>{ console.log(err);}

          })
        },
        error: (data: any) =>
        {
          console.log(data);
        }

      })
    }

   }
   assigTodelivary(OrderID:number)
   {
     this._DelivaryyService.assignOrderToDelivary(OrderID,this.delivary.id).subscribe({
      next:(data)=>{
        this.TokenOrders=data.data;
        this.orders=this.orders.filter((obj) => {
          return obj.id != this.TokenOrders.id;
        });
        console.log(data.data);
      },
      error:(err)=>{console.log(err);}
     });
   }

  }


