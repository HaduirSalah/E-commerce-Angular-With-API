import { Injectable } from '@angular/core';
import { IOffer } from '../SharedClassesAndTypes/IOffer';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AddOfferService {

  constructor(private http:HttpClient) { }

  AddOffer(offer:IOffer):Observable<IOffer>
  {
    return this.http.post<IOffer>(`http://localhost:5099/api/Offers`,offer);
  }
}
