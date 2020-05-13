import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { HomeComponent } from "./home/home.component";
import { AuthComponent } from "src/app/shared/auth/auth.component";

const routes: Routes = [
    { path: "", component: HomeComponent }
  ];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
  })
 export class HomeRoutingModule {}
