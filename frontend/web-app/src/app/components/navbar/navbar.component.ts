import { Component, HostListener } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { AppService } from 'src/app/services/app.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent {

  public isWinterTime = true;
  public isScreenSmall: boolean;

  constructor(
    public readonly translate: TranslateService,
    public readonly appService: AppService
  ) {
    this.setWinterTime();
  }

  @HostListener('window:resize', ['$event'])
  public onResize(event: Event): void {
    this.updateIsScreenSmallVariable();
  }

  public changeLang(language: string): void {
    localStorage.setItem('language', language);
    this.translate.use(language);
  }

  public onTweakClick(): void {
    this.appService.isInTweakMode = true;
  }

  public onNormalModeClick(): void {
    this.appService.isInTweakMode = false;
  }

  private updateIsScreenSmallVariable(): void {
    this.isScreenSmall = window.innerWidth <= 720;
  }

  private setWinterTime(): void {
    const now = new Date();
    const currentYear = new Date().getFullYear();

    const fromDec = new Date(currentYear, 11, 1);
    const toDec = new Date(currentYear, 11, 31);
    const isDecember = (now.getTime() <= toDec.getTime() && now.getTime() >= fromDec.getTime());

    const fromJan = new Date(currentYear, 0, 1);
    const toJan = new Date(currentYear, 0, 31);
    const isJanuary = (now.getTime() <= toJan.getTime() && now.getTime() >= fromJan.getTime());

    if (isDecember || isJanuary)
      this.isWinterTime = true;
  }

}
