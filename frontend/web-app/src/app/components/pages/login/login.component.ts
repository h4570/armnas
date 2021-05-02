import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LoginResponse, UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {

  public password: string;
  public isFreezed: boolean;
  public loginFailed: boolean;
  public adminNotFound: boolean;

  constructor(
    public readonly userService: UserService,
    private readonly router: Router,
  ) { }

  public async onLoginClick(): Promise<void> {
    this.isFreezed = true;
    this.adminNotFound = false;
    if (this.password.trim().length === 0) {
      this.loginFailed = true;
      this.password = '';
    }
    const res = await this.userService.login({ id: 0, login: 'admin', password: this.password });
    if (res === LoginResponse.success) {
      this.router.navigateByUrl('/home');
    }
    else if (res === LoginResponse.adminNotFound) {
      this.adminNotFound = true;
    } else {
      this.loginFailed = true;
      this.password = '';
    }
    this.isFreezed = false;
  }

}
