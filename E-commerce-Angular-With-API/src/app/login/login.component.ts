import { Component, OnInit } from '@angular/core';
import { User_Login } from '../SharedClassesAndTypes/User_Login';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LoginService } from '../Services/login.service';
import { Router } from '@angular/router';
import jwtDecode from 'jwt-decode';
import { CustomerService } from '../Services/customer.service';
import { ICustomer } from '../SharedClassesAndTypes/ICustomer';
import { Customer } from '../SharedClassesAndTypes/Customer';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginForm!: FormGroup;
  customerModel = new Customer(0, '', 0)
  customerApplicationID: string = ''


  constructor(private customerService: CustomerService,
              private fb: FormBuilder,
              private loginService: LoginService,
               private router: Router)
             { }

  ngOnInit() {
    this.loginForm = this.fb.group({
      userName: this.fb.control('', [Validators.required]),
      password: this.fb.control('', [Validators.required]),
    });
  }

  get userName() { return this.loginForm.get('userName');}
  get password() { return this.loginForm.get('password');}

  // Node JS And Template Dirven Forms
  submitData()
  {
    this.loginService.Login(this.loginForm.value).subscribe({
      next: data =>
      {
        if (data.message == 'Success')
        {
          localStorage.setItem('UserToken', data.token);
          this.loginService.saveCurrentUser();
          const user:any = jwtDecode(data.token);
          if (user.Role=="Delivery")
          {
            this.router.navigate([`/Delivary/${user.ID}`]);
          }
          else if (user.Role=="Customer")
          {
            this.router.navigate(['/Home']);
          }
          else if (user.Role=="Admin")
          {
            this.router.navigate(['/Dashboard']);
            console.log("Admin")
          }


        }
      },
      error: error => console.log(error)
    });
  }
}
