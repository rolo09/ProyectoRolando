using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppConteo
{
    //Formatear el listview de artículos
    public class ArticuloCell : ViewCell
    {
        public ArticuloCell()
        {
            var lblArticulo = new Label
            {
                Font = Font.BoldSystemFontOfSize(NamedSize.Large),
                HorizontalOptions = LayoutOptions.Start
            };
            lblArticulo.SetBinding(Label.TextProperty, new Binding("id_articulo"));

            var lblDescripcion = new Label
            {
                Font = Font.BoldSystemFontOfSize(NamedSize.Medium),
                HorizontalOptions = LayoutOptions.End
            };
            lblDescripcion.SetBinding(Label.TextProperty, new Binding("descripcion_articulo"));

            var panel1 = new StackLayout
            {
                Children = { lblArticulo },
                Orientation = StackOrientation.Horizontal,
            };

            var panel2 = new StackLayout
            {
                Children = { lblDescripcion },
                Orientation = StackOrientation.Horizontal,

            };

            View = new StackLayout
            {
                Children = { panel1, panel2 },
                Orientation = StackOrientation.Vertical,
                Spacing = -5
            };
        }
    }
}
