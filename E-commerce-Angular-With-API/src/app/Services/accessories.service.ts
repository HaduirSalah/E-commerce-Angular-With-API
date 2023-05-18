import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IAPIResult } from '../SharedClassesAndTypes/IAPIResult';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class AccessoriesService {

  _UrlAPI:string='http://localhost:5099/api/Products/accessories'
  constructor(private http:HttpClient) { }

  GetAllProducts():Observable<IAPIResult>{
    return this.http.get<IAPIResult>(this._UrlAPI);
  }
}
