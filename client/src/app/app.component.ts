import { Component } from '@angular/core';
import { AuthService } from './_services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Village Fund Auditor Registration';

  constructor(
    private authService: AuthService
  ) { }

  loggedIn(): boolean {
    return this.authService.loggedIn();
  }
}
