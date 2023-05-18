import { Component, OnInit } from '@angular/core';
import { IProduct } from '../SharedClassesAndTypes/IProduct';
import { ProductService } from '../Services/product.service';
import { DeleteProductService } from '../Services/delete-product.service';
import { Router } from '@angular/router';
import { SupplierService } from '../Services/supplier.service';
import { ISupplier } from '../SharedClassesAndTypes/ISupplier';
import { OfferService } from '../Services/offer.service';
import { IOffer } from '../SharedClassesAndTypes/IOffer';


@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  products: IProduct[] = [];
  suppliers: ISupplier[] = [];
  offers: IOffer[] = [];
  ErrorMessage: string = '';
  constructor(private router:Router,private supplierService: SupplierService,private productService: ProductService,private offerService:OfferService){

  }
  ngOnInit(): void {
    this.productService.GetAllProducts().subscribe({
      next: data =>
        //console.log(data);
        this.products = data.data,
      error: err => this.ErrorMessage = err
    }),
    this.supplierService.GetAllSuppliers().subscribe({
      next: data =>
        //console.log(data);
        this.suppliers = data.data,
      error: err => this.ErrorMessage = err
    }),
    this.offerService.GetAllOffers().subscribe({
      next: data =>
        //console.log(data);
        this.offers = data.data,
      error: err => this.ErrorMessage = err
    })
  }

}
