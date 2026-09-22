using System;
using System.Collections.Generic;

class Program
{
    static int LeerEntero(string mensaje, int min, int max)
    {
        int valor;

        while (true)
        {
            Console.Write(mensaje);
            string entrada = Console.ReadLine() ?? "";

            if (int.TryParse(entrada, out valor))
            {
                if (valor >= min && valor <= max)
                {
                    return valor;
                }
            }

            Console.WriteLine($"[ERROR] Ingrese un número entero entre {min} y {max}.");
        }
    }

    static decimal LeerDecimal(string mensaje, decimal min)
    {
        decimal valor;

        while (true)
        {
            Console.Write(mensaje);
            string entrada = Console.ReadLine() ?? "";

            if (decimal.TryParse(entrada, out valor))
            {
                if (valor >= min)
                {
                    return valor;
                }
            }

            Console.WriteLine($"[ERROR] Ingrese un número decimal mayor o igual a {min:C2}.");
        }
    }

    static decimal CalcularFactura(
        decimal precio,
        int cantidad,
        bool tieneDescuento,
        out decimal montoIva,
        out decimal montoDescuento)
    {
        decimal subtotal = precio * cantidad;

        montoDescuento = tieneDescuento ? subtotal * 0.10m : 0m;

        decimal baseIva = subtotal - montoDescuento;
        montoIva = baseIva * 0.19m;

        return baseIva + montoIva;
    }

    static void ImprimirEncabezado(string titulo)
    {
        Console.Clear();
        Console.WriteLine("====================================================");
        Console.WriteLine($"                 {titulo}");
        Console.WriteLine("====================================================");
    }

    static string LeerNombreProducto()
    {
        while (true)
        {
            Console.Write("Ingrese el nombre del producto: ");
            string nombre = (Console.ReadLine() ?? "").Trim();

            if (!string.IsNullOrWhiteSpace(nombre))
            {
                return nombre;
            }

            Console.WriteLine("[ERROR] El nombre no puede estar vacío.");
        }
    }

