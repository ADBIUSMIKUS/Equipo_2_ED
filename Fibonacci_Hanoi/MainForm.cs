namespace FibonacciHanoi;

internal static class Retro
{
    public static readonly Color Fondo = Color.FromArgb(10, 13, 29);
    public static readonly Color FondoMenu = Color.FromArgb(15, 19, 39);
    public static readonly Color FondoTarjeta = Color.FromArgb(23, 29, 54);
    public static readonly Color Cian = Color.FromArgb(44, 225, 248);
    public static readonly Color Rosa = Color.FromArgb(255, 79, 176);
    public static readonly Color Amarillo = Color.FromArgb(255, 221, 72);
    public static readonly Color Verde = Color.FromArgb(0, 255, 163);
    public static readonly Color Rojo = Color.FromArgb(255, 92, 119);
    public static readonly Color Texto = Color.FromArgb(243, 246, 255);
    public static readonly Color Secundario = Color.FromArgb(163, 175, 204);

    public static Font Titulo(float tamaño) => new("Consolas", tamaño, FontStyle.Bold);
    public static Font Cuerpo(float tamaño, FontStyle estilo = FontStyle.Regular) => new("Segoe UI", tamaño, estilo);
}

public sealed class MainForm : Form
{
    private readonly Panel _contenido = new() { Dock = DockStyle.Fill, BackColor = Retro.Fondo };
    private readonly System.Windows.Forms.Timer _reproductor = new() { Interval = 420 };
    private IReadOnlyList<Movimiento> _movimientos = [];
    private int _siguiente;
    private bool _modoManual;
    private HanoiBoard? _tablero;
    private ListBox? _lista;
    private Label? _estado;
    private Button? _iniciar;
    private TextBox? _txtNumeroFactorial;
    private Button? _btnCalcularFactorial;

    public bool MostrarPopups { get; set; } = true;

    public void SimularFactorial(string valor)
    {
        MostrarFactorial();
        if (_txtNumeroFactorial is not null)
            _txtNumeroFactorial.Text = valor;
        _btnCalcularFactorial?.PerformClick();
    }

    public MainForm()
    {
        Text = "Recursión Arcade | Factorial, Fibonacci y Torres de Hanói";
        Size = new Size(1220, 760);
        MinimumSize = new Size(1050, 680);
        StartPosition = FormStartPosition.CenterScreen;
        BackColor = Retro.Fondo;
        ForeColor = Retro.Texto;
        Font = Retro.Cuerpo(10);

        var menu = new Panel { Dock = DockStyle.Left, Width = 220, BackColor = Retro.FondoMenu, Padding = new Padding(18, 24, 18, 20) };
        var marca = new Label
        {
            Text = "RECURSIÓN\nARCADE",
            Dock = DockStyle.Top,
            Height = 110,
            Font = Retro.Titulo(20),
            ForeColor = Retro.Cian,
            TextAlign = ContentAlignment.MiddleLeft
        };
        var navegacion = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            Height = 280,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false
        };
        navegacion.Controls.AddRange([
            BotonMenu("INICIO", Retro.Cian, MostrarInicio),
            BotonMenu("01 · FACTORIAL", Retro.Verde, MostrarFactorial),
            BotonMenu("02 · FIBONACCI", Retro.Amarillo, MostrarFibonacci),
            BotonMenu("05 · HANÓI", Retro.Rosa, MostrarHanoi)
        ]);
        var pie = new Label
        {
            Text = "C# · WINDOWS FORMS\nMODO RECURSIVO",
            Dock = DockStyle.Bottom,
            Height = 48,
            Font = Retro.Titulo(9),
            ForeColor = Retro.Secundario
        };
        menu.Controls.Add(pie);
        menu.Controls.Add(navegacion);
        menu.Controls.Add(marca);
        Controls.Add(_contenido);
        Controls.Add(menu);

