import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { HttpClientModule } from "@angular/common/http";
import { HomeModule } from "./screens/home/home.module";
import { DialogsModule } from "@progress/kendo-angular-dialog";
import { ButtonsModule } from "@progress/kendo-angular-buttons";
import { SharedModule } from "./shared/shared.module";
import { CoreModule } from "./core/core.module";
import { InputsModule } from '@progress/kendo-angular-inputs';


@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    SharedModule,
    HomeModule,
    DialogsModule,
    ButtonsModule,
    CoreModule,
    InputsModule
  ],
  providers: [
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
