import { ICustomer } from "./ICustomer";

export class Customer implements ICustomer {
  constructor(
    public id: number = 0,
    public applicationUserId: string,
    public totalPoint: number = 0
  ) { }
}
