using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace AppConteo.Droid
{
	[Activity (Label = "AppConteo", Icon = "@drawable/icon", Theme="@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar; 

			base.OnCreate (bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);

            var app = new App();

            app.Contexto.RutaConexion =
                System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            app.Contexto.RutaConexion += "/inventario.db3";

            app.Contexto.NuevaConexion = () => new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(), app.Contexto.RutaConexion, false);

            LoadApplication(app);
		}
	}
}

