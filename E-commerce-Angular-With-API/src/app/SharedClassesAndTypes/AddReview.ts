export class AddReview
{
  constructor(
    // public id:number,
    public productID:number,
    public reviewText:string,
    public date:Date,
    public customerId:number
  ){}
}
