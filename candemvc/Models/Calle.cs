using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace candemvc.Models
{
    [Table("Calles")]
    public class Calle
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID_Calle")]
        public int ID_Calle { get; set; }

        [Display(Name = "Nombre de la calle")]
        [Column("Nombre_Calle")]
        public string NombreCalle { get; set; }

        public Calle(string nombreCalle)
        {
            NombreCalle = nombreCalle;
        }
    }

}