import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DelivaryyService {

  constructor(private http:HttpClient) { }

  getdelivarybyid(ApplidicationUserID:string):Observable<any>
  {
    return this.http.get(`http://localhost:5099/api/Delivery/GetDelivaryByID/${ApplidicationUserID}`).pipe(catchError((err) => { return err;}) );
  }      
  getallAllowedOrders():Observable<any>
  {
    return this.http.get('http://localhost:5099/api/Order/waiting_to_delivred').pipe(catchError((err) => { return err;}) );
  }         
  assignOrderToDelivary(orderid:number,delivaryId:number):Observable<any>
  {
    return this.http.get(`http://localhost:5099/api/Order/ASSigined_to_delivred/${orderid}/${delivaryId}`).pipe(catchError((err) => { return err;}) );
  }
  getassiengdedorede(delivaryid:number):Observable<any>
  {
    return this.http.get(`http://localhost:5099/api/Order/OrderByDelivary/${delivaryid}`).pipe(catchError((err) => { return err;}) );;
  }
}
