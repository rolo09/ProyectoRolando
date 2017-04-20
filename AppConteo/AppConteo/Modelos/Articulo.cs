using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppConteo.Modelos
{
    [Table("Articulos")]
    public class Articulo
    {
        [PrimaryKey, NotNull]
        public string id_articulo { get; set; }
        public string descripcion_articulo { get; set; }
        public DateTime ultima_compra { get; set; }
        public DateTime ultima_salida { get; set; }
        public int minimo { get; set; }
        public int maximo { get; set; }
        public int saldo { get; set; }
        public int id_inventario { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}", id_articulo, descripcion_articulo);
        }
    }
}
