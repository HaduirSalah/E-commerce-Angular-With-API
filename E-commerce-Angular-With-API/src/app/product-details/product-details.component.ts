import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { ProductService } from '../Services/product.service';
import { IProduct } from '../SharedClassesAndTypes/IProduct';
import { CartService } from '../Services/cart.service';
import { ReviewService } from '../Services/review.service';
import { IReview } from '../SharedClassesAndTypes/IReview';
import { FormBuilder, FormGroup } from '@angular/forms';
import { LoginService } from '../Services/login.service';
import { AddReview } from '../SharedClassesAndTypes/AddReview';
import { CustomerService } from '../Services/customer.service';
import { ICart } from '../SharedClassesAndTypes/ICart';
import { Review } from '../SharedClassesAndTypes/Review';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  productId: any;
  Product!: IProduct;
  productTemp!:IProduct;
  ErrorMessage: string = "";
  ReviewObj=new Review(0,0,'',new Date(),0)
  ReviewsByProductId:any[]=[];
  result:IReview[]=[];
  ApplicationUserID:string='';
  AppUser:any=this.loginService.currentUser.getValue();
  AppUserID:string='';
  customerID!:number;
  customerIdetifier!:number;
  ReviewText:string='';
  Products: IProduct[] = [];
  SelectQuantity: number=1;

  tempPrice:any[]=[];
  constructor(private activatedRoute: ActivatedRoute, private productsService: ProductService,
    private cartService: CartService, private reviewServise: ReviewService
    , private loginService: LoginService,private customerService:CustomerService)
    {

     }
  ngOnInit(): void {
      this.AppUserID=this.AppUser.ID
      this.activatedRoute.paramMap.subscribe((params: ParamMap) => {
        this.productId = params.get("id");
      })

      this.customerService.GetCustomerID(this.AppUserID).subscribe({
        next : data=>{
          this.customerID=data.data.id;
          console.log(this.customerID)
        }
      })

      this.productsService.GetProductByID(this.productId).subscribe({
        next: (data) => {
          console.log(data.data)
          this.Product = data.data;
          this.tempPrice.push(this.Product.price);
        },
        error: (erorr: string) => this.ErrorMessage = erorr
      }),
      this.reviewServise.GetReviews(this.productId).subscribe({
        next:data=>{
          this.ReviewsByProductId=data.data
        }
      }),
      this.Products.forEach((a:IProduct) => {
        Object.assign(a,{quantity: 1,total:a.price})
      });
  }

  addToCart(product:IProduct){

     product.quantity=this.SelectQuantity;
     product.price=product.quantity*this.Product.price;
    this.cartService.AddToCartTest(product,this.customerID).subscribe(
      {
        next:(data)=>{
          console.log("succses");
          this.cartService.change_Count_of_cart.next(product.quantity)
      }
        ,
        error:()=>{console.log("errorro form APi");}

      }
    )
    ;
    console.log("customerID from  ADDTO CATert in ------"+this.customerID);
  }

  AddNewReview() {
    this.ReviewObj.id=0,
    this.ReviewObj.productID=this.productId,
    this.ReviewObj.customerId=this.customerID,
    this.ReviewObj.date=new Date(),
    this.ReviewObj.reviewText=this.ReviewText
    console.log(this.ReviewObj)
    this.reviewServise.AddReview(this.ReviewObj).subscribe({
      next:data=>{
        console.log(data)
        console.log("/////////////////////////////////////")
        console.log(this.ReviewObj)
      }
    });
  }


 // tempObj:any[]=[];
  IncreaseTheValue(product:IProduct)
  {
    this.SelectQuantity=this.SelectQuantity+1;
   // this.tempObj.push({id:product.id,price:product.price,quantity:this.SelectQuantity});

    //product.price=product.price+(this.tempObj[0].price* 1);
    console.log("price "+product.price+" Select Value "+this.SelectQuantity);

  }
}
