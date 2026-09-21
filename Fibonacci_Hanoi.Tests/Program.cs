using FibonacciHanoi;

var errores = new List<string>();

Probar("Fibonacci: 10 términos conocidos", () =>
    Algoritmos.Fibonacci(10).SequenceEqual(new long[] { 0, 1, 1, 2, 3, 5, 8, 13, 21, 34 }));
Probar("Fibonacci: 40 términos", () =>
    Algoritmos.Fibonacci(40)[^1] == 63_245_986);
Probar("Fibonacci: límite inferior", () =>
    Lanza<ArgumentOutOfRangeException>(() => Algoritmos.Fibonacci(0)));
Probar("Fibonacci: límite superior", () =>
    Lanza<ArgumentOutOfRangeException>(() => Algoritmos.Fibonacci(41)));

for (var cantidad = 1; cantidad <= 10; cantidad++)
{
    var discos = cantidad;
    Probar($"Hanói: {discos} disco(s), movimientos legales y mínimos", () => ValidarHanoi(discos));
}

Probar("Hanói: límite inferior", () =>
    Lanza<ArgumentOutOfRangeException>(() => Algoritmos.Hanoi(0)));
Probar("Hanói: límite superior", () =>
    Lanza<ArgumentOutOfRangeException>(() => Algoritmos.Hanoi(11)));

Console.WriteLine($"Resultado: {16 - errores.Count}/16 pruebas correctas.");
return errores.Count == 0 ? 0 : 1;

void Probar(string nombre, Func<bool> prueba)
{
    try
    {
        var correcto = prueba();
        Console.WriteLine($"{(correcto ? "PASS" : "FAIL")} {nombre}");
        if (!correcto)
            errores.Add(nombre);
    }
    catch (Exception error)
    {
        Console.WriteLine($"FAIL {nombre}: {error.Message}");
        errores.Add(nombre);
    }
}

static bool Lanza<T>(Action accion) where T : Exception
{
    try
    {
        accion();
        return false;
    }
    catch (T)
    {
        return true;
    }
}

static bool ValidarHanoi(int cantidad)
{
    var movimientos = Algoritmos.Hanoi(cantidad);
    if (movimientos.Count != (1 << cantidad) - 1)
        return false;

    var torres = new Dictionary<char, Stack<int>>
    {
        ['A'] = new(),
        ['B'] = new(),
        ['C'] = new()
    };
    for (var disco = cantidad; disco >= 1; disco--)
        torres['A'].Push(disco);

    foreach (var movimiento in movimientos)
    {
        var origen = torres[movimiento.Origen];
        var destino = torres[movimiento.Destino];
        if (origen.Count == 0 || origen.Peek() != movimiento.Disco)
            return false;
        if (destino.Count > 0 && destino.Peek() < movimiento.Disco)
            return false;
        destino.Push(origen.Pop());
    }

    return torres['A'].Count == 0 && torres['B'].Count == 0 && torres['C'].Count == cantidad;
}
