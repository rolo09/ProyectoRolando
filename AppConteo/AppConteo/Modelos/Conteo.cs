using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppConteo.Modelos
{
    [Table("Conteos")]
    public class Conteo
    {
        [PrimaryKey, AutoIncrement, NotNull]
        public int id_conteo { get; set; }
        [NotNull]
        public int id_inventario { get; set; }
        [NotNull]
        public int id_articulo { get; set; }
        [NotNull]
        public int id_usuario { get; set; }
        [NotNull]
        public int conteo { get; set; }
        public string nota { get; set; }
        [NotNull]
        public DateTime fecha { get; set; }
    }
}
