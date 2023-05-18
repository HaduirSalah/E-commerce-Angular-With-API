import { Injectable } from '@angular/core';
import { IReview } from '../SharedClassesAndTypes/IReview';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IAPIResult } from '../SharedClassesAndTypes/IAPIResult';

@Injectable({
  providedIn: 'root'
})
export class ReviewService {

  constructor(private http:HttpClient) { }

  AddReview(review:IReview):Observable<IReview>
  {
    return this.http.post<IReview>(`http://localhost:5099/api/Review`,review);
  }

  GetReviews(productId:number):Observable<IAPIResult>
  {
    return this.http.get<IAPIResult>(`http://localhost:5099/api/Review/Product/${productId}`);
  }
}
