import { Component, OnInit } from "@angular/core";
import { DialogService, DialogCloseResult, DialogRef } from "@progress/kendo-angular-dialog";
import { FormControl, FormGroup } from "@angular/forms";
import { Router, ActivatedRoute, NavigationEnd, Event } from "@angular/router";
import { AuthComponent } from "../../auth/auth.component";
import { UserService } from "src/app/core/services/user.service";
import { User } from "src/app/core/models/user.model";

@Component({
  selector: "rs-header",
  templateUrl: "./header.component.html"
})
export class HeaderComponent implements OnInit {
  private authDialogRef: DialogRef;
  private authUrls: string[] = ["/login", "/register"];

  uploadWindowIsActive = false;
  currentUser: User;
  formGroup: FormGroup = new FormGroup({});

  constructor(
    private userService: UserService,
    private dialogService: DialogService,
    private router: Router,
    private route: ActivatedRoute) {

      this.router.events.subscribe(
      (event: Event) => {
        if (event instanceof NavigationEnd) {
          if (this.authDialogRef) {
            return;
          }

          if (this.authUrls.includes(event.url)) {
            this.authOpenDialog(event.url);
          }
        }
      });
  }

  ngOnInit() {
    this.userService.currentUserSubject.subscribe(
      (userData) => {
        this.currentUser = userData;
      }
    );
  }

  uploadOpenDialog() {
    this.uploadWindowIsActive = true;
    return false;
  }

  uploadCloseHandler() {
    this.uploadWindowIsActive = false;
  }

  authOpenDialog(url?: string) {
    this.authDialogRef = this.dialogService.open({
      content: AuthComponent
    });

    const authComponent = this.authDialogRef.content.instance;
    authComponent.url = url;

    return false;
  }

  logOut() {
    this.userService.purgeAuth();
    this.router.navigateByUrl("/");
    return false;
  }
}
