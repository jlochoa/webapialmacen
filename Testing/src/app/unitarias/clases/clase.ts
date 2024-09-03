export class Jugador {
    puntos: number;
  
    constructor() {
      this.puntos = 100;
    }
  
    restarPuntos(puntos: number) {
      if (puntos >= this.puntos) {
        this.puntos = 0;
      } else {
        this.puntos = this.puntos - puntos;
      }
  
      return this.puntos;
    }
  }
  