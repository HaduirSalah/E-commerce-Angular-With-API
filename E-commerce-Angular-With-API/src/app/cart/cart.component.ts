import { OrderService } from './../Services/order.service';
/*import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ICart } from '../SharedClassesAndTypes/ICart';
import { CartService } from '../Services/cart.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent {
  Products= this.cartService.GetProducts();
  Carts: ICart[] = [];

  ErrorMessage: string = '';

  constructor(private cartService:CartService, private router: Router) {}

  ngOnInit(): void {
    console.log(this.Products);
  }
}*/

import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ICart } from '../SharedClassesAndTypes/ICart';
import { CartService } from '../Services/cart.service';
import { IProduct } from '../SharedClassesAndTypes/IProduct';
import { LoginService } from '../Services/login.service';
import jwtDecode from 'jwt-decode';
import { CustomerService } from '../Services/customer.service';
import { IAPIResult } from '../SharedClassesAndTypes/IAPIResult';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent {
  Products:any[]=[] /*this.cartService.GetProducts();*/
  Carts: ICart[] = [];
  UserId:string='';
  SelectQuantity:number=1;
  ProductList: IProduct[] = [];
  public grandTotal!:number;
  ErrorMessage: string = '';
  CustomerID:number=0;
  crediteNumber:string='';
  paidMethod:string='';
  TotalPrice:number=0;
  orderId:number=0;



  constructor(private cartService:CartService,private loginService:LoginService, private router: Router,private customerService:CustomerService ,private orderservice:OrderService) {}

  ngOnInit(): void {
    this.loginService.currentUser.subscribe({
      next:(data:any)=>{
        var user=(JSON.parse(JSON.stringify((data))));
        this.UserId=user.ID;
        console.log(this.UserId);}});

    /*this.cartService.GetProducts().subscribe((res: string | any[])=>{
      this.Products=res;
      this.grandTotal=this.cartService.getTotalPrice()
    })*/
 this.customerService.GetCustomerID(this.UserId).subscribe({
  next:data=>{this.CustomerID=data.data.id;
  console.log("wwwww"+this.CustomerID)
  this.cartService.GetProducts(this.CustomerID).subscribe({

    next : data=> {
     this.Products=data.data
     console.log(this.Products);
     this.grandTotal=this.cartService.getTotalPrice()
    }
    ,error:err=>console.log(err)
  })

}
 })
 console.log("hamadaaa")


 this.cartService.GetProducts(this.CustomerID).subscribe({

      next : data=> {
       this.Products=data.data
       console.log(this.Products);
       this.grandTotal=this.cartService.getTotalPrice()
      }
      ,error:err=>console.log(err)
    })


  }

  PayProcess(){
    this.orderservice.GetOrders(this.CustomerID,this.crediteNumber,this.paidMethod,this.TotalPrice).subscribe({

      next : data=> {
       console.log(data.data)
       this.router.navigateByUrl("/Order/"+data.data.id)

       }
       ,error:err=>console.log(err)
    })

  }
  // GetTheTotalPrice(){

  // }
  tempPrice:any[]=[];
  tempObj:any[]=[];
  IncreaseTheValue(product:IProduct)
  {
    this.SelectQuantity=this.SelectQuantity+1;
    this.tempObj.push({id:product.id,price:product.price,quantity:this.SelectQuantity});
    this.tempPrice.push(product.price);
    // product.price=product.price+(this.tempPrice[0]* 1);
    product.price=product.price+(this.tempObj[0].price* 1);
    console.log("price "+product.price+" Select Value "+this.SelectQuantity);

  }

  DeleteOrder(CartID:number,OrderQuantity:number){
    this.cartService.DeleteCartProduct(CartID).subscribe({
      next:data=>{
        console.log(data)
        this.cartService.change_Count_of_cart.next(-OrderQuantity)
      },
      error:err=>console.log(err)
    });

    const elem = document.getElementById('cartCard');
    elem?.parentNode?.removeChild(elem);

  }
}
