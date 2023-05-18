export interface IOrder{
  id:number,
  date:Date,
  totalPrice:number,
  paidMethod:string,
  creditNumber:string,
  orderState:string,
  customerID:number,
  deliveryID:number
}
