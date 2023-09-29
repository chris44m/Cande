using System;
using System.Collections.Generic;

namespace CandelariaP.Models;

public partial class Asiento
{
    public int IdAsiento { get; set; }

    public int? IdZona { get; set; }

    public int? IdCalle { get; set; }

    public bool? Disponible { get; set; }

    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();

    public virtual Calle IdCalleNavigation { get; set; }

    public virtual Zona IdZonaNavigation { get; set; }
}
