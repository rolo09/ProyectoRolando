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

            btnArticulos.Clicked += (s, e) => {
                Navigation.PushModalAsync(new ArticulosPage(Contexto));
            };

            btnSincronizar.Clicked += async (s, e) =>
            {
                if (!CrossConnectivity.Current.IsConnected)
                {
                    await DisplayAlert("Conexión", "No hay una conexión de internet. Pruebe de nuevo.", "Aceptar");
                    return;
                }

                if (await DisplayAlert("Sincronizar", "¿Desea sincronizar los datos?", "Aceptar", "Cancelar"))
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
                }
            };
		}
	}
}
