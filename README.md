# Fibonacci y Torres de Hanói

Aplicación de escritorio en C# y Windows Forms con un único menú de estilo retro.

- **Fibonacci:** calcula recursivamente la serie desde `F(0) = 0` hasta la cantidad de términos elegida (1 a 40). Usa memoria de resultados para evitar recalcular llamadas repetidas.
- **Torres de Hanói:** genera recursivamente los movimientos mínimos para llevar de 1 a 10 discos de la torre A a la C, con la torre B como auxiliar. Muestra el tablero, la lista de movimientos, reproducción automática, pausa, paso a paso y reinicio.

## Abrir y ejecutar

En Windows, abre [`Fibonacci_Hanoi.sln`](Fibonacci_Hanoi.sln) con Visual Studio 2022 y ejecuta el proyecto `Fibonacci_Hanoi`. Requiere el SDK de .NET 8 con soporte de Windows Forms.

Desde PowerShell también puedes ejecutar:

```powershell
dotnet run --project .\Fibonacci_Hanoi\Fibonacci_Hanoi.csproj
```

Para ejecutar las pruebas sin paquetes externos:

```powershell
dotnet run --project .\Fibonacci_Hanoi.Tests\Fibonacci_Hanoi.Tests.csproj
```

El repositorio contiene **código fuente**, no un ejecutable precompilado. Al compilar, Visual Studio genera el ejecutable localmente.
