import { Component, OnInit } from '@angular/core';
import { LoginService } from '../Services/login.service';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import jwtDecode from 'jwt-decode';
import { CartService } from '../Services/cart.service';
import { CustomerService } from '../Services/customer.service';
import { DelivaryyService } from '../Services/delivaryy.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit{
  IsLoging: boolean = false;
  totalItems:number=0;
  UserName:any
  UserId:any
  UserRole!:string;
  CustomerID:any;

  constructor(private loginService: LoginService,
              private router:Router,
              private cartService :CartService,
              private customerService :CustomerService,
              private delivaryyService:DelivaryyService){ }

  ngOnInit(): void
  {
    this.cartService.change_Count_of_cart.subscribe({
      next: (data:any)=>{
        console.log(data);
        this.totalItems+=data;
      }

    }),
    this.loginService.currentUser.subscribe({
      next:(data:any)=>
      { console.log("-------------------------------------+");
        console.log(data);
        if(data.Role.length>2)
        this.IsLoging=true;
        this.UserId=data.ID;
        this.UserName=data.Name;
        this.UserRole=data.Role;
        console.log("user 3333333333333333333"+this.UserId);
        console.log("user 33333333333333333"+this.UserName);
        console.log("user id3333333333333333e"+this.UserRole);
      }
    })

    if (this.UserId!=null&&this.UserRole=="Customer")    ///////////////////////////////////////////////////////////////
    {
     this.customerService.GetCustomerID(this.UserId).subscribe({
      next:data=>
      {
        this.CustomerID= data.data.id;
        console.log(data.data.id)
        console.log(this.CustomerID)
          this.cartService.GetProducts(this.CustomerID).subscribe({
            next:data=>
            {
              for (let i = 0; i < data.data.length; i++)
              {
                this.totalItems+=data.data[i].quantity
              }
            }
          })
      }
     })
    }
    else if (this.UserId!=null&&this.UserRole=="Delivery")
    {
      this.delivaryyService.getdelivarybyid(this.UserId).subscribe({
        next:(data)=>{
          console.log(data);
        },
        error:(err)=>{console.log(err);}

      })
    }
  }

  SignOut()
  {
    this.loginService.currentUser.next(null);
    localStorage.removeItem('UserToken');
    this.router.navigate(['/Login'])
    console.log("SignOut Done")
    this.IsLoging=false;
    this.cartService.change_Count_of_cart.next(0)
  }

}
