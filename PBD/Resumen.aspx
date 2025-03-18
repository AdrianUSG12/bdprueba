<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Resumen.aspx.cs" Inherits="PBD.Resumen" %>

<!DOCTYPE html>
<html>
<head>
    <title>Resumen de Compra</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 20px;
            background-color: #f4f4f9;
        }
        .container {
            background-color: #fff;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }
        h2 {
            text-align: center;
            color: #333;
        }
        .total {
            font-weight: bold;
            font-size: 1.2em;
            margin-top: 20px;
            text-align: center;
        }
    </style>
</head>
<body>
    <div class="container">
        <h2>Resumen de Compra</h2>
        <!-- Aquí se puede mostrar el contenido de los productos seleccionados -->
        <asp:Literal ID="productosSeleccionados" runat="server"></asp:Literal>

        <div class="total">
            Total: <asp:Label ID="total" runat="server"></asp:Label>
        </div>
    </div>
</body>
</html>

