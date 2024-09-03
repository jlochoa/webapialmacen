export const iva= (importe: number)=> {
    if (importe < 0) {
      return 0;
    } else {
      return importe * 0.21;
    }
}
  