import { Injectable, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { IAPIResult } from '../SharedClassesAndTypes/IAPIResult';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CategoryService implements OnInit {

  _url:string='http://localhost:5099/api/Category'
  constructor(private http:HttpClient) { }

  GetAllCategories():Observable<IAPIResult>{
    return this.http.get<IAPIResult>(this._url);
  }
  ngOnInit(): void {

  }
}
