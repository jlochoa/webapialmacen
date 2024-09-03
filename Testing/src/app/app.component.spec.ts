import { TestBed } from '@angular/core/testing';
import { AppComponent } from './app.component';

// describe marca el comienzo de la prueba. 'AppComponent' es el título de la prueba
describe('AppComponent', () => {
  // TestBed configura e inicializa el entorno para pruebas unitarias y proporciona métodos para crear componentes y servicios en pruebas unitarias.
  // El método TestBed.configureTestingModule() crea el módulo de pruebas con los componentes que va a probar
  beforeEach(() =>
    TestBed.configureTestingModule({
      declarations: [AppComponent]
    })
  );

  // Cada it es una prueba unitaria. 'should create the app' es el nombre de la prueba
  xit('should create the app', () => {
    // fixture es la pieza (componente) que se va a probar
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    // Se expera (expect) que el componente (app-->AppComponent) se haya creado toBeTruthy
    expect(app).toBeTruthy();
  });

  xit(`should have as title 'testing'`, () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    // Se expera (expect) que el componente (app-->AppComponent) tenga una propiedad titulo con el valor 'pruebas'
    expect(app.title).toEqual('testing');
  });

  xit('should render title', () => {
    const fixture = TestBed.createComponent(AppComponent);
    // Se activa el detector de cambios
    fixture.detectChanges();
    const compiled = fixture.nativeElement as HTMLElement;
    // Se expera (expect) que en el componente (app-->AppComponent) haya un elemento de cclase content span con el valor 'pruebas app is running!
    expect(compiled.querySelector('#test')?.textContent).toContain('testing app is running!');
  });
});
