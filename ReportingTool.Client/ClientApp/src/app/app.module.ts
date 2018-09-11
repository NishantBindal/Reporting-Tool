import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AppComponent } from './app.component';
import { NavbarComponent } from './component/navbar/navbar.component';
import { FooterComponent } from './component/footer/footer.component';
import { LoginComponent } from './component/login/login.component';
import { SignupComponent } from './component/signup/signup.component';
import { LandingPageComponent } from './component/landing-page/landing-page.component';
import { GridViewComponent } from './component/grid-view/grid-view.component';
import { AddVoucherComponent } from './component/add-voucher/add-voucher.component';
import { TypeHeadComponent } from './component/type-head/type-head.component';
import { DatePickerComponent } from './component/date-picker/date-picker.component';
import { FormsModule } from '@angular/forms';
import { TablePaginatorComponent } from './component/table-paginator/table-paginator.component';
import { AppRoutingModule } from './app-routing.module';
import { SearchComponent } from './component/search/search.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    FooterComponent,
    LoginComponent,
    SignupComponent,
    LandingPageComponent,
    GridViewComponent,
    AddVoucherComponent,
    TypeHeadComponent,
    DatePickerComponent,
    TablePaginatorComponent,
    SearchComponent
  ],
  imports: [
    BrowserModule, NgbModule, FormsModule, AppRoutingModule      
  ],
  providers: [],
  entryComponents: [LoginComponent, SignupComponent],
  bootstrap: [AppComponent]
})
export class AppModule { }
