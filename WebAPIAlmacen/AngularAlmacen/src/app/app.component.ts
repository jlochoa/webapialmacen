import { Component } from '@angular/core';
import { SignalrService } from './services/signalr.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'AngularAlmacen';
  get connected() {
    return this.signalRService.connected;
  }

  constructor(private signalRService: SignalrService) {}
}
