import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IAPIResult } from '../SharedClassesAndTypes/IAPIResult';

@Injectable({
  providedIn: 'root'
})
export class OfferService {

  constructor(private http:HttpClient) { }

  GetAllOffers():Observable<IAPIResult>
  {
    return this.http.get<IAPIResult>("http://localhost:5099/api/Offers");
  }

  GetOfferByID(id:number):Observable<IAPIResult>
  {
    return this.http.get<IAPIResult>(`http://localhost:5099/api/Offers/${id}`);
  }
}
