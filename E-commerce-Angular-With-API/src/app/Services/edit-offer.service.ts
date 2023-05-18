import { Injectable } from '@angular/core';
import { IOffer } from '../SharedClassesAndTypes/IOffer';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EditOfferService {

  constructor(private http:HttpClient) { }

  EditOffer(offer:IOffer):Observable<IOffer>
  {
    return this.http.put<IOffer>(`http://localhost:5099/api/Offers/${offer.id}`,offer);
  }
}
