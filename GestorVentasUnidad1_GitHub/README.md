# Sistema Gestor de Ventas e Inventario Express (Mini-POS)

## Información del estudiante

**Nombre:** Steven Andres Agamez Orozco  
**Programa:** Ingeniería en Sistemas  
**Tecnología:** C# / .NET 8

## Descripción

Aplicación de consola desarrollada como solución al Reto Final de la Unidad 1 de Fundamentos de C# (.NET 8).

El sistema permite:

1. Registrar productos en el inventario.
2. Consultar el inventario completo.
3. Registrar ventas.
4. Aplicar descuento de cliente frecuente del 10%.
5. Calcular IVA del 19%.
6. Actualizar automáticamente el stock.
7. Mostrar un ticket de venta.
8. Consultar el reporte de caja y estadísticas de la sesión.

El proyecto utiliza únicamente conceptos correspondientes a la Unidad 1: variables, tipos primitivos, listas, estructuras de control, métodos estáticos y validaciones mediante `TryParse`.

## Requisitos

- .NET 8 SDK
- Visual Studio 2022 con soporte para .NET 8, o una terminal con el SDK de .NET 8.

## Cómo ejecutar el proyecto

### Opción 1: Visual Studio

1. Descarga o clona este repositorio.
2. Abre Visual Studio.
3. Selecciona **Open a project or solution**.
4. Abre el archivo `GestorVentasUnidad1.csproj`.
5. Ejecuta el proyecto con **Ctrl + F5** o **F5**.

### Opción 2: Terminal

Clona el repositorio:

```bash
git clone URL_DEL_REPOSITORIO
```

Entra en la carpeta:

```bash
cd GestorVentasUnidad1
```

Ejecuta:

```bash
dotnet run
```

También puedes comprobar que el proyecto compile correctamente con:

```bash
dotnet build
```

## Estructura del proyecto

```text
GestorVentasUnidad1/
│
├── GestorVentasUnidad1.csproj
├── Program.cs
├── README.md
└── .gitignore
```

## Funcionalidades

### 1. Registrar producto

Solicita:

- Nombre.
- Precio unitario.
- Stock inicial.

No permite nombres vacíos ni productos duplicados.

### 2. Consultar inventario

Muestra:

- ID.
- Nombre.
- Precio.
- Stock.
- Alerta cuando existen menos de 5 unidades.

### 3. Registrar venta

Permite seleccionar un producto y una cantidad.

El sistema valida que exista stock suficiente y calcula:

```text
Subtotal = Precio × Cantidad
Descuento = 10% del subtotal si aplica
IVA = (Subtotal - Descuento) × 19%
Total = Subtotal - Descuento + IVA
```

Después de una venta, el stock se actualiza automáticamente.

### 4. Reporte

Muestra:

- Cantidad de ventas realizadas.
- Dinero acumulado en caja.
- Promedio de dinero por venta.
- Producto con mayor cantidad de unidades vendidas.

## Métodos obligatorios implementados

El archivo `Program.cs` contiene los cuatro métodos solicitados:

```csharp
static int LeerEntero(string mensaje, int min, int max)

static decimal LeerDecimal(string mensaje, decimal min)

static decimal CalcularFactura(
    decimal precio,
    int cantidad,
    bool tieneDescuento,
    out decimal montoIva,
    out decimal montoDescuento)

static void ImprimirEncabezado(string titulo)
```

## Ejemplo de prueba

Una prueba básica puede realizarse así:

1. Registrar `Café Colombiano 500g`.
2. Precio: `18000`.
3. Stock: `10`.
4. Registrar una venta de `2` unidades.
5. Aplicar descuento de cliente frecuente.
6. Verificar que el stock pase de `10` a `8`.
7. Consultar el reporte de caja.

## GitHub

Este proyecto está preparado para ser publicado en un repositorio público de GitHub.

No se deben subir las carpetas `bin/` ni `obj/`, ya que están excluidas mediante `.gitignore`.
