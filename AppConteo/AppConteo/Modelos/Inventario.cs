using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppConteo.Modelos
{
    [Table("Inventarios")]
    public class Inventario
    {
        [PrimaryKey, NotNull]
        public int id_inventario { get; set; }
        public string descripcion_inventario { get; set; }
    }
}
