import { Component } from '@angular/core';
import { IProduct } from '../SharedClassesAndTypes/IProduct';
import { ProductService } from '../Services/product.service';
import { Router } from '@angular/router';
import { ICategory } from '../SharedClassesAndTypes/ICategory';
import { CartService } from '../Services/cart.service';
import { ICart } from '../SharedClassesAndTypes/ICart';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss']
})
export class ProductComponent {
  Products: IProduct[] = [];
  ErrorMessage: string = '';
  Categories: ICategory[] = [];
  loading: boolean = false;
  page:number=1;
  count:number=0;
  productSize:number=5;
  indexofPages: number=1;
  countOfPage:number=0;
  TempArray:any[]=[];

  constructor(private productService: ProductService, private router: Router,private cartService:CartService) {
  }

  ngOnInit(): void {
    this.GetAllProducts()
    this.GetAllCategories()
    this.getpages(1);
  }

  GetAllProducts() {
    this.loading = true;
    this.productService.GetAllProducts().subscribe({
      next: data => {
        this.Products = data.data
        this.loading = false
      },
      error: err => {
        this.ErrorMessage = err
        this.loading = false
      }
    })
  }
  GetAllCategories() {
    this.loading = true;
    this.productService.GetAllCategories().subscribe({
      next: data => {
        this.Categories = data.data
        this.loading = false
      },
      error: err => {
        this.ErrorMessage = err
        this.loading = false
      }
    })
  }
  GetProductsWithCategory(category: string) {
    this.loading = true;
    this.productService.GetProductByCategory(category).subscribe({
      next: data => {
        this.Products = data.data
        this.loading = false
      }
      ,
      error: err => {
        this.ErrorMessage = err
        this.loading = false
      }
    })
  }
  FilterProducts(event: any) {
    this.loading = true;
    if (event.target.value == 'all') {
      this.GetAllProducts()
      this.loading = false
    }
    else {
      this.GetProductsWithCategory(event.target.value)
      this.loading = false
    }
  }
  addToCart(product:IProduct){

    this.cartService.AddToCart(product);


  }
  getpages(num:number)
  {
    this.productService.GetProductByPage(num).subscribe({
      next: (data)=>{
        this.Products = data.items;
        this.countOfPage=data.count;
        this.TempArray= new Array(this.countOfPage);
        console.log(data)
      }
      ,
      error: (error)=>{console.log(error); }
      
    });
    this.indexofPages=num;
    console.log("getpages"+this.indexofPages);
  }
  gotleft()
  {
    (this.indexofPages>1)?this.indexofPages-=1:this.indexofPages=1;
    this.getpages(this.indexofPages);
    console.log("gotleft"+this.indexofPages);
  }
  gotoright()
  {
    (this.indexofPages<this.countOfPage+1)?this.indexofPages+=1:this.indexofPages=this.countOfPage+1;
    this.getpages(this.indexofPages);
    console.log("gotoright"+this.indexofPages);
  }

}
