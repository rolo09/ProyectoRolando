using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;
using AppConteo.Modelos;
using AppConteo.Servicios;
using Plugin.Connectivity;
using System.Diagnostics;

namespace AppConteo
{
	public partial class MainPage : ContentPage
	{
        public ContextoDatos Contexto { get; set; }

        public MainPage(ContextoDatos contexto)
		{
            Contexto = contexto;

			InitializeComponent();

            btnRecibir.Clicked += async (s, e) =>
            {
                if (!CrossConnectivity.Current.IsConnected)
                {
                    await DisplayAlert("Conexión", "No hay una conexión de internet. Pruebe de nuevo.", "Aceptar");
                    return;
                }

                //Eliminar usuarios existentes
                Contexto.EjecutarComando("DELETE FROM Usuarios");
                //Leer usuarios e inventarios de la WebAPI y insertarlos en la tabla de SQLite
                var servicio = new Servicios.ServicioRest();
                servicio.getUsuarios(Contexto);
            };

            btnAceptar.Clicked += async (s, e) =>
            {
                //Revisa la conexión
                if (!CrossConnectivity.Current.IsConnected)
                {
                    await DisplayAlert("Conexión", "No hay una conexión de internet. Pruebe de nuevo.", "Aceptar");
                    return;
                }
  
                //Validar campos
                if (string.IsNullOrEmpty(txtUsuario.Text))
                {
                    await DisplayAlert("Error", "El campo usuario está vacío", "Aceptar");
                    txtUsuario.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtClave.Text))
                {
                    await DisplayAlert("Error", "El campo clave está vacío", "Aceptar");
                    txtClave.Focus();
                    return;
                }

                if (contexto.validarUsuario(txtUsuario.Text, txtClave.Text))
                {
                    await Navigation.PushModalAsync(new Springboard(Contexto));
                }
                else
                {
                    await DisplayAlert("Usuario no válido", "El usuario y/o la contraseña no son válidos", "Aceptar");
                }

            };
		}

    }
}
