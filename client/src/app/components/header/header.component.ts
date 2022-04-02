import { Component, OnInit } from '@angular/core';
import { AlertService } from 'src/app/_services/alert.service';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  expiredTime: any;

  constructor(
    public authService: AuthService,
    private alert: AlertService
  ) { }

  ngOnInit(): void {
    setInterval(() => {
      this.setExpiredTime();
    }, 1000);
  }

  setExpiredTime(): void {
    if (!this.authService.decodeToken) return;
    const secNum: any = new Date(this.authService.decodeToken.exp - Math.floor(Date.now() / 1000));
    let hours: any   = Math.floor(secNum / 3600);
    let minutes: any = Math.floor((secNum - (hours * 3600)) / 60);
    let seconds: any = secNum - (hours * 3600) - (minutes * 60);

    if (!hours && !minutes && !seconds) {
      this.logout();
      return;
    }

    if (hours   < 10) {hours   = '0' + hours; }
    if (minutes < 10) {minutes = '0' + minutes; }
    if (seconds < 10) {seconds = '0' + seconds; }

    this.expiredTime = hours + ':' + minutes + ':' + seconds;
  }

  logout(): any {
    this.authService.logout();
    this.alert.success('Logout successfully');
  }

}
