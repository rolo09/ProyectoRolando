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
	public partial class ArticuloPage : ContentPage
	{
        public Articulo articulo;

        public ContextoDatos Contexto { get; set; }

        public ArticuloPage (ContextoDatos contexto, Articulo articulo)
		{
            Contexto = contexto;

            InitializeComponent ();

            this.articulo = articulo;
            //Cargar datos a las etiquetas
            lblId.Text = articulo.id_articulo;
            lblDescripcion.Text = articulo.descripcion_articulo;
            lblUltCompra.Text = articulo.ultima_compra.ToString();
            lblUltSalida.Text = articulo.ultima_salida.ToString();
            lblMaximo.Text = articulo.maximo.ToString();
            lblMinimo.Text = articulo.minimo.ToString();

            btnConteo.Clicked += async (s, e) =>
            {
                //Ir a la ventana de conteo
                await Navigation.PushModalAsync(new ConteoFisicoPage(Contexto, this.articulo));
            };

        }
	}
}
