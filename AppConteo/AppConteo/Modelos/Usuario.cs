using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppConteo.Modelos
{
    [Table("Usuarios")]
    public class Usuario
    {
        [PrimaryKey, NotNull]
        public int id_usuario { get; set; }
        public string usuario { get; set; }
        public string clave { get; set; }
        public int id_inventario { get; set; }
    }
}
