import { Injectable } from "@angular/core";
import { Observable ,  BehaviorSubject ,  ReplaySubject } from "rxjs";
import { ApiService } from "./api.service";
import { JwtService } from "./jwt.service";
import { map } from "rxjs/operators";
import { User } from "../models/user.model";
import { AuthComponentType } from "../enums/auth-component.type";

@Injectable()
export class UserService {
  currentUserSubject = new BehaviorSubject<User>({} as User);
  isAuthenticatedSubject = new ReplaySubject<boolean>(1);
  authComponentType = AuthComponentType;
  controller = "account";

  constructor (
    private apiService: ApiService,
    private jwtService: JwtService
  ) {}

  populate() {
    if (this.jwtService.getToken()) {
      this.apiService.get(`/${this.controller}/current`)
      .subscribe(
        data => this.setAuth(data),
        (err) => this.purgeAuth()
      );
    } else {
      this.purgeAuth();
    }
  }

  setAuth(user: User) {
    this.jwtService.saveToken(user.token);
    this.currentUserSubject.next(user);
    this.isAuthenticatedSubject.next(true);
  }

  purgeAuth() {
    this.jwtService.destroyToken();
    this.currentUserSubject.next({} as User);
    this.isAuthenticatedSubject.next(false);
  }

  attemptAuth(type: AuthComponentType, credentials): Observable<User> {
    const route = (type === this.authComponentType.logIn)
      ? `/token?email=${credentials.email}&password=${credentials.password}`
      : `/${this.controller}/register`;

    return this.apiService.post(route, credentials)
      .pipe(map(
      data => {
        this.setAuth(data);
        return data;
      }
    ));
  }

  getCurrentUser(): User {
    return this.currentUserSubject.value;
  }

  update(user): Observable<User> {
    return this.apiService
    .put(`/${this.controller}/user`, { user })
    .pipe(map(data => {
      this.currentUserSubject.next(data.user);
      return data.user;
    }));
  }
}
