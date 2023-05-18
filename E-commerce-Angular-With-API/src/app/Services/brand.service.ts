import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IAPIResult } from '../SharedClassesAndTypes/IAPIResult';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BrandService {

  _url:string='http://localhost:5099/api/Brand'
  constructor(private http:HttpClient) { }

  GetAllBrands():Observable<IAPIResult>{
    return this.http.get<IAPIResult>(this._url);
  }
}
