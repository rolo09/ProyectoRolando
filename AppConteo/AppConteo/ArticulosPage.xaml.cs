using AppConteo.Modelos;
using AppConteo.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppConteo
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ArticulosPage : ContentPage
	{
        public ContextoDatos Contexto { get; set; }
        public ArticulosPage (ContextoDatos contexto)
		{
            Contexto = contexto;

            InitializeComponent ();

            //Asignar template
            listaArticulos.ItemTemplate = new DataTemplate(typeof(ArticuloCell));
            //Asignar fuente de datos
            listaArticulos.ItemsSource = Contexto.GetArticulos(txtBuscar.Text.Trim());
            //Opción buscar
            txtBuscar.Completed += (s,e) => {
                listaArticulos.ItemsSource = Contexto.GetArticulos(txtBuscar.Text.Trim());
            };
            //Abrir detalle del artículo según artículo seleccionado
            listaArticulos.ItemSelected += async (s, e) => {
                //await DisplayAlert("MENSAJE",e.SelectedItem.ToString(),"Aceptar");
                await Navigation.PushModalAsync(new ArticuloPage(Contexto, (Articulo)e.SelectedItem));
            };

            //Limpiar campo de búsqueda y restablecer listiew
            btnLimpiar.Clicked += (s, e) =>
            {
                txtBuscar.Text = "";
                listaArticulos.ItemsSource = Contexto.GetArticulos(txtBuscar.Text.Trim());
            };

        }
	}
}
