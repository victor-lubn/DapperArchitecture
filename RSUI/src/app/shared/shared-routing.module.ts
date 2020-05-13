import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { HomeComponent } from "src/app/screens/home/home/home.component";

const routes: Routes = [
    { path: "login", component: HomeComponent },
    { path: "register", component: HomeComponent }
  ];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
  })
 export class SharedRoutingModule {
 }
