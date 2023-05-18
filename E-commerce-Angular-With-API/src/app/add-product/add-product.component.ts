import { Component } from '@angular/core';
import { AddNewProductService } from '../Services/add-new-product.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IProduct } from '../SharedClassesAndTypes/IProduct';
import { Router } from '@angular/router';
import { Product } from '../SharedClassesAndTypes/Product';
import { ICategory } from '../SharedClassesAndTypes/ICategory';
import { CategoryService } from '../Services/category.service';
import { IBrand } from '../SharedClassesAndTypes/IBrand';
import { BrandService } from '../Services/brand.service';
import { OfferService } from '../Services/offer.service';
import { IOffer } from '../SharedClassesAndTypes/IOffer';
import { ISupplier } from '../SharedClassesAndTypes/ISupplier';
import { SupplierService } from '../Services/supplier.service';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.scss']
})
export class AddProductComponent {
  productForm!: FormGroup;
  Categories: ICategory[] = []
  Offers: IOffer[] = []
  Brands: IBrand[] = []
  Suppliers: ISupplier[] = []
  ErrorMessage: string = '';
  base64: any;
  constructor(private supplierService:SupplierService,private offerService: OfferService, private brandService: BrandService, private categoryService: CategoryService, private addNewProductService: AddNewProductService, private fb: FormBuilder, private router: Router) {

  }

  ngOnInit() {
    this.productForm = this.fb.group({
      name: this.fb.control( '',[Validators.required,
                                 Validators.pattern('[a-z A-Z 0-9]+'),
                                 Validators.minLength(3),
                                 Validators.maxLength(50),
                                ]),
      description: this.fb.control('',[Validators.required,
                                       Validators.minLength(10),
                                       Validators.maxLength(500)
                                      ]),
      price: this.fb.control('',[ Validators.required ]),
      quantity: this.fb.control('', [Validators.required]),
      categoryID: this.fb.control('', [ Validators.required]),
      brandID: this.fb.control('',[Validators.required]),
      offerID: this.fb.control('',[Validators.required]),
      supplierID: this.fb.control('',[ Validators.required]),
      ImageOfProduct: this.fb.control(null,[ Validators.required])
    }),

    this.categoryService.GetAllCategories().subscribe({
      next: data =>
        //console.log(data);
        this.Categories = data.data,
      error: err => this.ErrorMessage = err
    });
    this.brandService.GetAllBrands().subscribe({
      next: data =>
        //console.log(data);
        this.Brands = data.data,
      error: err => this.ErrorMessage = err
    });
    this.offerService.GetAllOffers().subscribe({
      next: data =>
        //console.log(data);
        this.Offers = data.data,
      error: err => this.ErrorMessage = err
    });
    this.supplierService.GetAllSuppliers().subscribe({
      next: data =>
        //console.log(data);
        this.Suppliers = data.data,
      error: err => this.ErrorMessage = err
    });
  }
//#region
  get name() {
    return this.productForm.get('name');
  }
  get description() {
    return this.productForm.get('description');
  }
  get price() {
    return this.productForm.get('price');
  }
  get quantity() {
    return this.productForm.get('quantity');
  }

  get categoryID() {
    return this.productForm.get('categoryID');
  }
  get brandID() {
    return this.productForm.get('brandID');
  }
  get offerID() {
    return this.productForm.get('offerID');
  }
  get supplierID() {
    return this.productForm.get('supplierID');
  }
  get imageOfProduct() {
    return this.productForm.get('ImageOfProduct');
  }
 //#endregion

 submitData() {

   if (this.productForm.valid)
   {
    const Formdata = new FormData();
    Formdata.append('ImageOfProduct', this.imageOfProduct?.value, 'pic.jpg');
    Formdata.append('name', this.name?.value);
    Formdata.append('description', this.description?.value);
    Formdata.append('price', this.price?.value);
    Formdata.append('quantity', this.quantity?.value);
    // Formdata.append('imageName', this.productForm.get('imageName')?.value);
    Formdata.append('categoryID', this.categoryID?.value);
    Formdata.append('brandID', this.brandID?.value);
    Formdata.append('offerID', this.offerID?.value);
    Formdata.append('supplierID', this.supplierID?.value);

    this.addNewProductService.AddProduct(Formdata).subscribe({
      next: data => console.log(data),
      error: error => console.log(error)
    });
    console.log(Formdata);
    this.router.navigate(['/Dashboard/Products'])
  }
  else
  {
    console.log("E+++++====error in : ");
    console.log(this.productForm);
  }
  }

GetImagePath(event: any) {

    const file = event.target.files[0];
    this.productForm.patchValue({
      ImageOfProduct:file
    });
    this.productForm.get('ImageOfProduct')?.updateValueAndValidity()

    const reader = new FileReader();     //to reade image file and dispaly it
    reader.onload = () => {
      this.base64 = reader.result as string;
    }
    reader.readAsDataURL(file)
  }
}
