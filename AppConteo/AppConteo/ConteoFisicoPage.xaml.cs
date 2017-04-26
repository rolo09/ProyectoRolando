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
	public partial class ConteoFisicoPage : ContentPage
	{
        public Articulo articulo;
        public Conteo conteo;
        int cantidad = 0;

        public ContextoDatos Contexto { get; set; }

        public ConteoFisicoPage (ContextoDatos contexto, Articulo articulo)
		{
            Contexto = contexto;

            InitializeComponent ();

            this.articulo = articulo;
            //Cargar campos importantes del artículo
            lblIdArt.Text = articulo.id_articulo;
            lblDescArt.Text = articulo.descripcion_articulo;
            //Cargar fuente de datos al listview de conteos
            listaConteo.ItemsSource = Contexto.GetConteos(articulo.id_articulo);

            //Botón nuevo conteo
            btnNuevoConteo.Clicked += async (sender, events) =>
            {
                if (string.IsNullOrEmpty(txtCantidad.Text))
                {
                    //Validar cantidad
                    await DisplayAlert("Cantidad", "Ingrese una cantidad", "Aceptar");
                    return;
                }

                //Validar que la cantidad sea numérica
                if (!int.TryParse(txtCantidad.Text, out cantidad))
                {
                    await DisplayAlert("Cantidad", "La cantidad debe ser un entero", "Aceptar");
                    return;
                }

                //Nueva instancia de conteo
                conteo = new Conteo();

                //Seteo nuevo conteo
                conteo.id_inventario = articulo.id_inventario;
                conteo.id_articulo = articulo.id_articulo;
                conteo.id_usuario = VariablesGlobales.idusuario;
                conteo.conteo = Convert.ToInt32(txtCantidad.Text);
                conteo.nota = string.IsNullOrEmpty(txtNota.Text) ? "" : txtNota.Text;
                conteo.fecha = DateTime.Now;

                //Guardar el conteo
                Contexto.Guardar(new[] { conteo });

                //Cargar nueva lista de conteo del artículo
                listaConteo.ItemsSource = Contexto.GetConteos(articulo.id_articulo);

                //Limpiar campo
                txtCantidad.Text = string.Empty;
                txtNota.Text = string.Empty;

                //Proceso correcto
                await DisplayAlert("Correcto", "Se ha guardado el nuevo conteo", "Aceptar");
            };

            btnModificarConteo.Clicked += async (s, e) =>
            {
                //Validar que la cantidad no esté vacía
                if (string.IsNullOrEmpty(txtCantidad.Text))
                {
                    await DisplayAlert("Cantidad", "Ingrese una cantidad", "Aceptar");
                    return;
                }

                //Validar que se halla selecciona un conteo
                if (conteo == null)
                {
                    await DisplayAlert("Seleccione", "Debe seleccionar el conteo a modificar", "Aceptar");
                    return;
                }

                //Validar que el conteo sea numérico
                if (!int.TryParse(txtCantidad.Text, out cantidad))
                {
                    await DisplayAlert("Cantidad", "La cantidad debe ser un entero", "Aceptar");
                    return;
                }

                //Setear canmpos variables en la modificación
                conteo.conteo = int.Parse(txtCantidad.Text);
                conteo.nota = txtNota.Text;
                conteo.fecha = DateTime.Now;

                //Actualizar conteo
                Contexto.Actualizar(new[] { conteo } );

                //Refrescar lista
                listaConteo.ItemsSource = Contexto.GetConteos(articulo.id_articulo);

                //Limpiar campos
                txtCantidad.Text = string.Empty;
                txtNota.Text = string.Empty;

                //Proceso correcto
                await DisplayAlert("Correcto", "Se ha actualizado el conteo", "Aceptar");

            };

            btnEliminarConteo.Clicked += async (s, e) =>
            {
                //Validar que el campo cantidad no vaya vacio
                if (string.IsNullOrEmpty(txtCantidad.Text))
                {
                    await DisplayAlert("Cantidad", "Ingrese una cantidad", "Aceptar");
                    return;
                }

                //Validar que se halla seleccionado un conteo
                if (conteo == null)
                {
                    await DisplayAlert("Seleccione", "Debe seleccionar el conteo a eliminar", "Aceptar");
                    return;
                }

                //Validar que la cantidad sea entera
                if (!int.TryParse(txtCantidad.Text, out cantidad))
                {
                    await DisplayAlert("Cantidad", "La cantidad debe ser un entero", "Aceptar");
                    return;
                }

                //Borrar conteo
                Contexto.Borrar(new[] { conteo });

                //Refrescar lista
                listaConteo.ItemsSource = Contexto.GetConteos(articulo.id_articulo);

                //Limpiar campos
                txtCantidad.Text = string.Empty;
                txtNota.Text = string.Empty;

                //Proceso correcto
                await DisplayAlert("Correcto", "Se ha eliminado el conteo", "Aceptar");
            };

            //Seleccionar una registro de la lista
            listaConteo.ItemSelected += (s, e) =>
            {
                conteo = (Conteo)e.SelectedItem;

                txtCantidad.Text = conteo.conteo.ToString();
                txtNota.Text = conteo.nota;
            };

        }

    }
}
