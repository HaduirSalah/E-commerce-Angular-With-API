import { Injectable } from '@angular/core';
import { ISupplier } from '../SharedClassesAndTypes/ISupplier';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AddSupplierService {

  constructor(private http:HttpClient) { }

  AddSupplier(supplier:ISupplier):Observable<ISupplier>
  {
    return this.http.post<ISupplier>(`http://localhost:5099/api/Supplier`,supplier);
  }
}
