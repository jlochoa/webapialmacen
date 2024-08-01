using System;
using System.Collections.Generic;

namespace WebAPIAlmacen.Models;

public partial class Operacione
{
    public int Id { get; set; }

    public DateTime FechaAccion { get; set; }

    public string Operacion { get; set; } = null!;

    public string Controller { get; set; } = null!;

    public string Ip { get; set; } = null!;
}
