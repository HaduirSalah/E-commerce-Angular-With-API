import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ConfirmPasswordValidator } from '../validations/confirmPassword.validators';
import { User_Register } from '../SharedClassesAndTypes/User_Register';
import { RegisterService } from '../Services/register.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  registerForm!: FormGroup;
  roleslist!: any[];  // Options = ['Facebook', 'Twitter', 'Google'];
  // registerModel = new User_Register('', '', '');
  constructor(
    private registerService: RegisterService, private fb: FormBuilder, private router: Router)
    { }

  ngOnInit() {
    this.registerService.GetRoles().subscribe({
      next: (data)=>{
               this.roleslist=data.data;
      },
      error: (data)=>{console.log(data);}
      
    })
    this.registerForm = this.fb.group(
      {
        userName: this.fb.control(
          '',
          [
            Validators.required,
            Validators.pattern('[a-z A-Z]+'),
            Validators.minLength(3),
            Validators.maxLength(50),
          ],
        ),
        email: this.fb.control('', [Validators.required, Validators.email]),
        password: this.fb.control(
          '',
          [
            Validators.required,
            Validators.pattern(
              '^(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{6,}$'
            ),
          ],
        ),
        confirmPassword: this.fb.control(
          '',
          [
            Validators.required,
          ],
        ),
        NameOfRoleOFUserID:this.fb.control(
          '',
          [
            Validators.required,
          ],
        ),
        SSN: this.fb.control('' ),

       
        
        AccountNumber: this.fb.control('' )
        
       }, {
      validator: [ConfirmPasswordValidator]
    }
    );

  }

  get userName() {
    return this.registerForm.get('userName');
  }
  get email() {
    return this.registerForm.get('email');
  }
  get password() {
    return this.registerForm.get('password');
  }
  get confirmPassword() {
    return this.registerForm.get('confirmPassword');
  }
  get NameOfRoleOFUserID() {
    return this.registerForm.get('NameOfRoleOFUserID');
  }


  //#region Delivery
  get SSN() {
    return this.registerForm.get('SSN');
  }

  get AccountNumber() {
    return this.registerForm.get('AccountNumber');
  }
  //#endregion
  
  
  
  
  
  // get option() {
  //   return this.registerForm.get('option');
  // }

  // Node JS And Template Dirven Forms
  submitData() {
    //component ===> service
    //service==>http call
    console.log(this.registerForm);
    this.registerService.Register(this.registerForm.value).subscribe({
      next: data => {
        console.log(data);
        this.router.navigate(['/Login']);
      },
      error: error => console.log("errorrrrorrr"+error)
    });
  }
  makechange()
  {
    if (this.NameOfRoleOFUserID?.value=="Delivery")
    {  
       this.SSN?.setValidators(Validators.required);
       this.AccountNumber?.setValidators(Validators.required);
      console.log("delevary");
    }
    else
    {
       this.SSN?.clearValidators();
       this.AccountNumber?.clearValidators();
       this.SSN?.patchValue("");
       this.AccountNumber?.patchValue("");
      console.log(this.NameOfRoleOFUserID?.value);
      console.log("Normalcautomer");

    }
  }
}
