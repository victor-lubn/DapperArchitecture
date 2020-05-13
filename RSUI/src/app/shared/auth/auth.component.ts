import { Component, Input, OnInit } from "@angular/core";
import { DialogRef, DialogContentBase } from "@progress/kendo-angular-dialog";
import { FormControl, FormGroup } from "@angular/forms";
import { Router } from "@angular/router";
import { UserService } from "src/app/core/services/user.service";
import { AuthComponentType } from "src/app/core/enums/auth-component.type";

@Component({
    selector: "rs-auth",
    templateUrl: "./auth.component.html"
})
export class AuthComponent extends DialogContentBase implements OnInit {

    @Input() url?: string;
    email: string;
    password: string;
    type: AuthComponentType = AuthComponentType.logIn;
    authComponentType = AuthComponentType;
    title: string;
    btnTitle: string;

    authForm: FormGroup = new FormGroup({
        email: new FormControl(this.email),
        password: new FormControl(this.password)
    });

    constructor(
        private userService: UserService,
        public dialog: DialogRef,
        private router: Router) {
        super(dialog);
    }

    ngOnInit(): void {
        if (this.url === "/register") {
            this.signUp();
        } else {
            this.logIn();
        }
    }

    close(): boolean {
        this.router.navigateByUrl("/");
        this.dialog.close({ text: "Cancel" });
        return false;
    }

    signUp(): boolean {
        this.router.navigateByUrl("/register");
        this.type = this.authComponentType.signUp;
        this.title = "Create account";
        this.btnTitle = "Sign up";
        return false;
    }

    logIn(): boolean {
        this.router.navigateByUrl("/login");
        this.type = this.authComponentType.logIn;
        this.title = "Welcome";
        this.btnTitle = "Log in";
        return false;
    }

    submitForm() {
        const credentials = this.authForm.value;
        this.userService
        .attemptAuth(this.type, credentials)
        .subscribe(
          data => this.router.navigateByUrl("/"),
          err => {
          },
          () => this.close()
        );
    }
}
