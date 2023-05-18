import { Injectable } from '@angular/core';
import { IOffer } from '../SharedClassesAndTypes/IOffer';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DeleteOfferService {

  constructor(private http:HttpClient) { }

  DeleteOffer(offer:IOffer):Observable<IOffer>
  {
    return this.http.delete<IOffer>(`http://localhost:5099/api/Offers/${offer.id}`);
  }
}
