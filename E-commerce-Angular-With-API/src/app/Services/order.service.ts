import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, catchError } from 'rxjs';
import { IAPIResult } from '../SharedClassesAndTypes/IAPIResult';

@Injectable({
  providedIn: 'root'
})
export class OrderService {


  constructor(private http:HttpClient) { }

  GetOrders(Id:number,creditNumber:string,PayMethod:string,TotalPrice:number):Observable<IAPIResult>
  {
    console.log("1111111111111111")
    return this.http.post<IAPIResult>("http://localhost:5099/api/Order",
    {customerID:Id,crediteNumber:creditNumber,paidMethod:PayMethod,totalPrice:TotalPrice ,  date: "2023-05-05T13:30:33.259Z"  }
    );

  }


  GetOrdersById(Id:number):Observable<IAPIResult>
{

  return this.http.get<IAPIResult>(`http://localhost:5099/api/Order/${Id}`);

}

GetOrdersByCustomerId(Id:number):Observable<any>
{

  return this.http.get<any>(`http://localhost:5099/api/Order/OrderByCustomer/${Id}`).pipe(catchError((err) => { return err;}));

}

}
