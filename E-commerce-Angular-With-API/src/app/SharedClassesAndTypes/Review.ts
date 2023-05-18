export class Review{
  constructor(
    public id:number=0,
    public productID:number,
    public reviewText:string,
    public date:Date,
    public customerId:number,
    ){

  }
}
