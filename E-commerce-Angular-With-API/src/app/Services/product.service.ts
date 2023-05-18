import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IAPIResult } from '../SharedClassesAndTypes/IAPIResult';
import { Observable, catchError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  _UrlAPI:string='http://localhost:5099/api/Products'
  _CategoriesUrl:string='http://localhost:5099/api/Category'
  constructor(private http:HttpClient) { }

  GetAllProducts():Observable<IAPIResult>{
    return this.http.get<IAPIResult>(this._UrlAPI);
  }

  GetAllCategories():Observable<IAPIResult>{
    return this.http.get<IAPIResult>(this._CategoriesUrl);
  }

  GetProductByID(id:number):Observable<IAPIResult>
  {
    return this.http.get<IAPIResult>(`http://localhost:5099/api/Products/${id}`);
  }

  GetProductByCategory(category:string):Observable<IAPIResult>
  {
    return this.http.get<IAPIResult>(`http://localhost:5099/api/Products/${category}`);
  }
  GetProductByPage(page:number):Observable<any>
  {
    return this.http.get<any>(`http://localhost:5099/api/Products/ByPage/${page}`).pipe(catchError((err) => { return err;})
    );
  }

}
