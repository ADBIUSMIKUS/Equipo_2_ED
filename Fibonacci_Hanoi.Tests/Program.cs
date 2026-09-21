using FibonacciHanoi;

var errores = new List<string>();
var total = 0;

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

Probar("Tablero manual: sólo se selecciona el disco superior", () => EjecutarSTA(() =>
{
    using var tablero = new TableroDePrueba { Size = new System.Drawing.Size(600, 360) };
    tablero.Reiniciar(3);
    tablero.Clic(100, 290);
    return tablero.DiscoSeleccionado is null;
}));

Probar("Tablero manual: selección, vista previa y colocación", () => EjecutarSTA(() =>
{
    using var tablero = new TableroDePrueba { Size = new System.Drawing.Size(600, 360) };
    tablero.Reiniciar(3);
    Movimiento? realizado = null;
    tablero.MovimientoManualRealizado += movimiento => realizado = movimiento;
    tablero.Clic(100, 250);
    if (tablero.DiscoSeleccionado != 1)
        return false;
    tablero.Pasar(300, 250);
    if (tablero.TorrePrevisualizada != 1)
        return false;
    tablero.Clic(300, 250);
    return realizado == new Movimiento(1, 'A', 'B')
        && tablero.DiscoSeleccionado is null
        && tablero.TorrePrevisualizada is null;
}));

Probar("Tablero manual: no permite disco grande sobre pequeño", () => EjecutarSTA(() =>
{
    using var tablero = new TableroDePrueba { Size = new System.Drawing.Size(600, 360) };
    tablero.Reiniciar(3);
    var cantidadMovimientos = 0;
    tablero.MovimientoManualRealizado += _ => cantidadMovimientos++;
    tablero.Clic(100, 250);
    tablero.Clic(300, 250);
    tablero.Clic(100, 270);
    tablero.Pasar(300, 250);
    if (tablero.TorrePrevisualizada is not null)
        return false;
    tablero.Clic(300, 250);
    if (cantidadMovimientos != 1 || tablero.DiscoSeleccionado != 2)
        return false;
    tablero.Pasar(500, 250);
    tablero.Clic(500, 250);
    return cantidadMovimientos == 2 && tablero.DiscoSeleccionado is null;
}));

Probar("Tablero manual: al completar y reiniciar se limpia la selección", () => EjecutarSTA(() =>
{
    using var tablero = new TableroDePrueba { Size = new System.Drawing.Size(600, 360) };
    tablero.Reiniciar(1);
    tablero.Clic(100, 290);
    tablero.Pasar(500, 250);
    tablero.Clic(500, 250);
    if (!tablero.EstaResuelto || tablero.TorrePrevisualizada is not null)
        return false;
    tablero.Reiniciar(1);
    return !tablero.EstaResuelto && tablero.DiscoSeleccionado is null;
}));

Console.WriteLine($"Resultado: {total - errores.Count}/{total} pruebas correctas.");
return errores.Count == 0 ? 0 : 1;

void Probar(string nombre, Func<bool> prueba)
{
    total++;
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

static bool EjecutarSTA(Func<bool> prueba)
{
    var correcto = false;
    Exception? error = null;
    var hilo = new Thread(() =>
    {
        try
        {
            correcto = prueba();
        }
        catch (Exception excepcion)
        {
            error = excepcion;
        }
    });
    hilo.SetApartmentState(ApartmentState.STA);
    hilo.Start();
    hilo.Join();
    if (error is not null)
        throw error;
    return correcto;
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

internal sealed class TableroDePrueba : HanoiBoard
{
    public void Clic(int x, int y) =>
        OnMouseClick(new System.Windows.Forms.MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 1, x, y, 0));

    public void Pasar(int x, int y) =>
        OnMouseMove(new System.Windows.Forms.MouseEventArgs(System.Windows.Forms.MouseButtons.None, 0, x, y, 0));
}