        _reproductor.Tick += (_, _) => SiguientePaso();
        MostrarInicio();
    }

    private static Button BotonMenu(string texto, Color acento, Action accion)
    {
        var boton = Boton(texto, acento);
        boton.Width = 182;
        boton.Height = 50;
        boton.Margin = new Padding(0, 0, 0, 10);
        boton.Click += (_, _) => accion();
        return boton;
    }

    private static Button Boton(string texto, Color acento)
    {
        var boton = new Button
        {
            Text = texto,
            BackColor = Retro.FondoTarjeta,
            ForeColor = Retro.Texto,
            FlatStyle = FlatStyle.Flat,
            Font = Retro.Cuerpo(9.5f, FontStyle.Bold),
            Cursor = Cursors.Hand,
            Height = 46
        };
        boton.FlatAppearance.BorderColor = acento;
        boton.FlatAppearance.BorderSize = 2;
        return boton;
    }

    private static Label Etiqueta(string texto, Color color, float tamaño, int alto) => new()
    {
        Text = texto,
        ForeColor = color,
        Font = Retro.Cuerpo(tamaño, FontStyle.Bold),
        Dock = DockStyle.Top,
        Height = alto,
        TextAlign = ContentAlignment.MiddleLeft
    };

    private static Panel Tarjeta() => new()
    {
        Dock = DockStyle.Fill,
        BackColor = Retro.FondoTarjeta,
        Padding = new Padding(25)
    };

    private static NumericUpDown Numero(int inicial, int maximo) => new()
    {
        Minimum = 1,
        Maximum = maximo,
        Value = inicial,
        BackColor = Retro.FondoMenu,
        ForeColor = Retro.Texto,
        Font = Retro.Titulo(15),
        TextAlign = HorizontalAlignment.Center,
        Width = 100,
        Height = 38
    };

    private Panel Preparar(string titulo, string descripcion)
    {
        _reproductor.Stop();
        _contenido.Controls.Clear();
        var pagina = new Panel { Dock = DockStyle.Fill, Padding = new Padding(30, 24, 30, 25) };
        var cabecera = new Panel { Dock = DockStyle.Top, Height = 100 };
        cabecera.Controls.Add(Etiqueta(descripcion, Retro.Secundario, 10, 32));
        cabecera.Controls.Add(new Label
        {
            Text = titulo,
            Dock = DockStyle.Top,
            Height = 58,
            ForeColor = Retro.Texto,
            Font = Retro.Titulo(22),
            TextAlign = ContentAlignment.MiddleLeft
        });
        pagina.Controls.Add(cabecera);
        _contenido.Controls.Add(pagina);
        return pagina;
    }

    private void MostrarInicio()
    {
        var pagina = Preparar("SELECCIONA UNA MISIÓN", "Problemas clásicos resueltos mediante algoritmos recursivos.");
        var opciones = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 3, RowCount = 1, Padding = new Padding(0, 8, 0, 16) };
        opciones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));
        opciones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));
        opciones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34f));
        opciones.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        opciones.Controls.Add(TarjetaInicio("01", "FACTORIAL", "Calcula n! recursivamente con validaciones y traza de llamadas.", Retro.Verde, MostrarFactorial), 0, 0);
        opciones.Controls.Add(TarjetaInicio("02", "FIBONACCI", "Genera los términos de la sucesión con una función recursiva.", Retro.Amarillo, MostrarFibonacci), 1, 0);
        opciones.Controls.Add(TarjetaInicio("05", "TORRES DE HANÓI", "Descubre cada movimiento y observa cómo cambia el tablero.", Retro.Rosa, MostrarHanoi), 2, 0);
        pagina.Controls.Add(opciones);
        opciones.BringToFront();
    }

    private static Panel TarjetaInicio(string numero, string nombre, string descripcion, Color acento, Action accion)
    {
        var panel = Tarjeta();
        panel.Margin = new Padding(8);
        panel.Controls.Add(new Label { Text = descripcion, Dock = DockStyle.Top, Height = 100, ForeColor = Retro.Secundario, Font = Retro.Cuerpo(11) });
        panel.Controls.Add(Etiqueta(nombre, Retro.Texto, 20, 52));
        panel.Controls.Add(new Label { Text = numero, Dock = DockStyle.Top, Height = 90, ForeColor = acento, Font = Retro.Titulo(38) });
        var entrar = Boton("ABRIR EJERCICIO ▶", acento);
        entrar.Dock = DockStyle.Bottom;
        entrar.Click += (_, _) => accion();
        panel.Controls.Add(entrar);
        return panel;
    }

    private void MostrarFactorial()
    {
        var pagina = Preparar("01 · FACTORIAL", "n! = n × (n - 1)!, con 0! = 1. Función recursiva con trazabilidad de llamadas.");
        var columnas = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1 };
        columnas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 36));
        columnas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 64));
        columnas.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        var pnlEntrada = Tarjeta();
        pnlEntrada.Margin = new Padding(0, 0, 10, 0);

        var lblInstruccion = Etiqueta("INGRESA UN NÚMERO (n ≥ 0)", Retro.Verde, 11, 40);

        var txtNumero = new TextBox
        {
            BackColor = Retro.FondoMenu,
            ForeColor = Retro.Texto,
            BorderStyle = BorderStyle.FixedSingle,
            Font = Retro.Titulo(15),
            TextAlign = HorizontalAlignment.Center,
            Dock = DockStyle.Top,
            Height = 38
        };

        var pnlBotones = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            Height = 58,
            WrapContents = false,
            Padding = new Padding(0, 10, 0, 0)
        };

        var btnCalcular = Boton("CALCULAR FACTORIAL", Retro.Verde);
        btnCalcular.Width = 180;

        var btnLimpiar = Boton("LIMPIAR", Retro.Secundario);
        btnLimpiar.Width = 90;
        btnLimpiar.Margin = new Padding(10, 0, 0, 0);

        pnlBotones.Controls.AddRange([btnCalcular, btnLimpiar]);

        var lblEstado = new Label
        {
            Dock = DockStyle.Top,
            Height = 65,
            ForeColor = Retro.Secundario,
            Font = Retro.Cuerpo(9.5f),
            TextAlign = ContentAlignment.MiddleLeft,
            Text = "Ingresa un número entero y pulsa CALCULAR."
        };

        var lblReglas = new Label
        {
            Dock = DockStyle.Bottom,
            Height = 110,
            ForeColor = Retro.Secundario,
            Font = Retro.Cuerpo(9),
            Text = "• Rango recomendado: 0 a 100.\n• Caso base: 0! = 1\n• Caso recursivo: n! = n × (n - 1)!\n• Soporta números grandes con BigInteger sin desbordamiento."
        };

        pnlEntrada.Controls.Add(lblReglas);
        pnlEntrada.Controls.Add(lblEstado);
        pnlEntrada.Controls.Add(pnlBotones);
        pnlEntrada.Controls.Add(txtNumero);
        pnlEntrada.Controls.Add(lblInstruccion);

        var pnlSalida = Tarjeta();
        pnlSalida.Margin = new Padding(10, 0, 0, 0);

        var lblTituloResultado = Etiqueta("RESULTADO", Retro.Verde, 12, 35);
        var lblResultado = new TextBox
        {
            Dock = DockStyle.Top,
            Height = 48,
            ReadOnly = true,
            Multiline = true,
            BorderStyle = BorderStyle.None,
            BackColor = Retro.FondoMenu,
            ForeColor = Retro.Verde,
            Font = Retro.Titulo(12),
            Text = "Esperando cálculo..."
        };

        var lblTituloTraza = Etiqueta("TRAZA Y LLAMADAS RECURSIVAS", Retro.Cian, 11, 35);
        lblTituloTraza.Dock = DockStyle.Top;

        var txtPasos = new TextBox
        {
            Multiline = true,
            ReadOnly = true,
            ScrollBars = ScrollBars.Both,
            Dock = DockStyle.Fill,
            BackColor = Retro.FondoMenu,
            ForeColor = Retro.Texto,
            BorderStyle = BorderStyle.None,
            Font = Retro.Titulo(10.5f),
            Text = "Aquí se mostrará el árbol y retorno de cada llamada recursiva."
        };

        pnlSalida.Controls.Add(txtPasos);
        pnlSalida.Controls.Add(lblTituloTraza);
        pnlSalida.Controls.Add(lblResultado);
        pnlSalida.Controls.Add(lblTituloResultado);

        _txtNumeroFactorial = txtNumero;
        _btnCalcularFactorial = btnCalcular;

        btnCalcular.Click += (_, _) =>
        {
            var texto = txtNumero.Text?.Trim();

            if (string.IsNullOrWhiteSpace(texto))
            {
                lblEstado.ForeColor = Retro.Rojo;
                lblEstado.Text = "⚠ Hay datos faltantes. Por favor introduce un número.";
                if (MostrarPopups) MessageBox.Show("Hay datos faltantes", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNumero.Focus();
                return;
            }

            if (!int.TryParse(texto, out var n))
            {
                lblEstado.ForeColor = Retro.Rojo;
                lblEstado.Text = "⚠ Introducir sólo números enteros válidos.";
                if (MostrarPopups) MessageBox.Show("Introducir sólo números", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNumero.SelectAll();
                txtNumero.Focus();
                return;
            }

            if (n < 0)
            {
                lblEstado.ForeColor = Retro.Rojo;
                lblEstado.Text = "⚠ El número debe ser un entero mayor o igual a cero.";
                if (MostrarPopups) MessageBox.Show("El número debe ser un entero mayor o igual a cero.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNumero.SelectAll();
                txtNumero.Focus();
                return;
            }

            if (n > 100)
            {
                lblEstado.ForeColor = Retro.Rojo;
                lblEstado.Text = "⚠ Por favor ingresa un número menor o igual a 100.";
                if (MostrarPopups) MessageBox.Show("Para evitar saturación de llamadas recursivas, introduce un número entre 0 y 100.", "Límite sugerido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                var (resultado, pasos) = Algoritmos.FactorialConPasos(n);
                lblResultado.Text = $"{n}! = {resultado:N0}";
                txtPasos.Text = string.Join(Environment.NewLine, pasos);
                lblEstado.ForeColor = Retro.Verde;
                lblEstado.Text = $"✔ ¡Cálculo exitoso! Se ejecutaron {n + 1} llamadas recursivas.";
            }
            catch (Exception ex)
            {
                lblEstado.ForeColor = Retro.Rojo;
                lblEstado.Text = $"Error: {ex.Message}";
                MessageBox.Show($"Error durante el cálculo: {ex.Message}", "Excepción", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        };

        btnLimpiar.Click += (_, _) =>
        {
            txtNumero.Clear();
            lblResultado.Text = "Esperando cálculo...";
            txtPasos.Text = "Aquí se mostrará el árbol y retorno de cada llamada recursiva.";
            lblEstado.ForeColor = Retro.Secundario;
            lblEstado.Text = "Ingresa un número entero y pulsa CALCULAR.";
            txtNumero.Focus();
        };

        columnas.Controls.Add(pnlEntrada, 0, 0);
        columnas.Controls.Add(pnlSalida, 1, 0);
        pagina.Controls.Add(columnas);
        columnas.BringToFront();
    }

    private void MostrarFibonacci()
    {
        var pagina = Preparar("02 · FIBONACCI", "F(n) = F(n - 1) + F(n - 2), con F(0) = 0 y F(1) = 1.");
        var columnas = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1 };
        columnas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34));
        columnas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66));
        columnas.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        var entrada = Tarjeta();
        entrada.Margin = new Padding(0, 0, 10, 0);
        entrada.Controls.Add(Etiqueta("El cálculo es recursivo y reutiliza resultados ya obtenidos.", Retro.Secundario, 10, 95));
        var calcular = Boton("CALCULAR SERIE", Retro.Amarillo);
        calcular.Dock = DockStyle.Top;
        entrada.Controls.Add(calcular);
        var cantidad = Numero(10, 40);
        cantidad.Dock = DockStyle.Top;
        entrada.Controls.Add(cantidad);
        entrada.Controls.Add(Etiqueta("NÚMERO DE TÉRMINOS (1–40)", Retro.Amarillo, 11, 65));

        var salida = Tarjeta();
        salida.Margin = new Padding(10, 0, 0, 0);
        var resumen = Etiqueta("Esperando entrada...", Retro.Cian, 11, 70);
        var serie = new TextBox
        {
            Multiline = true,
            ReadOnly = true,
            ScrollBars = ScrollBars.Vertical,
            Dock = DockStyle.Fill,
            BackColor = Retro.FondoMenu,
            ForeColor = Retro.Texto,
            BorderStyle = BorderStyle.None,
            Font = Retro.Titulo(13),
            Text = "Selecciona una cantidad y pulsa CALCULAR SERIE."
        };
        salida.Controls.Add(serie);
        salida.Controls.Add(resumen);
        salida.Controls.Add(Etiqueta("RESULTADO", Retro.Cian, 12, 42));

        calcular.Click += (_, _) =>
        {
            var valores = Algoritmos.Fibonacci((int)cantidad.Value);
            serie.Text = string.Join(Environment.NewLine, valores.Select((valor, indice) => $"F({indice}) = {valor:N0}"));
            resumen.Text = $"{valores.Count} términos · Último valor: {valores[^1]:N0}";
        };

        columnas.Controls.Add(entrada, 0, 0);
        columnas.Controls.Add(salida, 1, 0);
        pagina.Controls.Add(columnas);
        columnas.BringToFront();
    }

    private void MostrarHanoi()
    {
        var pagina = Preparar("02 · TORRES DE HANÓI", "Traslada todos los discos de A a C usando B como auxiliar y respetando las tres reglas.");
        var columnas = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1 };
        columnas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70));
        columnas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30));
        columnas.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        var zonaJuego = Tarjeta();
        zonaJuego.Margin = new Padding(0, 0, 8, 0);
        _tablero = new HanoiBoard { Dock = DockStyle.Fill };
        var instrucciones = Etiqueta(
            "MANUAL: selecciona el disco superior, pasa sobre una torre válida y haz clic para colocarlo.",
            Retro.Secundario, 9, 42);
        _estado = Etiqueta("", Retro.Cian, 10, 54);
        _estado.Dock = DockStyle.Bottom;
        var barra = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 67, WrapContents = false };
        var discos = Numero(5, 10);
        discos.Width = 67;
        var etiquetaDiscos = new Label
        {
            Text = "DISCOS",
            Width = 70,
            Height = 44,
            ForeColor = Retro.Secundario,
            Font = Retro.Titulo(10),
            TextAlign = ContentAlignment.MiddleLeft
        };
        _iniciar = Boton("INICIAR", Retro.Rosa);
        _iniciar.Width = 100;
        var paso = Boton("PASO", Retro.Cian);
        paso.Width = 74;
        var reiniciar = Boton("REINICIAR", Retro.Amarillo);
        reiniciar.Width = 110;
        foreach (var boton in new[] { _iniciar, paso, reiniciar })
            boton.Margin = new Padding(7, 0, 0, 0);
        barra.Controls.AddRange([etiquetaDiscos, discos, _iniciar, paso, reiniciar]);
        zonaJuego.Controls.Add(_tablero);
        zonaJuego.Controls.Add(_estado);
        zonaJuego.Controls.Add(instrucciones);
        zonaJuego.Controls.Add(barra);

        var registro = Tarjeta();
        registro.Margin = new Padding(8, 0, 0, 0);
        _lista = new ListBox
        {
            Dock = DockStyle.Fill,
            BackColor = Retro.FondoMenu,
            ForeColor = Retro.Texto,
            BorderStyle = BorderStyle.None,
            Font = Retro.Titulo(9),
            IntegralHeight = false,
            HorizontalScrollbar = true
        };
        registro.Controls.Add(_lista);
        registro.Controls.Add(Etiqueta("MOVIMIENTOS", Retro.Rosa, 11, 43));

        void PrepararJuego()
        {
            _reproductor.Stop();
            _modoManual = false;
            _movimientos = Algoritmos.Hanoi((int)discos.Value);
            _siguiente = 0;
            _tablero.Reiniciar((int)discos.Value);
            _lista.Items.Clear();
            foreach (var (movimiento, indice) in _movimientos.Select((movimiento, indice) => (movimiento, indice)))
                _lista.Items.Add($"{indice + 1:000}  {movimiento}");
            _lista.ClearSelected();
            _estado.ForeColor = Retro.Cian;
            _estado.Text = $"0 / {_movimientos.Count} movimientos · Mínimo: 2^{discos.Value} - 1";
            _iniciar.Text = "INICIAR";
            _iniciar.Enabled = true;
            paso.Enabled = true;
        }

        _tablero.SeleccionManualIniciada += () =>
        {
            if (_modoManual)
                return;

            _reproductor.Stop();
            _modoManual = true;
            _lista.Items.Clear();
            for (var i = 0; i < _siguiente; i++)
                _lista.Items.Add($"{i + 1:000}  {_movimientos[i]}");
            _lista.ClearSelected();
            _iniciar.Enabled = false;
            paso.Enabled = false;
            _estado.ForeColor = Retro.Cian;
            _estado.Text = "MODO MANUAL · Haz clic en otra torre válida. Reinicia para volver a la solución automática.";
        };

        _tablero.MovimientoManualRealizado += movimiento =>
        {
            _lista.Items.Add($"{_lista.Items.Count + 1:000}  {movimiento}");
            _lista.SelectedIndex = _lista.Items.Count - 1;
            _lista.TopIndex = Math.Max(0, _lista.Items.Count - 4);
            _estado.ForeColor = _tablero.EstaResuelto ? Retro.Amarillo : Retro.Cian;
            _estado.Text = _tablero.EstaResuelto
                ? $"¡Resuelto manualmente en {_lista.Items.Count} movimientos!"
                : $"{_lista.Items.Count} movimientos · Último: {movimiento}";
        };

        discos.ValueChanged += (_, _) => PrepararJuego();
        reiniciar.Click += (_, _) => PrepararJuego();
        paso.Click += (_, _) =>
        {
            _reproductor.Stop();
            SiguientePaso();
            if (_siguiente < _movimientos.Count)
                _iniciar.Text = "SEGUIR";
        };
        _iniciar.Click += (_, _) =>
        {
            if (_siguiente == _movimientos.Count)
                PrepararJuego();

            if (_reproductor.Enabled)
            {
                _reproductor.Stop();
                _iniciar.Text = "SEGUIR";
            }
            else
            {
                _reproductor.Start();
                _iniciar.Text = "PAUSAR";
            }
        };

        columnas.Controls.Add(zonaJuego, 0, 0);
        columnas.Controls.Add(registro, 1, 0);
        pagina.Controls.Add(columnas);
        columnas.BringToFront();
        PrepararJuego();
    }

    private void SiguientePaso()
    {
        if (_modoManual || _tablero is null || _lista is null || _estado is null || _iniciar is null)
            return;

        if (_siguiente == _movimientos.Count)
        {
            _reproductor.Stop();
            _iniciar.Text = "REPETIR";
            _estado.ForeColor = Retro.Amarillo;
            _estado.Text = $"¡Completado en {_movimientos.Count} movimientos!";
            return;
        }

        var movimiento = _movimientos[_siguiente];
        _tablero.Mover(movimiento);
        _lista.SelectedIndex = _siguiente;
        _lista.TopIndex = Math.Max(0, _siguiente - 3);
        _siguiente++;
        _estado.Text = $"{_siguiente} / {_movimientos.Count} · {movimiento}";

        if (_siguiente == _movimientos.Count)
            SiguientePaso();
    }
}
