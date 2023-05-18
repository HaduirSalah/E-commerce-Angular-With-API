import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User_Login } from '../SharedClassesAndTypes/User_Login';
import { BehaviorSubject, Observable } from 'rxjs';
import jwtDecode from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  currentUser = new BehaviorSubject(null);
  IsLogged: boolean = false;

  Url: string = "http://localhost:5099/api/Account/login";

  constructor (private http: HttpClient) 
  {
    if (localStorage.getItem('UserToken') != null)
    {
      this.IsLogged = true
      this.saveCurrentUser()
    }
    else
    {
      this.IsLogged = false
    }
  }

  Login(user: User_Login): Observable<any> 
  {
    return this.http.post<any>(this.Url, user);
  }

  saveCurrentUser() 
  {
    let token: any = localStorage.getItem('UserToken');
    this.currentUser.next(jwtDecode(token));
  }

}
