using System.Numerics;

namespace FibonacciHanoi;

public readonly record struct Movimiento(int Disco, char Origen, char Destino)
{
    public override string ToString() => $"Disco {Disco}: {Origen} → {Destino}";
}

public static class Algoritmos
{
    public static BigInteger Factorial(int n)
    {
        if (n is < 0 or > 100)
            throw new ArgumentOutOfRangeException(nameof(n), "El número debe ser un entero entre 0 y 100.");

        return FactorialRecursivo(n);
    }

    private static BigInteger FactorialRecursivo(int n)
    {
        if (n <= 1)
            return 1;

        return n * FactorialRecursivo(n - 1);
    }

    public static (BigInteger Resultado, IReadOnlyList<string> Pasos) FactorialConPasos(int n)
    {
        if (n is < 0 or > 100)
            throw new ArgumentOutOfRangeException(nameof(n), "El número debe ser un entero entre 0 y 100.");

        var pasos = new List<string>();
        var resultado = FactorialConTrazaRecursiva(n, 0, pasos);
        return (resultado, pasos);
    }

    private static BigInteger FactorialConTrazaRecursiva(int n, int nivel, List<string> pasos)
    {
        var sangria = new string(' ', nivel * 3);
        if (n <= 1)
        {
            pasos.Add($"{sangria}Caso base: Factorial({n}) = 1");
            return 1;
        }

        pasos.Add($"{sangria}Llamada recursiva: Factorial({n}) = {n} × Factorial({n - 1})");
        var subResultado = FactorialConTrazaRecursiva(n - 1, nivel + 1, pasos);
        var total = n * subResultado;
        pasos.Add($"{sangria}Retorno resuelto: {n} × Factorial({n - 1}) = {total:N0}");
        return total;
    }

    public static IReadOnlyList<long> Fibonacci(int cantidad)
    {
        if (cantidad is < 1 or > 40)
            throw new ArgumentOutOfRangeException(nameof(cantidad), "Elige entre 1 y 40 términos.");

        var memoria = new Dictionary<int, long>();
        var resultado = new List<long>(cantidad);
        for (var n = 0; n < cantidad; n++)
            resultado.Add(FibonacciRecursivo(n, memoria));

        return resultado;
    }

    private static long FibonacciRecursivo(int n, IDictionary<int, long> memoria)
    {
        if (n <= 1)
            return n;

        if (memoria.TryGetValue(n, out var valor))
            return valor;

        valor = FibonacciRecursivo(n - 1, memoria) + FibonacciRecursivo(n - 2, memoria);
        memoria[n] = valor;
        return valor;
    }

    public static IReadOnlyList<Movimiento> Hanoi(int discos)
    {
        if (discos is < 1 or > 10)
            throw new ArgumentOutOfRangeException(nameof(discos), "Elige entre 1 y 10 discos.");

        var movimientos = new List<Movimiento>((1 << discos) - 1);
        MoverRecursivo(discos, 'A', 'C', 'B', movimientos);
        return movimientos;
    }

    private static void MoverRecursivo(int n, char origen, char destino, char auxiliar, ICollection<Movimiento> movimientos)
    {
        if (n == 0)
            return;

        MoverRecursivo(n - 1, origen, auxiliar, destino, movimientos);
        movimientos.Add(new Movimiento(n, origen, destino));
        MoverRecursivo(n - 1, auxiliar, destino, origen, movimientos);
    }
}
