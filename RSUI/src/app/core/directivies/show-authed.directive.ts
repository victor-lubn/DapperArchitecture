import {
    Directive,
    Input,
    OnInit,
    TemplateRef,
    ViewContainerRef
  } from "@angular/core";

import { UserService } from "../services/user.service";
import { debugOutputAstAsTypeScript } from "@angular/compiler";

@Directive({ selector: "[rsShowAuthed]" })
export class ShowAuthedDirective implements OnInit {

    @Input() set rsShowAuthed(condition: boolean) {
        this.condition = condition;
    }

    condition: boolean;
    constructor(
        private templateRef: TemplateRef<any>,
        private userService: UserService,
        private viewContainer: ViewContainerRef
    ) {}

    ngOnInit(): void {
        this.userService.isAuthenticatedSubject.subscribe(
        (isAuthenticated) => {
            if (isAuthenticated && this.condition || !isAuthenticated && !this.condition) {
                this.viewContainer.createEmbeddedView(this.templateRef);
            } else {
                this.viewContainer.clear();
            }
        }
        );
    }
}
