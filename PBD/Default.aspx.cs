using System;
using System.Data;

using System.Web.UI.WebControls;
using Npgsql;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace PBD
{
    public partial class Default : System.Web.UI.Page
    {
        // Cadena de conexión a la base de datos de Heroku
        string connectionString = "Host=c3nv2ev86aje4j.cluster-czrs8kj4isg7.us-east-1.rds.amazonaws.com;Port=5432;Username=utmqlua6rpouu;Password=p21176a28d6816b8b6ec86fac846d943801cb380532e105b05d3175858f62da9e;Database=d3fv1jqg46jhvd;SSL Mode=Require;Trust Server Certificate=true;";

        protected void Page_Load(object sender, EventArgs e)
        {
            // Si es un POST, significa que se hizo una búsqueda
            
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            // Obtener el texto de búsqueda ingresado por el usuario
            string busqueda = txtBusqueda.Value.Trim();

            // Llamar al método para cargar los equipos que coinciden con la búsqueda
            CargarEquipos(busqueda);
        }
        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            string categoria = ddlCategoriaFiltro.SelectedValue;
            CargarEquiposPorCategoria(categoria);
        }

        private void CargarEquiposPorCategoria(string categoria)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT id, nombre_equipo, categoria, puntos_ganados FROM equipos_futbol";

                if (!string.IsNullOrEmpty(categoria))
                {
                    query += " WHERE categoria = @categoria";
                }

                query += " ORDER BY nombre_equipo";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    if (!string.IsNullOrEmpty(categoria))
                    {
                        cmd.Parameters.AddWithValue("@categoria", categoria);
                    }

                    DataTable dt = new DataTable();
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }

                    tblEquipos.Rows.Clear();

                    HtmlTableRow headerRow = new HtmlTableRow();
                    headerRow.Cells.Add(new HtmlTableCell { InnerText = "ID" });
                    headerRow.Cells.Add(new HtmlTableCell { InnerText = "Nombre del Equipo" });
                    headerRow.Cells.Add(new HtmlTableCell { InnerText = "Categoría" });
                    headerRow.Cells.Add(new HtmlTableCell { InnerText = "Puntos Ganados" });

                    tblEquipos.Rows.Add(headerRow);

                    foreach (DataRow row in dt.Rows)
                    {
                        HtmlTableRow tableRow = new HtmlTableRow();

                        tableRow.Cells.Add(new HtmlTableCell { InnerText = row["id"].ToString() });
                        tableRow.Cells.Add(new HtmlTableCell { InnerText = row["nombre_equipo"].ToString() });
                        tableRow.Cells.Add(new HtmlTableCell { InnerText = row["categoria"].ToString() });
                        tableRow.Cells.Add(new HtmlTableCell { InnerText = row["puntos_ganados"].ToString() });

                        tblEquipos.Rows.Add(tableRow);
                    }
                }
            }
        }

        private void CargarEquipos(string busqueda)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                // Abrir la conexión
                conn.Open();

                // Consulta SQL para obtener los equipos filtrados por nombre o id
                string query = "SELECT id, nombre_equipo, categoria, puntos_ganados FROM equipos_futbol WHERE nombre_equipo LIKE @busqueda OR id::TEXT LIKE @busqueda ORDER BY nombre_equipo";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    // Pasar el parámetro de búsqueda de forma segura
                    cmd.Parameters.AddWithValue("@busqueda", "%" + busqueda + "%");

                    // Crear un DataTable para almacenar los resultados
                    DataTable dt = new DataTable();
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Cargar los datos en el DataTable
                        dt.Load(reader);
                    }

                    // Limpiar la tabla existente
                    tblEquipos.Rows.Clear();

                    // Agregar la fila de encabezado (para tabla HTML, usaremos HtmlTableRow)
                    HtmlTableRow headerRow = new HtmlTableRow();
                    headerRow.Cells.Add(new HtmlTableCell { InnerText = "ID" });
                    headerRow.Cells.Add(new HtmlTableCell { InnerText = "Nombre del Equipo" });
                    headerRow.Cells.Add(new HtmlTableCell { InnerText = "Categoría" });
                    headerRow.Cells.Add(new HtmlTableCell { InnerText = "Puntos Ganados" });

                    tblEquipos.Rows.Add(headerRow);

                    // Agregar los registros de equipos a la tabla
                    foreach (DataRow row in dt.Rows)
                    {
                        HtmlTableRow tableRow = new HtmlTableRow();

                        tableRow.Cells.Add(new HtmlTableCell { InnerText = row["id"].ToString() });
                        tableRow.Cells.Add(new HtmlTableCell { InnerText = row["nombre_equipo"].ToString() });
                        tableRow.Cells.Add(new HtmlTableCell { InnerText = row["categoria"].ToString() });
                        tableRow.Cells.Add(new HtmlTableCell { InnerText = row["puntos_ganados"].ToString() });

                        tblEquipos.Rows.Add(tableRow);
                    }
                }
            }
        }
        protected void btnAgregarEquipo_Click(object sender, EventArgs e)
        {
            // Obtener los valores de los campos del formulario
            string nombre = txtNombre.Value;
            string categoria = ddlCategoria.SelectedValue;  // Obtener la categoría seleccionada
            int puntos = int.Parse(txtPuntos.Value);

            // Cadena de conexión a la base de datos
            string connectionString = "Host=c3nv2ev86aje4j.cluster-czrs8kj4isg7.us-east-1.rds.amazonaws.com;Port=5432;Username=utmqlua6rpouu;Password=p21176a28d6816b8b6ec86fac846d943801cb380532e105b05d3175858f62da9e;Database=d3fv1jqg46jhvd;SSL Mode=Require;Trust Server Certificate=true;";

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Comprobar si el equipo con el nombre proporcionado ya existe
                    string checkQuery = "SELECT COUNT(*) FROM equipos_futbol WHERE nombre_equipo = @nombre";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(checkQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        int count = Convert.ToInt32(cmd.ExecuteScalar());

                        if (count > 0)
                        {
                            // El equipo con ese nombre ya existe, muestra el mensaje de error
                            msgError.Visible = true;
                            return;
                        }
                    }

                    // Insertar un nuevo equipo sin especificar el campo "id" (el ID será generado automáticamente)
                    string insertQuery = "INSERT INTO equipos_futbol (nombre_equipo, categoria, puntos_ganados) VALUES (@nombre, @categoria, @puntos)";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(insertQuery, conn))
                    {
                        // Agregar los parámetros a la consulta SQL
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@categoria", categoria);  // Usar el valor seleccionado
                        cmd.Parameters.AddWithValue("@puntos", puntos);

                        // Ejecutar la consulta para insertar los datos
                        cmd.ExecuteNonQuery();
                    }

                    // Limpiar los campos después de agregar el equipo
                    txtNombre.Value = "";
                    ddlCategoria.SelectedIndex = 0;  // Resetear la categoría seleccionada
                    txtPuntos.Value = "";

                    // Opcional: Actualiza la lista de equipos después de agregar uno nuevo
                    CargarEquipos("");  // Puedes pasar "" para cargar todos los equipos, o realizar una búsqueda específica
                }
                catch (Exception ex)
                {
                    // Mostrar el error en caso de que algo falle
                    Response.Write($"Error: {ex.Message}");
                }
            }
        }




        // Método para verificar si el ID ya existe
        private bool VerificarIdExistente(string id)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM equipos_futbol WHERE id = @id";
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0; // Si el count es mayor a 0, el ID ya existe
                }
            }
        }

        // Método para insertar un nuevo equipo
        private void InsertarEquipo(string id, string nombre, string categoria, int puntos)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO equipos_futbol (id, nombre_equipo, categoria, puntos_ganados) VALUES (@id, @nombre, @categoria, @puntos)";
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    // Agregar los parámetros a la consulta SQL
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@categoria", categoria);
                    cmd.Parameters.AddWithValue("@puntos", puntos);

                    // Ejecutar la consulta
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
