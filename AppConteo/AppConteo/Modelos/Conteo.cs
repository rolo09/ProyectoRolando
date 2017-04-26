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
        public string id_articulo { get; set; }
        [NotNull]
        public int id_usuario { get; set; }
        [NotNull]
        public int conteo { get; set; }
        [NotNull]
        public string nota { get; set; }
        [NotNull]
        public DateTime fecha { get; set; }

        public override string ToString()
        {
            return string.Format("#{0} Cant:{1} Fecha:{2}", id_conteo, conteo, fecha);
        }
    }
}
