// See https://aka.ms/new-console-template for more information
using TestingASP.Calculos;
using TestingASP.Listas;

var misCalculos = new ClaseCalculos();

Console.WriteLine(misCalculos.LetraDNI(33420108));
Console.WriteLine(misCalculos.ImporteNetoFactura(2000));

var persona1 = new Persona();
persona1.Nombre = "Julián";
persona1.Edad = 60;
persona1.Soltero = true;
persona1.FechaIngresoALaEmpresa = DateTime.Now;

var persona2 = new Persona();
persona2.Nombre = "Julio";
persona2.Edad = 50;
persona2.Soltero = false;
persona2.FechaIngresoALaEmpresa = DateTime.Now.AddYears(-2);

var lista = new List<Persona>();
lista.Add(persona1);
lista.Add(persona2);

var miLista = new ListaPersonas(lista);
Console.WriteLine(miLista.Solteros());
Console.WriteLine(miLista.TodosMayoresEdad());
Console.WriteLine(miLista.TodosMayoresOIgualesDeUnaEdad(55));
Console.WriteLine(miLista.PersonaMayor().Nombre);
