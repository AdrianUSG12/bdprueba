using System;
using System.Web.UI;

namespace PBD
{
    public partial class Resumen : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MostrarResumen();
            }
        }

        private void MostrarResumen()
        {
            // Aquí puedes agregar la lógica para mostrar los productos seleccionados
            // y otros detalles, como el total, etc.
            productosSeleccionados.Text = "Este es el resumen de los productos seleccionados.";

            // Ejemplo de asignar un total
            total.Text = "100.00";
        }
    }
}


