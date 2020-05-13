import { Component, OnInit } from "@angular/core";
import { UserService } from "./core/services/user.service";

@Component({
  selector: "rs-root",
  templateUrl: "./app.component.html"
})
export class AppComponent implements OnInit {
  title = "Real Size";
  constructor (private userService: UserService) {}
  ngOnInit(): void {
    this.userService.populate();
  }
}
