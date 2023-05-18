import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { HomeComponent } from './home/home.component';
import { ProductComponent } from './product/product.component';
import { OrderComponent } from './order/order.component';
import { ReviewComponent } from './review/review.component';
import { CartComponent } from './cart/cart.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { AboutUsComponent } from './about-us/about-us.component';
import { CustomerServiceComponent } from './customer-service/customer-service.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ElectronicComponent } from './electronic/electronic.component';
import { FashionComponent } from './fashion/fashion.component';
import { HTTP_INTERCEPTORS, HttpClient, HttpClientModule } from '@angular/common/http';
import { AccessoriesComponent } from './accessories/accessories.component';
import { AccountComponent } from './account/account.component';
import { JewelleryComponent } from './jewellery/jewellery.component';
import { ProductDetailsComponent } from './product-details/product-details.component';
import { AddProductComponent } from './add-product/add-product.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { EditProductComponent } from './edit-product/edit-product.component';
import { DeleteProductComponent } from './delete-product/delete-product.component';
import { AddSupplierComponent } from './add-supplier/add-supplier.component';
import { SuppliersComponent } from './suppliers/suppliers.component';
import { EditSupplierComponent } from './edit-supplier/edit-supplier.component';
import { DeleteSupplierComponent } from './delete-supplier/delete-supplier.component';
import { ProductDashboardComponent } from './product-dashboard/product-dashboard.component';
import { OffersDashboardComponent } from './offers-dashboard/offers-dashboard.component';
import { AddOfferComponent } from './add-offer/add-offer.component';
import { EditOfferComponent } from './edit-offer/edit-offer.component';
import { DelivaryComponent } from './delivary/delivary.component';
import { TokenInterceptorervice } from './Services/token-interceptorervice.service';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    HomeComponent,
    ProductComponent,
    OrderComponent,
    ReviewComponent,
    CartComponent,
    PageNotFoundComponent,
    HeaderComponent,
    FooterComponent,
    AboutUsComponent,
    CustomerServiceComponent,
    ElectronicComponent,
    FashionComponent,
    AccessoriesComponent,
    AccountComponent,
    JewelleryComponent,
    ProductDetailsComponent,
    AddProductComponent,
    DashboardComponent,
    EditProductComponent,
    DeleteProductComponent,
    AddSupplierComponent,
    SuppliersComponent,
    EditSupplierComponent,
    DeleteSupplierComponent,
    ProductDashboardComponent,
    OffersDashboardComponent,
    AddOfferComponent,
    EditOfferComponent,
    DelivaryComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    FormsModule

  ],
  providers: [{provide:HTTP_INTERCEPTORS,
    useClass:TokenInterceptorervice,
    multi:true
  }],
  
  bootstrap: [AppComponent]
})
export class AppModule { }
