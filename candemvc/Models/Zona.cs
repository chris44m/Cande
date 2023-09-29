using System;
using System.Collections.Generic;

namespace CandelariaP.Models;

public partial class Zona
{
    public int IdZona { get; set; }

    public string NombreZona { get; set; }

    public decimal? Precio { get; set; }

    public int? IdCalle { get; set; }

    public virtual ICollection<Asiento> Asientos { get; set; } = new List<Asiento>();

    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();

    public virtual Calle IdCalleNavigation { get; set; }
}
