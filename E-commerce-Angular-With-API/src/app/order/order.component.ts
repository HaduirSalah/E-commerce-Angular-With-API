import { ActivatedRoute } from '@angular/router';
import { OrderDetailsService } from './../Services/order-details.service';
import { HttpClient } from '@angular/common/http';
import { CustomerService } from './../Services/customer.service';
import { CartService } from './../Services/cart.service';
import { IProduct } from './../SharedClassesAndTypes/IProduct';
import { Component } from '@angular/core';
import { OrderService } from '../Services/order.service';
import { IOrder } from '../SharedClassesAndTypes/IOrder';
import { LoginService } from '../Services/login.service';
import { IAPIResult } from '../SharedClassesAndTypes/IAPIResult';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.scss']
})
export class OrderComponent {

  OrdersDetails: any[] = [];
  Orders: IOrder[] = [];
  Products: any[] = [];
  UserId: string = '';
  CustomerID: number = 0;
  grandTotal: number = 0;
  TotalPrice: number = 0;
  OrderId: number = 0;
  UserName: string = '';
  constructor(private activerouter: ActivatedRoute, private orderdetails: OrderDetailsService, private orderService: OrderService, private cartservice: CartService, private loginservice: LoginService, private customerService: CustomerService, private cartService: CartService, private HttpClient: HttpClient) {

  }

  ngOnInit(): void {
    this.activerouter.paramMap.subscribe(
      (e: any) => { this.OrderId = e.get("id") }
    )
    this.loginservice.currentUser.subscribe({
      next: (data: any) => {
        var user = (JSON.parse(JSON.stringify((data))));
        console.log("*********KKKKKKKKKKKKKK**********")
        console.log(user.Name);
        this.UserName=user.Name;
        this.UserId = user.ID;
        console.log(this.UserId);
      }
    });





    this.customerService.GetCustomerID(this.UserId).subscribe({
      next: data => {
        this.CustomerID = data.data.id;


        this.orderService.GetOrdersByCustomerId(this.CustomerID).subscribe({

          next: data => {
            this.Orders = data.data
            console.log("ya Rab Nkhales" + this.CustomerID);
            console.log(data)

          }
          , error: err => console.log(err)
        })


        console.log("wwwww" + this.CustomerID)
        this.cartService.GetProducts(this.CustomerID).subscribe({

          next: data => {
            this.Products = data.data
            console.log(this.Products);
            this.grandTotal = this.cartService.getTotalPrice()
          }
          , error: err => console.log(err)
        })

      }
    })
    console.log("hamadaaa")




    this.orderService.GetOrdersById(this.OrderId).subscribe({

      next: data => {

        this.Orders = data.data
        console.log(this.Orders);

      }
      , error: err => console.log(err)
    })







  }
}
