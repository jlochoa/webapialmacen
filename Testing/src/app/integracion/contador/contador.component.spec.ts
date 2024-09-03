import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { By } from '@angular/platform-browser';

import { ContadorComponent } from './contador.component';

describe('ContadorComponent', () => {
  let component: ContadorComponent;
  let fixture: ComponentFixture<ContadorComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [FormsModule],
      declarations: [ContadorComponent]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ContadorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  xit('should create', () => {
    expect(component).toBeTruthy();
  });

  xit('Debe mostrar el numero', () => {
    component.numero = 8;
    fixture.detectChanges();
    const h2: HTMLElement = fixture.debugElement.query(By.css('h2')).nativeElement;

    expect(h2.innerHTML).toContain('8');
  });

  xit('Debe mostrar el número que aparece en el h2 cuando varía mediante setContador', () => {
    component.setContador(1);
    fixture.detectChanges();

    const h2: HTMLElement = fixture.debugElement.query(By.css('h2')).nativeElement;
    expect(h2.innerHTML).toContain('1');
  });

  xit('Debe incrementar el numero que aparece en el input cuando se produce el click en el botón +1', () => {
    const botones = fixture.debugElement.queryAll(By.css('button'));
    const input: HTMLInputElement = fixture.debugElement.query(By.css('input')).nativeElement;

    // El primer botón es el +1
    botones[0].triggerEventHandler('click', null); //null es porque no enviamos argumentos al click
    fixture.detectChanges();
    // whenStable se asegura de que el ciclo de cambios ha concluído. En este caso funciona sin la función también, pero puede que a veces el test (expect) se ejecute antes de que haya terminado el ciclo de detección de cambios
    fixture.whenStable().then(() => {
      expect(input.value).toContain('1');
    });
  });

  xit('Debe decrementar el numero que aparece en el input cuando se produce el click en el botón +1', () => {
    const botones = fixture.debugElement.queryAll(By.css('button'));
    const input: HTMLInputElement = fixture.debugElement.query(By.css('input')).nativeElement;

    // El primer botón es el +1
    botones[1].triggerEventHandler('click', null); //null es porque no enviamos argumentos al click
    fixture.detectChanges();
    // whenStable se asegura de que el ciclo de cambios ha concluído. En este caso funciona sin la función también, pero puede que a veces el test (expect) se ejecute antes de que haya terminado el ciclo de detección de cambios
    fixture.whenStable().then(() => {
      expect(input.value).toContain('-1');
    });
  });
  
});
