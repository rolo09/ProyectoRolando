using AppConteo.Modelos;
using SQLite.Net.Interop;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AppConteo.Servicios
{
    public class ContextoDatos: IDisposable
    {
        //Ubicación de la base de datos
        public string RutaConexion { get; set; }

        //Recibir de forma genérica la conexión desde las diferentes plataformas
        public Func<SQLite.Net.SQLiteConnection> NuevaConexion { get; set; }

        //Crear la nueva tabla SQLite a partir de la clase enviada
        public void Configurar<TClass>()
            where TClass : class
        {
            using (var conexion = NuevaConexion())
            {
                conexion.CreateTable<TClass>();

                var query = conexion.Table<TClass>().ToArray();

                foreach (var item in query)
                {
                    Debug.WriteLine(item.ToString());
                }
            }
        }

        //Insertar o guardar nuevos elementos en la tabla de SQLite
        public void Guardar<TClass>(TClass[] elementos)
            where TClass : class
        {
            try
            {
                using (var conexion = NuevaConexion())
                {
                    conexion.RunInTransaction(() =>
                    {
                        foreach (var item in elementos)
                        {
                            conexion.Insert(item);
                        }
                        
                    });
                }
            }
            catch (Exception ex)
            {
                
            }
            
        }

        public bool validarUsuario(string usuario, string clave)
        {
            using (var conexion = NuevaConexion())
            {
                var data = conexion.Table<Usuario>();
                var data1 = data.Where(x => x.usuario == usuario && x.clave == clave).FirstOrDefault();
                if (data1 != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }   
        }

        public Articulo GetArticulo(string codigo)
        {
            using (var conexion = NuevaConexion())
            {
                return conexion.Table<Articulo>().FirstOrDefault(x => x.id_articulo == codigo);
            }
        }

        public List<Articulo> GetArticulos(string buscar)
        {
            using (var conexion = NuevaConexion())
            {
                if (string.IsNullOrEmpty(buscar))
                    return conexion.Table<Articulo>().OrderBy(c => c.id_articulo).ToList();
                else
                    return conexion.Table<Articulo>().Where(x => x.id_articulo.Contains(buscar) || x.descripcion_articulo.Contains(buscar)).OrderBy(c => c.id_articulo).ToList();
            }
        }

        //Ejecutar comando de SQLite
        public void EjecutarComando(string query)
        {
            using (var conexion = NuevaConexion())
            {
                conexion.Execute(query);
            }
        }

        public void Dispose()
        {
            using (var conexion = NuevaConexion())
            {
                conexion.Dispose();
            }
        }

        
    }
}
