import { AbstractControl, FormControl } from "@angular/forms";

export function ForbiddenNameValidator(forbiddenName:RegExp)
{
  return (control:AbstractControl)=>{
    const forbidden= forbiddenName.test(control.value);
    return forbidden ? {'forbiddenName':{value:control.value}} :null;
  }
}
