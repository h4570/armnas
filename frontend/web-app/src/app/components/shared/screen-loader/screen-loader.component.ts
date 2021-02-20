import { Component, Input, OnChanges } from '@angular/core';

@Component({
  selector: 'app-screen-loader',
  templateUrl: './screen-loader.component.html',
  styleUrls: ['./screen-loader.component.scss']
})
export class ScreenLoaderComponent implements OnChanges {

  @Input()
  public show = false;

  @Input()
  public outside = false;

  public fadeIn = false;
  public fadeOut = false;
  public spin = false;
  public showContainer = false;

  public ngOnChanges(): void {
    if (this.show) {
      this.runFadeInSpinnerAnimation();
    } else {
      this.runFadeOutAnimation();
    }
  }

  private runFadeInSpinnerAnimation(): void {
    this.showContainer = true;
    this.fadeIn = true;
    this.spin = true;
    setTimeout(() => this.fadeIn = false, 700);
  }

  private runFadeOutAnimation(): void {
    this.fadeOut = true;
    this.spin = false;
    setTimeout(() => {
      this.fadeOut = false;
      this.showContainer = false;
    }, 700);
  }

}
