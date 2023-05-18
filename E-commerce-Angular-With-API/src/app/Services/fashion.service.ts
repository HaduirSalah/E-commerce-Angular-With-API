import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IAPIResult } from '../SharedClassesAndTypes/IAPIResult';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FashionService {

  _UrlAPI:string='http://localhost:5099/api/Products/fashion'
  constructor(private http:HttpClient) { }

  GetAllProducts():Observable<IAPIResult>{
    return this.http.get<IAPIResult>(this._UrlAPI);
  }
}
