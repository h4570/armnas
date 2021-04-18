import { Component, HostListener, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Message, MessageType } from 'src/app/models/message.model';
import { AppService } from 'src/app/services/app.service';
import { ODataService } from 'src/app/services/odata.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  // eslint-disable-next-line @typescript-eslint/naming-convention
  public MessageType: typeof MessageType = MessageType;

  public isWinterTime = true;
  public isScreenSmall: boolean;
  public newMessages: Message[] = [];
  public isMessagesIconDisabled = false;

  constructor(
    public readonly translate: TranslateService,
    public readonly appService: AppService,
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
    await this.refresh();
  }

  public async onDeleteMessageClick(msg: Message) {
    const ref = this.odata.messages.entities().entity(msg.id);
    await ref.patch(msg).toPromise();
    await this.refresh();
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

  private async refresh(): Promise<void> {
    const odataMessages = this.odata.messages.entities();
    this.isMessagesIconDisabled = false;
    this.newMessages = await odataMessages
      .filter({ hasBeenRead: false })
      .get()
      .toPromise()
      .then(c => c.entities);
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
