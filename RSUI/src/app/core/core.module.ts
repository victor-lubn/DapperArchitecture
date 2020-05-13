import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { ShowAuthedDirective } from "./directivies/show-authed.directive";
import { HTTP_INTERCEPTORS } from "@angular/common/http";
import { HttpTokenInterceptor } from "./interceptors/http.token.interceptor";
import { ApiService } from "./services/api.service";
import { JwtService } from "./services/jwt.service";
import { UserService } from "./services/user.service";

@NgModule({
  declarations: [ShowAuthedDirective],
  imports: [
    CommonModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: HttpTokenInterceptor, multi: true },
    ApiService,
    JwtService,
    UserService
  ],
  exports: [ ShowAuthedDirective ]
})
export class CoreModule { }
