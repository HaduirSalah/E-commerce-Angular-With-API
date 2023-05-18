import { Component, OnInit } from '@angular/core';
import { LoginService } from '../Services/login.service';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.scss']
})
export class AccountComponent implements OnInit{
  UserJsonString:any
  UserJsonObj:any
  constructor(private loginService: LoginService) {

  }


  ngOnInit(): void {
      this.UserJsonString=JSON.stringify(this.loginService.currentUser.getValue())
      this.UserJsonObj=JSON.parse(this.UserJsonString);
  }


}
