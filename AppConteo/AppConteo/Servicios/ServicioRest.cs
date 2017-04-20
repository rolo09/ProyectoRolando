using AppConteo.Modelos;
using Newtonsoft.Json;
using Plugin.Connectivity;
using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppConteo.Servicios
{
    class ServicioRest
    {
        public ContextoDatos Contexto { get; set; }
        public async void getUsuarios(ContextoDatos contexto)
        {
            try
            {
                Contexto = contexto;
                using (var httpClient = new HttpClient())
                {
                    HttpResponseMessage llamada = 
                        await httpClient.GetAsync("http://172.28.130.25/apiWebXamarin/api/usuarios").ConfigureAwait(false);

                    if (llamada.IsSuccessStatusCode)
                    {
                        var json = await llamada.Content.ReadAsStringAsync().ConfigureAwait(false);

                        var resultado = JsonConvert.DeserializeObject<Usuario[]>(json);

                        foreach (var item in resultado)
                        {
                            Contexto.Guardar(new[] { new Usuario() { id_usuario = item.id_usuario, usuario = item.usuario, clave = item.clave, id_inventario = item.id_inventario } });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        public async void getInventarios(ContextoDatos contexto)
        {
            try
            {
                Contexto = contexto;
                using (var httpClient = new HttpClient())
                {
                    HttpResponseMessage llamada =
                        await httpClient.GetAsync("http://172.28.130.25/apiWebXamarin/api/inventarios").ConfigureAwait(false);

                    if (llamada.IsSuccessStatusCode)
                    {
                        var json = await llamada.Content.ReadAsStringAsync().ConfigureAwait(false);

                        var resultado = JsonConvert.DeserializeObject<Inventario[]>(json);

                        foreach (var item in resultado)
                        {
                            Contexto.Guardar(new[] { new Inventario() { id_inventario = item.id_inventario, descripcion_inventario = item.descripcion_inventario } });
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public async void getArticulos(ContextoDatos contexto)
        {
            try
            {
                Contexto = contexto;
                using (var httpClient = new HttpClient())
                {
                    HttpResponseMessage llamada =
                        await httpClient.GetAsync("http://172.28.130.25/apiWebXamarin/api/articulos").ConfigureAwait(false);

                    if (llamada.IsSuccessStatusCode)
                    {
                        var json = await llamada.Content.ReadAsStringAsync().ConfigureAwait(false);

                        var resultado = JsonConvert.DeserializeObject<Articulo[]>(json);

                        foreach (var item in resultado)
                        {
                            Contexto.Guardar(new[] { new Articulo() { id_articulo = item.id_articulo,
                                                                    descripcion_articulo = item.descripcion_articulo,
                                                                    ultima_compra = item.ultima_compra,
                                                                    ultima_salida = item.ultima_salida,
                                                                    minimo = item.minimo,
                                                                    maximo = item.maximo, saldo = 
                                                                    item.saldo,
                                                                    id_inventario =item.id_inventario  } });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }


    }
}
