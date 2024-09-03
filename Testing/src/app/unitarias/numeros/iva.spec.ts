import { iva } from './iva';

describe('test función iva', () => {
  it('Debe de retornar 0 si el importe es negativo', () => {
    const ivaDevuelto = iva(-100);
    expect(ivaDevuelto).toBe(0);
  });

  it('Debe de retornar el iva correcto con importe positivo', () => {
    const ivaDevuelto= iva(100);
    expect(ivaDevuelto).toBe(21);
  });
});
