export class ProductEdit {
  constructor(
    public id:number,
    public name:string,
    public description:string,
    public imageName:string,
    public ImageOfProduct:File,
    public price:number,
    public quantity:number,
    public categoryID:number,
    public offerID:number,
    public supplierID:number,
    public brandID:number
    ){
  }

}
