import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IProduct } from '../SharedClassesAndTypes/IProduct';

@Injectable({
  providedIn: 'root'
})
export class AddNewProductService {
  constructor(private http:HttpClient) { }

  AddProduct(product:FormData):Observable<any>
  {
    return this.http.post<FormData>(`http://localhost:5099/api/Products`,product);
  }
}
