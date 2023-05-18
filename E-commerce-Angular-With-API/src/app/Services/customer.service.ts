import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable } from 'rxjs';
import { IAPIResult } from '../SharedClassesAndTypes/IAPIResult';
import { ICustomer } from '../SharedClassesAndTypes/ICustomer';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  constructor(private http:HttpClient) { }

  GetCustomerByID(id:number):Observable<IAPIResult>
  {
    return this.http.get<IAPIResult>(`http://localhost:5099/api/Customer/${id}`);
  }

  GetCustomerID(Appid:string):Observable<any>
  {
    return this.http.get<IAPIResult>(`http://localhost:5099/api/Customer/${Appid}`).pipe(catchError((err) => { return err;}) );
  }

  AddCustomer(customer:ICustomer):Observable<ICustomer>
  { 
    return this.http.post<ICustomer>(`http://localhost:5099/api/Customer`,customer);
  }

}
