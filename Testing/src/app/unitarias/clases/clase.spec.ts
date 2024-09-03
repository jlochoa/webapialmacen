import { Jugador } from './clase';

describe('Pruebas de clase jugador', () => {
  let jugador: Jugador;

  // Ciclo de vida de las pruebas
  // Al iniciar las pruebas
  beforeAll(() => {
    console.warn('BeforeAll');
    // jugador.puntos = 100;
  });

  // Al principio de cada prueba
  beforeEach(() => {
    // console.warn('BeforeEach');
    // jugador.puntos = 100;
    jugador = new Jugador();
  });

  // Después de todas las pruebas
  afterAll(() => {
    // console.warn('AfterAll');
  });

  // Después de cada prueba
  afterEach(() => {
    // console.warn('AfterEach');
    // jugador.puntos = 100;
  });

  it('Debe de retornar 80 puntos si se restan 20', () => {
    // const jugador = new Jugador();
    const resp = jugador.restarPuntos(20);

    expect(resp).toBe(80);
  });

  it('Debe de retornar 50 puntos si se restan 50', () => {
    // const jugador = new Jugador();
    const resp = jugador.restarPuntos(50);

    expect(resp).toBe(50);
  });

  it('Debe de retornar 0 puntos si se restan 100 o más', () => {
    // const jugador = new Jugador();
    const resp = jugador.restarPuntos(100);

    expect(resp).toBe(0);
  });
});
