import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-contador',
  templateUrl: './contador.component.html',
  styleUrls: ['./contador.component.css']
})
export class ContadorComponent implements OnInit {
  @Input() numero: number = 0;

  constructor() {}

  ngOnInit() {}

  setContador(valor: number) {
    this.numero += valor;
  }
}

