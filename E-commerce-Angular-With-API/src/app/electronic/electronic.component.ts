import { Component } from '@angular/core';
import { IProduct } from '../SharedClassesAndTypes/IProduct';
import { ElectronicService } from '../Services/electronic.service';
import { CartService } from '../Services/cart.service';

@Component({
  selector: 'app-electronic',
  templateUrl: './electronic.component.html',
  styleUrls: ['./electronic.component.scss']
})
export class ElectronicComponent {
  Products:IProduct[]=[];
  ErrorMessage:string='';

  constructor (  private electronicService:ElectronicService,private cartService:CartService)
  {

  }

    ngOnInit(): void {
      this.electronicService.GetAllProducts().subscribe({
        next: data=>
          this.Products=data.data,
          //console.log(data);

        error: err=>this.ErrorMessage=err


      })
  }
  addToCart(product:IProduct){
    this.cartService.AddToCart(product);
  }
}
