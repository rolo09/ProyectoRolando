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
using System.Net;
using System.ComponentModel;

namespace AppConteo
{
	public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        int idusuario = 0;
        public ContextoDatos Contexto { get; set; }

        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }

            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        public MainPage(ContextoDatos contexto)
		{
            Contexto = contexto;

			InitializeComponent();

            IsLoading = false;

            btnRecibir.Clicked += async (s, e) =>
            {
                //Corroborar la conexión de internet
                if (!CrossConnectivity.Current.IsConnected)
                {
                    await DisplayAlert("Conexión", "No hay una conexión de internet. Pruebe de nuevo.", "Aceptar");
                    return;
                }

                IsLoading = true;

                //Eliminar usuarios existentes
                Contexto.EjecutarComando("DELETE FROM Usuarios");
                //Leer usuarios e inventarios de la WebAPI y insertarlos en la tabla de SQLite
                var servicio = new Servicios.ServicioRest();
                servicio.getUsuarios(Contexto);

                IsLoading = false;

                await DisplayAlert("Usuarios recibidos", "Se han recibido correctamtente los usuarios.", "Aceptar");
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

                idusuario = contexto.validarUsuario(txtUsuario.Text, txtClave.Text);
                if (idusuario!=0)
                {
                    //Variable global con el id del usuario que ingresó
                    VariablesGlobales.idusuario = idusuario;
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
