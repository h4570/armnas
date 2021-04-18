import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {


  constructor(
    public readonly authService: AuthService,
    public readonly userService: UserService,
  ) { }

  public async ngOnInit(): Promise<void> {
    // TODO
    // - Add user to db via register()
    // - register() should be used only once
    // - Create fields
    // - use login() here
  }

}
