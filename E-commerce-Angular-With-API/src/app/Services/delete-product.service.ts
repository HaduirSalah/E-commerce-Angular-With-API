import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IProduct } from '../SharedClassesAndTypes/IProduct';

@Injectable({
  providedIn: 'root'
})
export class DeleteProductService {

  constructor(private http:HttpClient) { }

  DeleteProduct(product:IProduct):Observable<IProduct>
  {
    return this.http.delete<IProduct>(`http://localhost:5099/api/Products/${product.id}`);
  }
}
