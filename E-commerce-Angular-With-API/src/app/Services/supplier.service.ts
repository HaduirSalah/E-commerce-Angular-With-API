import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IAPIResult } from '../SharedClassesAndTypes/IAPIResult';

@Injectable({
  providedIn: 'root'
})
export class SupplierService {

  constructor(private http:HttpClient) { }

  GetAllSuppliers():Observable<IAPIResult>
  {
    return this.http.get<IAPIResult>("http://localhost:5099/api/Supplier");
  }

  GetSupplierByID(id:number):Observable<IAPIResult>
  {
    return this.http.get<IAPIResult>(`http://localhost:5099/api/Supplier/${id}`);
  }
}
