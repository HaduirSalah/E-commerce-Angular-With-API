import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { DeleteProductService } from '../Services/delete-product.service';
import { ProductService } from '../Services/product.service';
import { IProduct } from '../SharedClassesAndTypes/IProduct';

@Component({
  selector: 'app-product-dashboard',
  templateUrl: './product-dashboard.component.html',
  styleUrls: ['./product-dashboard.component.scss']
})
export class ProductDashboardComponent {
  products: IProduct[] = [];
  ErrorMessage: string = '';
  Product!:IProduct;

  constructor(private router:Router,private deleteService:DeleteProductService,private productService: ProductService) {

  }

  ngOnInit(): void {
    this.productService.GetAllProducts().subscribe({
      next: data =>
        //console.log(data);
        this.products = data.data,
      error: err => this.ErrorMessage = err
    })
  }

  ConfirmDelete(productID:any){
    this.productService.GetProductByID(productID).subscribe({
      next:data=>this.Product=data.data,
      error:err=>this.ErrorMessage=err
    })
    console.log(productID)
    if (confirm("Are you sure you want to delete this product?")) {
      // user clicked "Yes"
      // do something, such as call an API to delete the item
      this.deleteService.DeleteProduct(this.Product).subscribe({
        next:data=>{
          console.log(data),
          alert("Product Deleted Successfully"),
          this.router.navigate(['/Dashboard/Products'])
        },
        error:err=>this.ErrorMessage=err
      })
    } else {
      // user clicked "No"
      // do nothing
      // this.router.navigate(['/Dashboard'])

    }
  }
}
