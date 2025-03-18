<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PBD.Default" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <title>Buscar Equipos de Fútbol</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f9;
            margin: 0;
            padding: 0;
            background-image: url('https://st.depositphotos.com/1007712/3304/v/450/depositphotos_33049973-stock-illustration-soccer-design-element.jpg'); /* Asegúrate de reemplazar con la URL correcta */
            background-size: cover;
            background-position: center;
            background-repeat: no-repeat;
            height: 100vh;
        }

        .container {
            width: 70%;
            margin: 0 auto;
            padding: 20px;
            background-color: rgba(255, 255, 255, 0.8);
            border-radius: 10px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
            margin-top: 50px;
        }

        h2 {
            text-align: center;
            color: #333;
            margin-bottom: 20px;
        }

        label {
            font-weight: bold;
            color: #333;
        }

        input[type="text"],
        input[type="number"],
        select,
        button {
            width: 100%;
            padding: 10px;
            margin: 10px 0 20px 0;
            border: 1px solid #ccc;
            border-radius: 5px;
            font-size: 16px;
        }

        button {
            background-color: #5cb85c;
            color: white;
            cursor: pointer;
            border: none;
        }

        button:hover {
            background-color: #4cae4c;
        }

        table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 30px;
        }

        th, td {
            padding: 10px;
            text-align: center;
            border: 1px solid #ddd;
        }

        th {
            background-color: #007bff;
            color: white;
        }

        tr:nth-child(even) {
            background-color: #f2f2f2;
        }

        tr:hover {
            background-color: #ddd;
        }

        #msgError {
            background-color: #f8d7da;
            color: #721c24;
            padding: 10px;
            border-radius: 5px;
            margin-top: 20px;
            text-align: center;
        }

        #logo {
            position: absolute;
            top: 20px;
            left: 20px;
            width: 100px;
        }
    </style>
</head>
<body>
    <img id="logo" src="https://upload.wikimedia.org/wikipedia/commons/9/92/Logo_tese.jpg" alt="Logo" />

    <form id="form1" runat="server">
        <div class="container">
            <h2>Buscar Equipos de Fútbol</h2>
            <label for="txtBusqueda">Buscar por nombre o ID:</label>
            <input type="text" id="txtBusqueda" runat="server" />
            <button type="submit" runat="server" onserverclick="btnBuscar_Click">Buscar</button>
            <label for="ddlCategoriaFiltro">Filtrar por Categoría:</label>
<asp:DropDownList ID="ddlCategoriaFiltro" runat="server">
    <asp:ListItem Text="Todas" Value="" />
    <asp:ListItem Text="Serie A" Value="Serie A" />
    <asp:ListItem Text="Premier League" Value="Premier League" />
    <asp:ListItem Text="Primera División" Value="Primera División" />
    <asp:ListItem Text="MLS" Value="MLS" />
    <asp:ListItem Text="Bundesliga" Value="Bundesliga" />
    <asp:ListItem Text="Superliga Argentina" Value="Superliga Argentina" />
    <asp:ListItem Text="Ligue 1" Value="Ligue 1" />
    <asp:ListItem Text="Liga MX" Value="Liga MX" />
    <asp:ListItem Text="Brasileirão" Value="Brasileirão" />
</asp:DropDownList>

<button type="button" runat="server" onserverclick="btnFiltrar_Click">Filtrar</button>
            <table id="tblEquipos" border="1" runat="server">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Nombre del Equipo</th>
                        <th>Categoría</th>
                        <th>Puntos Ganados</th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>

            <h2>Agregar Nuevo Equipo</h2>
            <label for="txtNombre">Nombre del Equipo:</label>
            <input type="text" id="txtNombre" runat="server" />

            <label for="ddlCategoria">Categoría:</label>
            <asp:DropDownList ID="ddlCategoria" runat="server">
                <asp:ListItem Text="Serie A" Value="Serie A" />
                <asp:ListItem Text="Premier League" Value="Premier League" />
                <asp:ListItem Text="Primera División" Value="Primera División" />
                <asp:ListItem Text="MLS" Value="MLS" />
                <asp:ListItem Text="Bundesliga" Value="Bundesliga" />
                <asp:ListItem Text="Superliga Argentina" Value="Superliga Argentina" />
                <asp:ListItem Text="Ligue 1" Value="Ligue 1" />
                <asp:ListItem Text="Liga MX" Value="Liga MX" />
                <asp:ListItem Text="Brasileirão" Value="Brasileirão" />
            </asp:DropDownList>

            <label for="txtPuntos">Puntos Ganados:</label>
            <input type="number" id="txtPuntos" runat="server" />

            <button type="button" runat="server" onserverclick="btnAgregarEquipo_Click">Agregar Equipo</button>

            <div id="msgError" runat="server" style="display:none;">
                El ID del equipo ya existe. Intenta con otro ID.
            </div>
        </div>
    </form>
</body>
</html>