    static bool ProductoExiste(List<string> nombres, string nombre)
    {
        foreach (string producto in nombres)
        {
            if (producto.Equals(nombre, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }

    static void RegistrarProducto(
        List<string> nombres,
        List<decimal> precios,
        List<int> stocks,
        List<int> unidadesVendidas)
    {
        ImprimirEncabezado("REGISTRAR NUEVO PRODUCTO");

        string nombre = LeerNombreProducto();

        if (ProductoExiste(nombres, nombre))
        {
            Console.WriteLine("[ERROR] Ya existe un producto con ese nombre.");
            Console.WriteLine("Presione ENTER para continuar...");
            Console.ReadLine();
            return;
        }

        decimal precio = LeerDecimal("Ingrese el precio unitario ($): ", 0.01m);
        int stock = LeerEntero("Ingrese el stock inicial: ", 0, int.MaxValue);

        nombres.Add(nombre);
        precios.Add(precio);
        stocks.Add(stock);
        unidadesVendidas.Add(0);

        Console.WriteLine();
        Console.WriteLine("[OK] Producto registrado correctamente.");
        Console.WriteLine($"Producto: {nombre}");
        Console.WriteLine($"Precio: {precio:C2}");
        Console.WriteLine($"Stock inicial: {stock}");

        Console.WriteLine();
        Console.WriteLine("Presione ENTER para continuar...");
        Console.ReadLine();
    }

    static void MostrarInventario(
        List<string> nombres,
        List<decimal> precios,
        List<int> stocks)
    {
        ImprimirEncabezado("INVENTARIO COMPLETO");

        if (nombres.Count == 0)
        {
            Console.WriteLine("No hay productos registrados en el inventario.");
            Console.WriteLine();
            Console.WriteLine("Presione ENTER para continuar...");
            Console.ReadLine();
            return;
        }

        Console.WriteLine(
            "{0,-5} {1,-30} {2,15} {3,10}   {4}",
            "ID", "PRODUCTO", "PRECIO", "STOCK", "ESTADO");

        Console.WriteLine(new string('-', 80));

        for (int i = 0; i < nombres.Count; i++)
        {
            string estado = stocks[i] < 5 ? "[ALERTA: BAJO STOCK]" : "Disponible";

            Console.WriteLine(
                "{0,-5} {1,-30} {2,15:C2} {3,10}   {4}",
                i + 1,
                nombres[i],
                precios[i],
                stocks[i],
                estado);
        }

        Console.WriteLine();
        Console.WriteLine("Presione ENTER para continuar...");
        Console.ReadLine();
    }

    static void RegistrarVenta(
        List<string> nombres,
        List<decimal> precios,
        List<int> stocks,
        List<int> unidadesVendidas,
        ref int totalVentas,
        ref decimal totalCaja)
    {
        ImprimirEncabezado("REGISTRAR VENTA");

        if (nombres.Count == 0)
        {
            Console.WriteLine("No hay productos registrados.");
            Console.WriteLine("Registre al menos un producto antes de realizar una venta.");
            Console.WriteLine();
            Console.WriteLine("Presione ENTER para continuar...");
            Console.ReadLine();
            return;
        }

        Console.WriteLine("PRODUCTOS DISPONIBLES");
        Console.WriteLine(new string('-', 80));

        for (int i = 0; i < nombres.Count; i++)
        {
            string alerta = stocks[i] < 5 ? " [ALERTA: BAJO STOCK]" : "";

            Console.WriteLine(
                $"{i + 1}. {nombres[i]} | Precio: {precios[i]:C2} | Stock: {stocks[i]}{alerta}");
        }

        Console.WriteLine();

        int productoSeleccionado = LeerEntero(
            $"Seleccione el número del producto a vender (1-{nombres.Count}): ",
            1,
            nombres.Count);

        int indice = productoSeleccionado - 1;

        int cantidad;

        while (true)
        {
            cantidad = LeerEntero(
                $"Ingrese la cantidad a comprar (1-{stocks[indice]}): ",
                1,
                Math.Max(1, stocks[indice]));

            if (cantidad <= stocks[indice])
            {
                break;
            }

            Console.WriteLine(
                $"[ERROR] Stock insuficiente. Solo quedan {stocks[indice]} unidades en inventario.");
        }

        bool tieneDescuento = false;

        while (true)
        {
            Console.Write("¿Aplica descuento de cliente frecuente (10%)? (S/N): ");
            string respuesta = (Console.ReadLine() ?? "").Trim().ToUpper();

            if (respuesta == "S")
            {
                tieneDescuento = true;
                break;
            }

            if (respuesta == "N")
            {
                tieneDescuento = false;
                break;
            }

            Console.WriteLine("[ERROR] Responda únicamente S o N.");
        }

        decimal subtotal = precios[indice] * cantidad;

        decimal montoIva;
        decimal montoDescuento;

        decimal total = CalcularFactura(
            precios[indice],
            cantidad,
            tieneDescuento,
            out montoIva,
            out montoDescuento);

        stocks[indice] -= cantidad;
        unidadesVendidas[indice] += cantidad;

        totalVentas++;
        totalCaja += total;

        Console.WriteLine();
        Console.WriteLine("====================================================");
        Console.WriteLine("                  TICKET DE VENTA");
        Console.WriteLine("====================================================");
        Console.WriteLine($"Producto:              {nombres[indice]} (x{cantidad})");
        Console.WriteLine($"Subtotal:              {subtotal:C2}");
        Console.WriteLine($"Descuento (10%):      - {montoDescuento:C2}");
        Console.WriteLine($"IVA (19%):            + {montoIva:C2}");
        Console.WriteLine("----------------------------------------------------");
        Console.WriteLine($"TOTAL A PAGAR:         {total:C2}");
        Console.WriteLine("====================================================");
        Console.WriteLine($"[OK] Venta efectuada con éxito. Stock actualizado: {stocks[indice]} unidades.");

        Console.WriteLine();
        Console.WriteLine("Presione ENTER para continuar...");
        Console.ReadLine();
    }

    static void MostrarReporte(
        List<string> nombres,
        List<int> unidadesVendidas,
        int totalVentas,
        decimal totalCaja)
    {
        ImprimirEncabezado("REPORTE DE CAJA Y ESTADÍSTICAS");

        decimal promedioVenta = totalVentas > 0 ? totalCaja / totalVentas : 0m;

        Console.WriteLine($"Total de ventas realizadas:       {totalVentas}");
        Console.WriteLine($"Total acumulado en caja:          {totalCaja:C2}");
        Console.WriteLine($"Promedio de dinero por venta:     {promedioVenta:C2}");
        Console.WriteLine();

        if (nombres.Count == 0)
        {
            Console.WriteLine("Producto con mayor cantidad de unidades vendidas: No hay productos.");
        }
        else
        {
            int indiceMayor = 0;

            for (int i = 1; i < unidadesVendidas.Count; i++)
            {
                if (unidadesVendidas[i] > unidadesVendidas[indiceMayor])
                {
                    indiceMayor = i;
                }
            }

            Console.WriteLine(
                $"Producto con mayor cantidad de unidades vendidas: {nombres[indiceMayor]}");
            Console.WriteLine(
                $"Unidades vendidas: {unidadesVendidas[indiceMayor]}");
        }

        Console.WriteLine();
        Console.WriteLine("Presione ENTER para continuar...");
        Console.ReadLine();
    }

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        List<string> nombres = new List<string>();
        List<decimal> precios = new List<decimal>();
        List<int> stocks = new List<int>();
        List<int> unidadesVendidas = new List<int>();

        int totalVentas = 0;
        decimal totalCaja = 0m;
        int opcion;

        do
        {
            ImprimirEncabezado("SISTEMA GESTOR DE VENTAS E INVENTARIO (MINI-POS)");

            Console.WriteLine("1. Registrar nuevo producto en inventario");
            Console.WriteLine("2. Consultar inventario completo");
            Console.WriteLine("3. Registrar una venta");
            Console.WriteLine("4. Ver reporte de caja y estadísticas diarias");
            Console.WriteLine("5. Salir");
            Console.WriteLine();

            opcion = LeerEntero("Seleccione una opción (1-5): ", 1, 5);

            switch (opcion)
            {
                case 1:
                    RegistrarProducto(
                        nombres,
                        precios,
                        stocks,
                        unidadesVendidas);
                    break;

                case 2:
                    MostrarInventario(
                        nombres,
                        precios,
                        stocks);
                    break;

                case 3:
                    RegistrarVenta(
                        nombres,
                        precios,
                        stocks,
                        unidadesVendidas,
                        ref totalVentas,
                        ref totalCaja);
                    break;

                case 4:
                    MostrarReporte(
                        nombres,
                        unidadesVendidas,
                        totalVentas,
                        totalCaja);
                    break;

                case 5:
                    ImprimirEncabezado("SALIR");
                    Console.WriteLine("Gracias por utilizar el Sistema Gestor de Ventas e Inventario.");
                    Console.WriteLine("Programa finalizado correctamente.");
                    break;
            }

        } while (opcion != 5);
    }
}
