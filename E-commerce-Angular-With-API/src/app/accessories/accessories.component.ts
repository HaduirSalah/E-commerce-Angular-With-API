import { Component, OnInit } from '@angular/core';
import { AccessoriesService } from '../Services/accessories.service';
import { CartService } from '../Services/cart.service';
import { IProduct } from '../SharedClassesAndTypes/IProduct';
@Component({
  selector: 'app-accessories',
  templateUrl: './accessories.component.html',
  styleUrls: ['./accessories.component.scss']
})
export class AccessoriesComponent {//implements OnInit {
  Products: IProduct[] = [];
  ErrorMessage: string = '';

  constructor(private accessoriesService: AccessoriesService,private cartService:CartService) {
  }


  ngOnInit(): void {
    this.accessoriesService.GetAllProducts().subscribe({
      next: data =>

        //console.log(data);
        this.Products = data.data,


      error: err => this.ErrorMessage = err


    })
  }
  addToCart(product:IProduct){
    this.cartService.AddToCart(product);
  }

}
