import { Injectable } from '@angular/core';
import { ISupplier } from '../SharedClassesAndTypes/ISupplier';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EditSupplierService {

  constructor(private http:HttpClient) { }

  EditSupplier(supplier:ISupplier):Observable<ISupplier>
  {
    return this.http.put<ISupplier>(`http://localhost:5099/api/Supplier/${supplier.id}`,supplier);
  }
}
