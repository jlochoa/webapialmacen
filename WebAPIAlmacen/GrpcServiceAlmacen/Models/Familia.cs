using System;
using System.Collections.Generic;

namespace GrpcServiceAlmacen.Models;

public partial class Familia
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
