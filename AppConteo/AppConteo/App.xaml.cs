using AppConteo.Modelos;
using AppConteo.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace AppConteo
{
	public partial class App : Application
	{
        public ContextoDatos Contexto { get; set; }

        public App ()
		{
            Contexto = new ContextoDatos();

			InitializeComponent();

			MainPage = new MainPage(Contexto);
            
		}

		protected override void OnStart ()
		{
            //Crear tabla de usuarios
            Contexto.Configurar<Usuario>();

            //Crear tabla de inventarios
            Contexto.Configurar<Inventario>();

            //Crear tabla de artículos
            Contexto.Configurar<Articulo>();

            //Crear tabla de conteos
            Contexto.Configurar<Conteo>();
        }

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
