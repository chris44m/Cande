using System;
using System.Collections.Generic;

namespace CandelariaP.Models;

public partial class Compra
{
    public int IdCompra { get; set; }

    public int? IdAsiento { get; set; }

    public int? IdZona { get; set; }

    public int? IdCalle { get; set; }

    public int? IdComprador { get; set; }

    public string ImagenQr { get; set; }

    public DateTime? FechaCompra { get; set; }

    public virtual Asiento IdAsientoNavigation { get; set; }

    public virtual Calle IdCalleNavigation { get; set; }

    public virtual Compradore IdCompradorNavigation { get; set; }

    public virtual Zona IdZonaNavigation { get; set; }
}
