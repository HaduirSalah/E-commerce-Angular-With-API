/*import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IAPIResult } from '../SharedClassesAndTypes/IAPIResult';
import { IProduct } from '../SharedClassesAndTypes/IProduct';
import { ProductService } from './product.service';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  CartProducts : any[]=[];
  _UrlAPI:string='http://localhost:5099/api/Cart'

  constructor(private http:HttpClient) { }

  SetCart():Observable<IAPIResult>{
    return this.http.post<IAPIResult>(this._UrlAPI,ProductService);
  }

  AddToCart(Product:IProduct){
    this.CartProducts.push(Product);
    window.alert(this.CartProducts[0].id);
    window.alert(this.CartProducts[1].id);
    window.alert(this.CartProducts[2].id);
  }

  GetProducts(){
    return this.CartProducts
  }
}*/

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, Observable } from 'rxjs';
import { IAPIResult } from '../SharedClassesAndTypes/IAPIResult';
import { ICart } from '../SharedClassesAndTypes/ICart';
import { IProduct } from '../SharedClassesAndTypes/IProduct';
import { ProductService } from './product.service';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  public change_Count_of_cart=new BehaviorSubject(0);
 public CartProductsList : any=[];
 public productList= new BehaviorSubject<any>([])

  _UrlAPI:string='http://localhost:5099/api/Cart'

  constructor(private http:HttpClient) { }

  SetCart(product:any):Observable<IAPIResult>{
    return this.http.post<IAPIResult>(this._UrlAPI,product);
  }


  GetProducts(userid:number):Observable<any>{
    console.log("rrrrrrrrrrrrrrrrrrrr"+userid)
   /* console.log("cart prodlist"+(JSON.s4tringify (this.productList.asObservable())))*/
   return this.http.get(`${this._UrlAPI}/AllByuserID/${userid}`).pipe(  catchError((err) => { return err;}) );
  }

  DeleteCartProduct(CartId:number):Observable<any>{
    return this.http.delete(`http://localhost:5099/api/Cart/${CartId}`).pipe(  catchError((err) => { return err;}) );
  }




  setProduct(product:any){
    this.CartProductsList.push(...product);
    this.productList.next(product);
  }

  public AddToCartTest(product:IProduct,_customerID:number):Observable<any>{
    // this.CartProductsList.push(product);
    // this.productList.next(this.CartProductsList);
    // this.getTotalPrice()
    // console.log(this.CartProductsList);
    console.log("data form servese as function "+_customerID) ;
   return this.http.post(this._UrlAPI, { id: 0,
    customerID: _customerID,
    productID: product.id,
    quantity: product.quantity,
    totalPrice: product.price}).pipe(catchError((err) => { return err;}) );

}
public AddToCart(product:IProduct){
  // this.CartProductsList.push(product);
  // this.productList.next(this.CartProductsList);
  // this.getTotalPrice()
  // console.log(this.CartProductsList);

  // this.http.post(this._UrlAPI, { id: 0,
  // customerID: _customerID,
  // productID: product.id,
  // quantity: product.quantity,
  // totalPrice: product.price});

}





  getTotalPrice() :number{
    let grandTotal=0;
    this.CartProductsList.map((a:any)=>{
      grandTotal +=a.total  ;
    })
    return grandTotal;
  }
}
