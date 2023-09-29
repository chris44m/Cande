using System;
using System.Collections.Generic;

namespace CandelariaP.Models;

public partial class Compradore
{
    public int IdComprador { get; set; }

    public string NombreComprador { get; set; }

    public string ApellidoComprador { get; set; }

    public int? Dni { get; set; }

    public string Email { get; set; }

    public int? Teléfono { get; set; }

    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();
}
