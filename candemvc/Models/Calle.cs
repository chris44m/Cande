using System;
using System.Collections.Generic;

namespace CandelariaP.Models;

public partial class Calle
{
    public int IdCalle { get; set; }

    public string NombreCalle { get; set; }

    public virtual ICollection<Asiento> Asientos { get; set; } = new List<Asiento>();

    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();

    public virtual ICollection<Zona> Zonas { get; set; } = new List<Zona>();
}
