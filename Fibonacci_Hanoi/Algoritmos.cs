namespace FibonacciHanoi;

public readonly record struct Movimiento(int Disco, char Origen, char Destino)
{
    public override string ToString() => $"Disco {Disco}: {Origen} → {Destino}";
}

public static class Algoritmos
{
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
