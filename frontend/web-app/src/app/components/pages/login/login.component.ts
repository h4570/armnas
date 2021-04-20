import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { LoginResponse, UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {

  public password: string;
  public isFreezed: boolean;
  public failed: boolean;

  constructor(
    public readonly userService: UserService,
    private readonly router: Router,
  ) { }

  public async onLoginClick(): Promise<void> {
    this.isFreezed = true;
    if (this.password.trim().length === 0) {
      this.failed = true;
      this.password = '';
    }
    const res = await this.userService.login({ id: 0, login: 'admin', password: this.password });
    if (res === LoginResponse.success) {
      this.router.navigateByUrl('/home');
    } else {
      this.failed = true;
      this.password = '';
    }
    this.isFreezed = false;
  }

}
