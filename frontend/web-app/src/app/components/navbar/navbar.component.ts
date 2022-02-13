import { Component, HostListener, OnDestroy, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Subscription } from 'rxjs';
import { Message } from 'src/app/models/message.model';
import { AppService } from 'src/app/services/app.service';
import { AuthService } from 'src/app/services/auth.service';
import { ODataService } from 'src/app/services/odata.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit, OnDestroy {

  public isWinterTime = true;
  public isScreenSmall: boolean;
  public authSub: Subscription;
  public newMessages: Message[] = [];
  public isMessagesIconDisabled = false;
  public messagesAutoRefresh: any; // NodeJS.Timeout
  public keycloakUrl = environment.urls.keycloak;
  private readonly autoRefreshInterval = 10000;

  constructor(
    public readonly translate: TranslateService,
    public readonly appService: AppService,
    public readonly authService: AuthService,
    private readonly odata: ODataService
  ) {
    this.setWinterTime();
  }

  @HostListener('window:resize', ['$event'])
  public onResize(event: Event): void {
    this.updateIsScreenSmallVariable();
  }

  public async ngOnInit(): Promise<void> {
    this.updateIsScreenSmallVariable();
    if (this.authService.isAuthenticated)
      await this.refresh();
    this.authSub = this.authService.onIsAuthenticatedChange.subscribe(async (val) => {
      if (val) await this.refresh();
    });
    this.messagesAutoRefresh = setInterval(async () => {
      if (this.authService.isAuthenticated)
        await this.refresh();
    }, this.autoRefreshInterval);
  }

  public async ngOnDestroy(): Promise<void> {
    this.authSub.unsubscribe();
    if (this.messagesAutoRefresh)
      clearInterval(this.messagesAutoRefresh);
  }

  public async onDeleteMessageClick(msg: Message) {
    msg.hasBeenRead = true;
    const ref = this.odata.messages.entities().entity(msg.id);
    await ref.patch(msg).toPromise();
    await this.refresh();
  }

  public changeLang(language: string): void {
    localStorage.setItem('language', language);
    this.translate.use(language);
  }

  public onLogoutClick(): void {
    this.authService.logout();
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

  private async refresh(): Promise<void> {
    const odataMessages = this.odata.messages.entities();
    this.newMessages = await odataMessages
      .filter({ hasBeenRead: false })
      .top(30)
      .get()
      .toPromise()
      .then(c => c.entities);
    this.newMessages = this.newMessages.reverse();
    if (this.newMessages.length > 0)
      this.isMessagesIconDisabled = false;
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
