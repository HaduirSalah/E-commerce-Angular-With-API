import { Injectable } from '@angular/core';
import { ISupplier } from '../SharedClassesAndTypes/ISupplier';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DeleteSupplierService {

  constructor(private http:HttpClient) { }

  DeleteSupplier(supplier:ISupplier):Observable<ISupplier>
  {
    return this.http.delete<ISupplier>(`http://localhost:5099/api/Supplier/${supplier.id}`);
  }
}
