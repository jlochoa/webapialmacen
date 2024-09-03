import { mensaje } from './string';

// describe agrupa pruebas,
// El primer argumento es el nombre del grupo y el segundo la función que agrupa las pruebas
describe('Pruebas de strings', () => {
  // El primer argumento es el nombre de la prueba y el segundo la función que efectua la prueba
  it('Debe de retornar un string', () => {
    // La prueba espera (expect) sea (toBe) un string
    const resp = mensaje('Juan Luis');
    expect(typeof resp).toBe('string');
  });

  it('Debe de retornar un saludo con el nombre enviado', () => {
    const nombre = 'Juan Luis';
    const resp = mensaje(nombre);

    expect(resp).toContain(nombre);
  });
});