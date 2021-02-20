import { Component, OnDestroy, OnInit } from '@angular/core';
import { AppService } from './services/app.service';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { Title } from '@angular/platform-browser';
import { environment } from 'src/environments/environment';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit, OnDestroy {

  public env = environment;
  public animationActive = false;
  private animSubscription: Subscription;

  constructor(
    public readonly appService: AppService,
    private readonly router: Router,
    private readonly translate: TranslateService
  ) { }

  public async ngOnInit(): Promise<any> {
    await this.appService.updateVersionAndBuild();
    await this.configureLanguage();
    this.configureRoutingAnimation();
  }

  public ngOnDestroy(): void {
    this.animSubscription.unsubscribe();
  }

  private async configureLanguage(): Promise<any> {
    this.translate.setDefaultLang('en-US');
    const userLang = this.appService.getUserLanguage();
    await this.translate.use(userLang).toPromise();
  }

  private configureRoutingAnimation(): void {
    this.animSubscription = this.router.events.subscribe((res: any) => {
      if (res.navigationTrigger) {
        this.animationActive = true;
        setTimeout(() => {
          this.animationActive = false;
        }, 400);
      }
    });
  }

}
