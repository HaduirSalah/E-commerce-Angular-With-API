import { Component, OnInit } from '@angular/core';
import { DeleteProductService } from '../Services/delete-product.service';
import { ProductService } from '../Services/product.service';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';

@Component({
  selector: 'app-delete-product',
  templateUrl: './delete-product.component.html',
  styleUrls: ['./delete-product.component.scss']
})
export class DeleteProductComponent implements OnInit {
  productId: any;
  Product: any;
  ErrorMessage: string = "";

  constructor(private router:Router ,private activatedRoute: ActivatedRoute, private deleteProduct: DeleteProductService, private productsService: ProductService) { }

  ngOnInit(): void {
    this.activatedRoute.paramMap.subscribe((params: ParamMap) => {
      this.productId = params.get("id")
    }),
      this.productsService.GetProductByID(this.productId).subscribe({
        next: (data) => this.Product = data.data,
        error: (erorr: string) => this.ErrorMessage = erorr
      })
  }

  confirmDelete() {
    if (confirm("Are you sure you want to delete this item?")) {
      // user clicked "Yes"
      // do something, such as call an API to delete the item
      this.deleteProduct.DeleteProduct(this.Product).subscribe({
        next:data=>{
          console.log(data)
          alert("Deleted")
        },
        error:err=>this.ErrorMessage=err
      })
    } else {
      // user clicked "No"
      // do nothing
      this.router.navigate(['/Dashboard'])

    }
  }

}
