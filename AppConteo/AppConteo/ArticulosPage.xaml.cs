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

            listaArticulos.ItemTemplate = new DataTemplate(typeof(ArticuloCell));
            
            listaArticulos.ItemsSource = Contexto.GetArticulos(txtBuscar.Text.Trim());

            txtBuscar.Completed += (s,e) => {
                listaArticulos.ItemsSource = Contexto.GetArticulos(txtBuscar.Text.Trim());
            };

            listaArticulos.ItemSelected += async (s, e) => {
                //await DisplayAlert("MENSAJE",e.SelectedItem.ToString(),"Aceptar");
                await Navigation.PushModalAsync(new ArticuloPage(Contexto, (Articulo)e.SelectedItem));
            };

            btnLimpiar.Clicked += (s, e) =>
            {
                txtBuscar.Text = "";
                listaArticulos.ItemsSource = Contexto.GetArticulos(txtBuscar.Text.Trim());
            };

        }
	}
}
