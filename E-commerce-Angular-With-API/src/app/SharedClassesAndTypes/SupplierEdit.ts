export class SupplierEdit{
  constructor(
    public id:number,
    public name:string,
    public ssn:string,
    public verifecationState:boolean,
    public totalSales:number,
    public accountNumber?:string){
  }

}
