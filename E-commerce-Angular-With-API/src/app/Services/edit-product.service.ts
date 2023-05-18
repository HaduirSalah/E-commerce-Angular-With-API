import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IProduct } from '../SharedClassesAndTypes/IProduct';

@Injectable({
  providedIn: 'root'
})
export class EditProductService {

  constructor(private http:HttpClient) { }

  EditProduct(product:IProduct):Observable<IProduct>
  {
    return this.http.put<IProduct>(`http://localhost:5099/api/Products/${product.id}`,product);
  }
}
