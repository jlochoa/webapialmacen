import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Subject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AuthGuard } from '../guards/auth-guard.service';

@Injectable({
  providedIn: 'root'
})
export class SignalrService {
  urlSignalR = environment.urlSignalR;
  hubConnection!: signalR.HubConnection;
  messageSubscription: Subject<string> = new Subject<string>();
  connected = false;

  constructor(private authGuard:AuthGuard) {
    this.configure();
  }

  configure() {
    if (!this.hubConnection) {
      this.hubConnection = new signalR.HubConnectionBuilder()
        .withUrl(this.urlSignalR, {
          skipNegotiation: true,
          transport: signalR.HttpTransportType.WebSockets,
        })
        .withAutomaticReconnect()
        .build();
    }
    if (this.authGuard.isLoggedIn()){
      this.connect();
    }
  }

  connect(){
    this.hubConnection
        .start()
        .then(() => {
          console.log('Connection started');
          this.connected = true;
          this.listenMessages();
        })
        .catch((err) => console.log('Error while starting connection: ' + err));
  }

  disconnect() {
    this.hubConnection.stop().then(() => {
      console.log('Connection stopped');
      this.connected = false;
    }).catch((err) => console.log('Error while stopping connection: ' + err));
  }

  listenMessages() {
    this.hubConnection.on('GetMessage', (message: string) => {
      this.messageSubscription.next(message);
    });
  }

  sendMessage(message: string) {
    this.hubConnection.send('SendMessage', message);
  }
}
