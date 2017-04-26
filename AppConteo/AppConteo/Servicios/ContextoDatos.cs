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

        //Actualizar tabla
        public void Actualizar<TClass>(TClass[] elementos)
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
                            conexion.Update(item);
                        }
                    });
                }
            }
            catch (Exception ex)
            {

            }
        }

        //Eliminar tabla
        public void Borrar<TClass>(TClass[] elementos)
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
                            conexion.Delete(item);
                        }
                    });
                }
            }
            catch (Exception ex)
            {

            }
        }

        //Validar usuario
        public int validarUsuario(string usuario, string clave)
        {
            using (var conexion = NuevaConexion())
            {
                var data = conexion.Table<Usuario>();
                var data1 = data.Where(x => x.usuario == usuario && x.clave == clave).FirstOrDefault();
                if (data1 != null)
                {
                    return data1.id_usuario;
                }
                else
                {
                    return 0;
                }
            }   
        }

        //Devolver un artículo específico
        public Articulo GetArticulo(string codigo)
        {
            using (var conexion = NuevaConexion())
            {
                return conexion.Table<Articulo>().FirstOrDefault(x => x.id_articulo == codigo);
            }
        }

        //Devolver una lista de artículos con filtro
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

        //Devolver un conteo específico
        public Conteo GetConteo(int id)
        {
            using (var conexion = NuevaConexion())
            {
                return conexion.Table<Conteo>().FirstOrDefault(x => x.id_conteo == id);
            }
        }

        //Devolver la lista de conteos de un artículo específico
        public List<Conteo> GetConteos(string id_articulo)
        {
            using (var conexion = NuevaConexion())
            {
                return conexion.Table<Conteo>().Where(x => x.id_articulo == id_articulo).OrderBy(c => c.id_conteo).ToList();
            }
        }

        //Devolver todos los conteos existentes
        public List<Conteo> GetConteosTodos()
        {
            using (var conexion = NuevaConexion())
            {
                return conexion.Table<Conteo>().ToList();
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

        //Cerrar conexión
        public void Dispose()
        {
            using (var conexion = NuevaConexion())
            {
                conexion.Dispose();
            }
        }

        
    }
}
