import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { UploadModule } from "@progress/kendo-angular-upload";
import { DialogModule } from "@progress/kendo-angular-dialog";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { BrowserModule } from "@angular/platform-browser";
import { ButtonsModule } from "@progress/kendo-angular-buttons";
import { ReactiveFormsModule } from "@angular/forms";
import { RouterModule } from "@angular/router";
import { SharedRoutingModule } from "./shared-routing.module";
import { HeaderComponent } from "./layout/header/header.component";
import { AuthComponent } from "./auth/auth.component";
import { UploadComponent } from "./upload/upload.component";
import { CoreModule } from "../core/core.module";
import { InputsModule } from "@progress/kendo-angular-inputs";

@NgModule({
  declarations: [ UploadComponent, HeaderComponent, AuthComponent ],
  entryComponents: [ AuthComponent ],
  imports: [
    CommonModule,
    UploadModule,
    BrowserModule,
    BrowserAnimationsModule,
    DialogModule,
    ButtonsModule,
    ReactiveFormsModule,
    RouterModule,
    SharedRoutingModule,
    CoreModule,
    InputsModule
  ],
  exports: [ UploadComponent, HeaderComponent ]
})
export class SharedModule { }
