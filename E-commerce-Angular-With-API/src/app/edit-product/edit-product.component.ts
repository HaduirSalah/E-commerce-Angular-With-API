import { Component, OnChanges, OnInit, SimpleChanges, ViewChild } from '@angular/core';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { ProductService } from '../Services/product.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IBrand } from '../SharedClassesAndTypes/IBrand';
import { ICategory } from '../SharedClassesAndTypes/ICategory';
import { Product } from '../SharedClassesAndTypes/Product';
import { EditProductService } from '../Services/edit-product.service';
import { BrandService } from '../Services/brand.service';
import { CategoryService } from '../Services/category.service';
import { IOffer } from '../SharedClassesAndTypes/IOffer';
import { ISupplier } from '../SharedClassesAndTypes/ISupplier';
import { OfferService } from '../Services/offer.service';
import { SupplierService } from '../Services/supplier.service';
import { ProductEdit } from '../SharedClassesAndTypes/ProductEdit';


@Component({
  selector: 'app-edit-product',
  templateUrl: './edit-product.component.html',
  styleUrls: ['./edit-product.component.scss']
})
export class EditProductComponent implements OnInit {
  productId: any;
  Product: any;
  ErrorMessage: string = "";
  productForm!: FormGroup;
  // productModel = new ProductEdit(0,'', '', '', 0, 0, 0, 0, 0, 0);
  Categories: ICategory[] = []
  Offers: IOffer[] = []
  Brands: IBrand[] = []
  Suppliers: ISupplier[] = []
  base64: any;

  constructor(private supplierService: SupplierService, private offerService: OfferService, private brandService: BrandService, private categoryService: CategoryService, private fb: FormBuilder, private activatedRoute: ActivatedRoute, private productsService: ProductService, private editProduct: EditProductService, private router: Router) { }
  ngOnInit(): void {
    this.productForm = this.fb.group({
      name: this.fb.control('', [Validators.required,
      Validators.pattern('[a-z A-Z 0-9]+'),
      Validators.minLength(3),
      Validators.maxLength(50),
      ]),
      description: this.fb.control('', [Validators.required,
      Validators.minLength(10),
      Validators.maxLength(500)
      ]),
      price: this.fb.control('', [Validators.required]),
      quantity: this.fb.control('', [Validators.required]),
      categoryID: this.fb.control('', [Validators.required]),
      brandID: this.fb.control('', [Validators.required]),
      offerID: this.fb.control('', [Validators.required]),
      supplierID: this.fb.control('', [Validators.required]),
      ImageOfProduct: this.fb.control(null, [Validators.required])
    }),
      this.categoryService.GetAllCategories().subscribe({
        next: data =>
          //console.log(data);
          this.Categories = data.data,
        error: err => this.ErrorMessage = err
      }),
      this.brandService.GetAllBrands().subscribe({
        next: data =>
          //console.log(data);
          this.Brands = data.data,
        error: err => this.ErrorMessage = err
      }),
      this.offerService.GetAllOffers().subscribe({
        next: data =>
          //console.log(data);
          this.Offers = data.data,
        error: err => this.ErrorMessage = err
      }),
      this.supplierService.GetAllSuppliers().subscribe({
        next: data =>
          //console.log(data);
          this.Suppliers = data.data,
        error: err => this.ErrorMessage = err
      }),
      this.activatedRoute.paramMap.subscribe((params: ParamMap) => {
        this.productId = params.get("id");
      }),
      this.productsService.GetProductByID(this.productId).subscribe({
        next: (data) => {
          this.Product = data.data
          const file = data.data.imageOfProduct;
          this.productForm.patchValue({
            ImageOfProduct: file
          });
          this.productForm.get('ImageOfProduct')?.updateValueAndValidity()

          const reader = new FileReader();     //to reade image file and dispaly it
          reader.onload = () => {
            this.base64 = reader.result as string;
          }
        },
        error: (erorr: string) => this.ErrorMessage = erorr
      })
  }

  get id() {
    return this.productForm.get('id');
  }
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
  get imageName() {
    return this.productForm.get('imageName');
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

  submitData() {
    if (this.productForm.valid) {
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

      this.editProduct.EditProduct(this.productForm.value).subscribe({
        next: data => console.log(data),
        error: error => console.log(error)
      });
      console.log(this.productForm.value)
      this.router.navigate(['/Dashboard'])
    }
    else {
      console.log("E+++++====error in : ");
      console.log(this.productForm);
    }
  }

  categorySelected(event: any) {
    this.categoryID?.setValue(event.target.value, { onlySelf: true, });
    console.log(" this.CategoryID   " + event.target.value)
  }




  // brandSelected(event: any) {
  //   this.brandID?.setValue(event.target.value, { onlySelf: true, });
  //   console.log(" this.BrandID   " + event.target.value)
  // }

  // offerSelected(event: any) {
  //   this.offerID?.setValue(event.target.value, { onlySelf: true, });
  //   console.log(" this.OfferID   " + event.target.value)
  // }

  // supplierSelected(event: any) {
  //   this.supplierID?.setValue(event.target.value, { onlySelf: true, });
  //   console.log(" this.OfferID   " + event.target.value)
  // }

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
    // this .productForm.patchValue({imageName:this.base64})
    reader.readAsDataURL(file)
  }

  // Update(Product:any){
  //   this.base64=Product.imageName
  //   console.log("Called")
  // }

}
