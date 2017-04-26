using AppConteo.Servicios;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppConteo
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Springboard : ContentPage
	{
        public ContextoDatos Contexto { get; set; }
        public Springboard (ContextoDatos contexto)
		{
            Contexto = contexto;

            InitializeComponent ();

            //Botón login
            btnLogin.Clicked += async (s, e) =>
            {
                await Navigation.PopModalAsync();
            };

            //Botón artículos
            btnArticulos.Clicked += (s, e) => {
                Navigation.PushModalAsync(new ArticulosPage(Contexto));
            };

            //Botón recibir desde el servidor
            btnRecibir.Clicked += async (s, e) =>
            {
                if (!CrossConnectivity.Current.IsConnected)
                {
                    await DisplayAlert("Conexión", "No hay una conexión de internet. Pruebe de nuevo.", "Aceptar");
                    return;
                }

                if (await DisplayAlert("Sincronizar", "¿Desea recibir datos desde el servidor?", "Aceptar", "Cancelar"))
                {
                    //Eliminar inventarios existentes
                    Contexto.EjecutarComando("DELETE FROM Inventarios");
                    //Leer inventarios de la WebAPI y insertarlos en la tabla de SQLite
                    var servicio = new Servicios.ServicioRest();
                    servicio.getInventarios(Contexto);

                    //Eliminar articulos
                    Contexto.EjecutarComando("DELETE FROM Articulos");
                    //Leer articulos de la WebAPI y insertarlos en la tabla de SQLite
                    servicio.getArticulos(Contexto);

                    await DisplayAlert("Datos recibidos", "Se han recibido correctamente los datos.", "Aceptar");
                }
            };

            //Botón enviar hacia el servidor
            btnEnviar.Clicked += async (s, e) =>
            {
                if (!CrossConnectivity.Current.IsConnected)
                {
                    await DisplayAlert("Conexión", "No hay una conexión de internet. Pruebe de nuevo.", "Aceptar");
                    return;
                }

                if (await DisplayAlert("Sincronizar", "¿Desea enviar los datos hacia el servidor?", "Aceptar", "Cancelar"))
                {
                    var servicio = new Servicios.ServicioRest();
                    servicio.setConteos(Contexto);
                    await DisplayAlert("Datos enviados", "Se han enviados correctamente los datos.", "Aceptar");
                }
            };
		}
	}
}
