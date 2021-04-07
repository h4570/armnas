import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-transmission',
  templateUrl: './transmission.component.html',
  styleUrls: ['./transmission.component.scss']
})
export class TransmissionComponent implements OnInit {

  public readonly transmissionUrl: string;

  constructor() {
    this.transmissionUrl = environment.urls.transmission;
    console.log(this.transmissionUrl);
  }

  public ngOnInit(): void {
  }

}
