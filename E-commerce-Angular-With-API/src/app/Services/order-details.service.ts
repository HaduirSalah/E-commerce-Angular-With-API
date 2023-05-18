import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IAPIResult } from '../SharedClassesAndTypes/IAPIResult';

@Injectable({
  providedIn: 'root'
})
export class OrderDetailsService {

  constructor(private http:HttpClient) { }

  GetOrdersByOrderId(Id:number):Observable<IAPIResult>
{

  return this.http.get<IAPIResult>(`http://localhost:5099/api/OrderDetails/OrderByOrderId/${Id}`);

}
}
