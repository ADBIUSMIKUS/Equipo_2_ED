using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MCDArcade
{
    // Enum de los métodos disponibles
    enum MetodoMCD { Euclides, RestaSucesiva, ListaDivisores, FactorizacionPrima }

    public partial class Form1 : Form
    {
        // ── Controles ──────────────────────────────────────────────────────────
        private Panel      pnlHeader          = null!;
        private Label      lblTitle            = null!;
        private Label      lblSubTitle         = null!;

        private Panel      pnlMetodo          = null!;   // selector de método
        private Label      lblMetodoTitulo    = null!;
        private Button[]   btnMetodos         = null!;

        private Panel      pnlInput           = null!;
        private Label      lblNumA            = null!;
        private TextBox    txtNumA            = null!;
        private Label      lblNumB            = null!;
        private TextBox    txtNumB            = null!;
        private Button     btnCalcular        = null!;
        private Button     btnEjemplo         = null!;
        private Button     btnLimpiar         = null!;

        private Panel      pnlPasos           = null!;
        private Label      lblSeccionPasos    = null!;
        private DataGridView dgvPasos         = null!;
        private Label      lblDetalleSeleccion = null!;

        private Panel      pnlResultado       = null!;
        private Label      lblResultadoTitulo = null!;
        private Label      lblResultadoValor  = null!;
        private Label      lblExplicacion     = null!;

        // ── Colores ────────────────────────────────────────────────────────────
        private readonly Color ColFondo    = Color.FromArgb(18, 14, 34);
        private readonly Color ColPanel    = Color.FromArgb(28, 22, 52);
        private readonly Color ColNeonCyan = Color.FromArgb(0, 230, 255);
        private readonly Color ColNeonPink = Color.FromArgb(255, 45, 120);
        private readonly Color ColNeonYellow = Color.FromArgb(255, 215, 0);
        private readonly Color ColNeonGreen  = Color.FromArgb(50, 255, 120);
        private readonly Color ColNeonOrange = Color.FromArgb(255, 140, 0);
        private readonly Color ColNeonPurple = Color.FromArgb(185, 100, 255);

        // ── Estado ─────────────────────────────────────────────────────────────
        private MetodoMCD metodoActual = MetodoMCD.Euclides;

        // Información de cada método: (nombre corto, emoji, color de acento, descripción)
        private readonly (string nombre, string emoji, Color color, string descripcion)[] infoMetodos =
        {
            ("Euclides",          "🔄", Color.FromArgb(0, 230, 255),   "Divide repetidamente hasta que el residuo sea 0."),
            ("Resta Sucesiva",    "➖", Color.FromArgb(255, 45, 120),  "Resta el menor al mayor hasta que sean iguales."),
            ("Lista Divisores",   "📋", Color.FromArgb(255, 215, 0),   "Enumera todos los divisores de ambos números y elige el mayor común."),
            ("Factorización Prima","🔢", Color.FromArgb(185, 100, 255), "Descompone cada número en factores primos y multiplica los comunes."),
        };

        // ══════════════════════════════════════════════════════════════════════
        public Form1()
        {
            InitializeComponent();
            ConfigurarVentana();
            CrearControles();
            SeleccionarMetodo(MetodoMCD.Euclides);
        }

        // ── Configuración de ventana ───────────────────────────────────────────
        private void ConfigurarVentana()
        {
            this.AutoScaleMode   = AutoScaleMode.None;
            this.Text            = "Calculadora Arcade de MCD – Equipo 2 (Estructura de Datos)";
            this.ClientSize      = new Size(960, 800);
            this.StartPosition   = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox     = false;
            this.BackColor       = ColFondo;
            this.ForeColor       = Color.White;
            this.Font            = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        }

        // ═══════════════════════════════════════════════════════════════════════
        //  CONSTRUCCIÓN DE CONTROLES
        // ═══════════════════════════════════════════════════════════════════════
        private void CrearControles()
        {
            CrearHeader();
            CrearSelectorMetodo();
            CrearPanelInput();
            CrearTablaPasos();
            CrearPanelResultado();
        }

        // ── 1. Encabezado ─────────────────────────────────────────────────────
        private void CrearHeader()
        {
            pnlHeader = PanelConBorde(20, 15, 920, 80, ColPanel, ColNeonCyan, 2);

            lblTitle = new Label
            {
                Text      = "🕹️  CALCULADORA ARCADE DE MCD  🕹️",
                Font      = new Font("Segoe UI", 18F, FontStyle.Bold),
                ForeColor = ColNeonYellow,
                TextAlign = ContentAlignment.MiddleCenter,
                Location  = new Point(10, 8),
                Size      = new Size(900, 36),
                BackColor = Color.Transparent
            };

            lblSubTitle = new Label
            {
                Text      = "Elige un método de cálculo y obtén el MCD paso a paso",
                Font      = new Font("Segoe UI", 10.5F, FontStyle.Regular),
                ForeColor = ColNeonCyan,
                TextAlign = ContentAlignment.MiddleCenter,
                Location  = new Point(10, 48),
                Size      = new Size(900, 22),
                BackColor = Color.Transparent
            };

            pnlHeader.Controls.AddRange(new Control[] { lblTitle, lblSubTitle });
            this.Controls.Add(pnlHeader);
        }

        // ── 2. Selector de método ─────────────────────────────────────────────
        private void CrearSelectorMetodo()
        {
            pnlMetodo = PanelConBorde(20, 105, 920, 95, ColPanel, ColNeonPurple, 2);

            lblMetodoTitulo = new Label
            {
                Text      = "🎮  SELECCIONA EL MÉTODO DE CÁLCULO:",
                Font      = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(185, 100, 255),
                Location  = new Point(15, 10),
                AutoSize  = true,
                BackColor = Color.Transparent
            };
            pnlMetodo.Controls.Add(lblMetodoTitulo);

            // Crear un botón por cada método
            btnMetodos = new Button[4];
            int x = 15;
            for (int i = 0; i < 4; i++)
            {
                var info  = infoMetodos[i];
                var idx   = i; // capturar para lambda

                var btn = new Button
                {
                    Text      = $"{info.emoji}  {info.nombre}",
                    Location  = new Point(x, 38),
                    Size      = new Size(213, 44),
                    FlatStyle = FlatStyle.Flat,
                    Font      = new Font("Segoe UI", 10F, FontStyle.Bold),
                    BackColor = Color.FromArgb(35, 27, 60),
                    ForeColor = info.color,
                    Cursor    = Cursors.Hand,
                    Tag       = (MetodoMCD)idx
                };
                btn.FlatAppearance.BorderColor = info.color;
                btn.FlatAppearance.BorderSize  = 2;
                btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(55, 42, 95);
                btn.Click += (s, e) => SeleccionarMetodo((MetodoMCD)idx);

                btnMetodos[i] = btn;
                pnlMetodo.Controls.Add(btn);
                x += 220;
            }

            // Tooltip de descripción
            var tt = new ToolTip { InitialDelay = 300 };
            for (int i = 0; i < 4; i++)
                tt.SetToolTip(btnMetodos[i], infoMetodos[i].descripcion);

            this.Controls.Add(pnlMetodo);
        }

        // ── 3. Panel de entradas ──────────────────────────────────────────────
        private void CrearPanelInput()
        {
            pnlInput = PanelConBorde(20, 210, 920, 90, ColPanel, ColNeonPink, 2);

            lblNumA = new Label
            {
                Text = "Primer número:", Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.White, Location = new Point(20, 13), AutoSize = true,
                BackColor = Color.Transparent
            };
            txtNumA = CajaNumero(20, 38, ColNeonCyan);
            txtNumA.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { btnCalcular.PerformClick(); e.SuppressKeyPress = true; } };

            lblNumB = new Label
            {
                Text = "Segundo número:", Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.White, Location = new Point(190, 13), AutoSize = true,
                BackColor = Color.Transparent
            };
            txtNumB = CajaNumero(190, 38, ColNeonPink);
            txtNumB.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { btnCalcular.PerformClick(); e.SuppressKeyPress = true; } };

            btnCalcular = BotonArcade("▶ CALCULAR", 365, 20, 175, 54, Color.FromArgb(0, 160, 180), ColNeonCyan);
            btnCalcular.Click += BtnCalcular_Click;

            btnEjemplo = BotonArcade("🎲 EJEMPLO", 555, 20, 165, 54, Color.FromArgb(55, 45, 85), ColNeonYellow);
            btnEjemplo.Click += BtnEjemplo_Click;

            btnLimpiar = BotonArcade("🗑️ LIMPIAR", 735, 20, 160, 54, Color.FromArgb(55, 45, 85), Color.LightCoral);
            btnLimpiar.Click += BtnLimpiar_Click;

            pnlInput.Controls.AddRange(new Control[] { lblNumA, txtNumA, lblNumB, txtNumB, btnCalcular, btnEjemplo, btnLimpiar });
            this.Controls.Add(pnlInput);
        }

        // ── 4. Tabla de pasos ─────────────────────────────────────────────────
        private void CrearTablaPasos()
        {
            pnlPasos = PanelConBorde(20, 310, 920, 340, ColPanel, ColNeonCyan, 1);

            lblSeccionPasos = new Label
            {
                Text      = "📋  PASO A PASO DEL PROCEDIMIENTO:",
                Font      = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = ColNeonCyan,
                Location  = new Point(15, 10),
                AutoSize  = true,
                BackColor = Color.Transparent
            };

            dgvPasos = new DataGridView
            {
                Location             = new Point(15, 38),
                Size                 = new Size(890, 255),
                BackgroundColor      = Color.FromArgb(12, 9, 22),
                BorderStyle          = BorderStyle.None,
                ReadOnly             = true,
                AllowUserToAddRows   = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                RowHeadersVisible    = false,
                SelectionMode        = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect          = false,
                AutoSizeColumnsMode  = DataGridViewAutoSizeColumnsMode.Fill,
                AutoSizeRowsMode     = DataGridViewAutoSizeRowsMode.AllCells,
                ScrollBars           = ScrollBars.Vertical,
                EnableHeadersVisualStyles = false,
                CellBorderStyle      = DataGridViewCellBorderStyle.SingleHorizontal,
                GridColor            = Color.FromArgb(45, 35, 75)
            };
            EstilizarGrid(dgvPasos);
            ConfigurarColumnasEuclides(); // columnas por defecto

            lblDetalleSeleccion = new Label
            {
                Text      = "💡 Haz clic en cualquier fila para leer la explicación completa.",
                Font      = new Font("Segoe UI", 9.5F, FontStyle.Italic),
                ForeColor = ColNeonCyan,
                Location  = new Point(15, 300),
                Size      = new Size(890, 28),
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.Transparent
            };

            dgvPasos.SelectionChanged += (s, e) =>
            {
                if (dgvPasos.SelectedRows.Count > 0 && dgvPasos.Columns.Count > 0)
                {
                    var row     = dgvPasos.SelectedRows[0];
                    int lastCol = dgvPasos.Columns.Count - 1;
                    string paso = row.Cells[0].Value?.ToString() ?? "";
                    string exp  = row.Cells[lastCol].Value?.ToString() ?? "";
                    lblDetalleSeleccion.Text      = $"🔍 {paso}: {exp}";
                    lblDetalleSeleccion.ForeColor = ColNeonYellow;
                }
            };

            pnlPasos.Controls.AddRange(new Control[] { lblSeccionPasos, dgvPasos, lblDetalleSeleccion });
            this.Controls.Add(pnlPasos);
        }

        // ── 5. Panel de resultado ─────────────────────────────────────────────
        private void CrearPanelResultado()
        {
            pnlResultado = PanelConBorde(20, 660, 920, 125, ColPanel, ColNeonGreen, 2);

            lblResultadoTitulo = new Label
            {
                Text      = "RESULTADO FINAL",
                Font      = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = ColNeonYellow,
                TextAlign = ContentAlignment.MiddleCenter,
                Location  = new Point(10, 10),
                Size      = new Size(900, 20),
                BackColor = Color.Transparent
            };

            lblResultadoValor = new Label
            {
                Text      = "Ingresa dos números y haz clic en \"CALCULAR\"",
                Font      = new Font("Segoe UI", 17F, FontStyle.Bold),
                ForeColor = Color.DarkGray,
                TextAlign = ContentAlignment.MiddleCenter,
                Location  = new Point(10, 34),
                Size      = new Size(900, 46),
                BackColor = Color.Transparent
            };

            lblExplicacion = new Label
            {
                Text      = "El MCD es el número más grande que divide exactamente a ambos números sin dejar residuo.",
                Font      = new Font("Segoe UI", 10F, FontStyle.Italic),
                ForeColor = Color.LightGray,
                TextAlign = ContentAlignment.MiddleCenter,
                Location  = new Point(10, 86),
                Size      = new Size(900, 30),
                BackColor = Color.Transparent
            };

            pnlResultado.Controls.AddRange(new Control[] { lblResultadoTitulo, lblResultadoValor, lblExplicacion });
            this.Controls.Add(pnlResultado);
        }

        // ══════════════════════════════════════════════════════════════════════
        //  LÓGICA DE SELECCIÓN DE MÉTODO
        // ══════════════════════════════════════════════════════════════════════
        private void SeleccionarMetodo(MetodoMCD metodo)
        {
            metodoActual = metodo;
            int idx = (int)metodo;

            // Resaltar el botón activo
            for (int i = 0; i < btnMetodos.Length; i++)
            {
                bool activo = i == idx;
                btnMetodos[i].BackColor = activo
                    ? Color.FromArgb(55, 40, 105)
                    : Color.FromArgb(35, 27, 60);
                btnMetodos[i].Font = new Font("Segoe UI",
                    activo ? 10.5F : 10F,
                    activo ? FontStyle.Bold : FontStyle.Regular);
                // Borde más grueso en el activo
                btnMetodos[i].FlatAppearance.BorderSize = activo ? 3 : 2;
            }

            // Actualizar subtítulo con descripción del método
            lblSubTitle.Text = $"{infoMetodos[idx].emoji}  {infoMetodos[idx].nombre}:  {infoMetodos[idx].descripcion}";
            lblSubTitle.ForeColor = infoMetodos[idx].color;

            // Reconfigurar columnas de la tabla según el método
            dgvPasos.Rows.Clear();
            dgvPasos.Columns.Clear();
            switch (metodo)
            {
                case MetodoMCD.Euclides:          ConfigurarColumnasEuclides();         break;
                case MetodoMCD.RestaSucesiva:     ConfigurarColumnasResta();            break;
                case MetodoMCD.ListaDivisores:    ConfigurarColumnasListaDivisores();   break;
                case MetodoMCD.FactorizacionPrima: ConfigurarColumnasFactorizacion();   break;
            }

            ResetearResultado();
        }

        // ══════════════════════════════════════════════════════════════════════
        //  CONFIGURACIÓN DE COLUMNAS POR MÉTODO
        // ══════════════════════════════════════════════════════════════════════
        private void ConfigurarColumnasEuclides()
        {
            AgregarColumna("colPaso",      "Paso",              11, 65,  DataGridViewContentAlignment.MiddleCenter);
            AgregarColumna("colDivision",  "División",          15, 95,  DataGridViewContentAlignment.MiddleCenter);
            AgregarColumna("colCociente",  "Cociente",          11, 70,  DataGridViewContentAlignment.MiddleCenter);
            AgregarColumna("colResiduo",   "Residuo (Sobra)",   15, 95,  DataGridViewContentAlignment.MiddleCenter, true);
            AgregarColumna("colExp",       "Explicación",       48, 320, DataGridViewContentAlignment.MiddleLeft,  false, true);
        }

        private void ConfigurarColumnasResta()
        {
            AgregarColumna("colPaso",      "Paso",              10, 60,  DataGridViewContentAlignment.MiddleCenter);
            AgregarColumna("colA",         "Valor A",           15, 90,  DataGridViewContentAlignment.MiddleCenter);
            AgregarColumna("colB",         "Valor B",           15, 90,  DataGridViewContentAlignment.MiddleCenter);
            AgregarColumna("colOp",        "Operación",         20, 140, DataGridViewContentAlignment.MiddleCenter, true);
            AgregarColumna("colExp",       "Explicación",       40, 280, DataGridViewContentAlignment.MiddleLeft,  false, true);
        }

        private void ConfigurarColumnasListaDivisores()
        {
            AgregarColumna("colNumero",    "Número",            15, 90,  DataGridViewContentAlignment.MiddleCenter);
            AgregarColumna("colDivisor",   "Divisor",           15, 90,  DataGridViewContentAlignment.MiddleCenter, true);
            AgregarColumna("colDivA",      "¿Divide a A?",      17, 105, DataGridViewContentAlignment.MiddleCenter);
            AgregarColumna("colDivB",      "¿Divide a B?",      17, 105, DataGridViewContentAlignment.MiddleCenter);
            AgregarColumna("colComun",     "¿Común?",           14, 85,  DataGridViewContentAlignment.MiddleCenter);
            AgregarColumna("colExp",       "Resultado",         22, 150, DataGridViewContentAlignment.MiddleLeft,  false, true);
        }

        private void ConfigurarColumnasFactorizacion()
        {
            AgregarColumna("colNumero",    "Número",            14, 85,  DataGridViewContentAlignment.MiddleCenter);
            AgregarColumna("colDivisor",   "Primo",             12, 70,  DataGridViewContentAlignment.MiddleCenter, true);
            AgregarColumna("colCociente",  "Cociente",          14, 85,  DataGridViewContentAlignment.MiddleCenter);
            AgregarColumna("colExp",       "Explicación",       60, 380, DataGridViewContentAlignment.MiddleLeft,  false, true);
        }

        // ══════════════════════════════════════════════════════════════════════
        //  MANEJADORES DE BOTONES
        // ══════════════════════════════════════════════════════════════════════
        private void BtnCalcular_Click(object? sender, EventArgs e)
        {
            dgvPasos.Rows.Clear();
            ResetarDetalle();

            if (!long.TryParse(txtNumA.Text.Trim(), out long numA))
            {
                MostrarError("Ingresa un número entero válido en el primer campo.");
                txtNumA.Focus(); return;
            }
            if (!long.TryParse(txtNumB.Text.Trim(), out long numB))
            {
                MostrarError("Ingresa un número entero válido en el segundo campo.");
                txtNumB.Focus(); return;
            }
            if (numA == 0 && numB == 0)
            {
                lblResultadoValor.Text   = "MCD(0, 0) = Indefinido";
                lblResultadoValor.ForeColor = ColNeonPink;
                lblExplicacion.Text      = "Matemáticamente indefinido: cualquier entero divide a 0.";
                return;
            }

            long absA = Math.Abs(numA);
            long absB = Math.Abs(numB);

            long mcd = metodoActual switch
            {
                MetodoMCD.Euclides           => EjecutarEuclides(absA, absB, numA, numB),
                MetodoMCD.RestaSucesiva      => EjecutarResta(absA, absB, numA, numB),
                MetodoMCD.ListaDivisores     => EjecutarListaDivisores(absA, absB, numA, numB),
                MetodoMCD.FactorizacionPrima => EjecutarFactorizacion(absA, absB, numA, numB),
                _                            => 0
            };

            if (dgvPasos.Rows.Count > 0)
            {
                dgvPasos.ClearSelection();
                dgvPasos.Rows[dgvPasos.Rows.Count - 1].Selected = true;
            }
        }

        // ══════════════════════════════════════════════════════════════════════
        //  IMPLEMENTACIÓN DE MÉTODOS
        // ══════════════════════════════════════════════════════════════════════

        // ── Método 1: Algoritmo de Euclides ───────────────────────────────────
        private long EjecutarEuclides(long absA, long absB, long numA, long numB)
        {
            int paso = 1;
            long a = Math.Max(absA, absB);
            long b = Math.Min(absA, absB);
            long mcd = EuclidesRecursivo(a, b, ref paso);

            lblResultadoValor.Text      = $"MCD({numA}, {numB}) = {mcd}";
            lblResultadoValor.ForeColor = ColNeonCyan;
            lblExplicacion.Text         = $"🔄 Euclides: el {mcd} es el número más grande que divide exactamente a {numA} y a {numB}. ({paso - 1} pasos)";
            return mcd;
        }

        private long EuclidesRecursivo(long a, long b, ref int paso)
        {
            if (b == 0) return a;

            long coc = a / b, res = a % b;
            int p = paso++;

            if (res == 0)
            {
                int ri = dgvPasos.Rows.Add($"Paso {p}", $"{a} ÷ {b}", coc, "0",
                    $"¡Residuo 0! La división es exacta → el MCD es {b}.");
                dgvPasos.Rows[ri].DefaultCellStyle.ForeColor = ColNeonGreen;
                dgvPasos.Rows[ri].DefaultCellStyle.Font      = new Font("Consolas", 10F, FontStyle.Bold);
                return b;
            }

            dgvPasos.Rows.Add($"Paso {p}", $"{a} ÷ {b}", coc, res,
                $"{b} cabe {coc} veces en {a} y sobra {res}. Continuamos: MCD({b}, {res}).");
            return EuclidesRecursivo(b, res, ref paso);
        }

        // ── Método 2: Resta Sucesiva ──────────────────────────────────────────
        private long EjecutarResta(long absA, long absB, long numA, long numB)
        {
            if (absA == 0 || absB == 0)
            {
                long r = absA == 0 ? absB : absA;
                dgvPasos.Rows.Add("Paso 1", absA, absB, "-", $"Uno de los valores es 0, el MCD es {r}.");
                MostrarResultado(numA, numB, r, "➖");
                return r;
            }

            long a = Math.Max(absA, absB);
            long b = Math.Min(absA, absB);
            int paso = 1;
            const int MAX_PASOS = 300;   // evitar bucle infinito en números muy grandes

            while (a != b && paso <= MAX_PASOS)
            {
                string op;
                long aViejo = a, bViejo = b;

                if (a > b) { a = a - b; op = $"{aViejo} - {bViejo} = {a}"; }
                else       { b = b - a; op = $"{bViejo} - {aViejo} = {b}"; }

                string exp = a == b
                    ? $"¡Ambos valores son iguales ({a})! Ese es el MCD."
                    : $"El mayor aún es más grande. Seguimos restando.";

                int ri = dgvPasos.Rows.Add($"Paso {paso}", a < b ? a : aViejo, b < a ? b : bViejo, op, exp);
                if (a == b)
                {
                    dgvPasos.Rows[ri].DefaultCellStyle.ForeColor = ColNeonGreen;
                    dgvPasos.Rows[ri].DefaultCellStyle.Font      = new Font("Consolas", 10F, FontStyle.Bold);
                }
                paso++;
            }

            if (paso > MAX_PASOS)
            {
                // Demasiados pasos: mostrar aviso y calcular directo
                dgvPasos.Rows.Add("...", "...", "...", "...",
                    $"⚠ Se llegó al límite de {MAX_PASOS} pasos. Se calculó el MCD directamente por Euclides.");
                long mcdDirecto = EuclidesDirecto(absA, absB);
                MostrarResultado(numA, numB, mcdDirecto, "➖");
                return mcdDirecto;
            }

            MostrarResultado(numA, numB, a, "➖");
            return a;
        }

        // ── Método 3: Lista de Divisores ──────────────────────────────────────
        private long EjecutarListaDivisores(long absA, long absB, long numA, long numB)
        {
            var divA = ObtenerDivisores(absA);
            var divB = ObtenerDivisores(absB);
            var comunes = divA.Intersect(divB).OrderBy(x => x).ToList();

            // Mostrar la lista de divisores de A (hasta 50 filas para no desbordar)
            bool primeraFilaA = true;
            foreach (long d in divA.Take(50))
            {
                bool esComun = divB.Contains(d);
                int ri = dgvPasos.Rows.Add(
                    primeraFilaA ? $"A = {absA}" : "",
                    d,
                    $"{absA} ÷ {d} = {absA / d}  ✔",
                    divB.Contains(d) ? $"{absB} ÷ {d} = {absB / d}  ✔" : "No divide  ✖",
                    esComun ? "✔ Común" : "✖",
                    esComun ? $"{d} divide a ambos." : $"{d} no divide a {absB}."
                );
                if (esComun)
                {
                    dgvPasos.Rows[ri].DefaultCellStyle.ForeColor =
                        d == comunes.Last() ? ColNeonGreen : ColNeonYellow;
                    if (d == comunes.Last())
                        dgvPasos.Rows[ri].DefaultCellStyle.Font = new Font("Consolas", 10F, FontStyle.Bold);
                }
                primeraFilaA = false;
            }
            if (divA.Count > 50)
                dgvPasos.Rows.Add("...", "...", "...", "...", "...", $"({divA.Count - 50} divisores más de {absA} no mostrados)");

            long mcd = comunes.Count > 0 ? comunes.Last() : 1;
            MostrarResultado(numA, numB, mcd, "📋",
                $"Divisores de {absA}: {string.Join(", ", divA.Take(12))}{(divA.Count > 12 ? "…" : "")} — " +
                $"Divisores de {absB}: {string.Join(", ", divB.Take(12))}{(divB.Count > 12 ? "…" : "")} — " +
                $"Comunes: {string.Join(", ", comunes)}");
            return mcd;
        }

        // ── Método 4: Factorización en Primos ─────────────────────────────────
        private long EjecutarFactorizacion(long absA, long absB, long numA, long numB)
        {
            // Factorizar A
            AggregarFilasFactorizacion(absA, "A");
            // Separador visual
            dgvPasos.Rows.Add("", "", "", "── Factorizando B ──────────────────────────────────────────────");

            // Factorizar B
            AggregarFilasFactorizacion(absB, "B");

            // Calcular MCD con factores comunes
            var factA = FactoresPrimos(absA);
            var factB = FactoresPrimos(absB);

            var comunes = new List<long>();
            var tempB   = new List<long>(factB);
            foreach (var f in factA)
            {
                if (tempB.Contains(f)) { comunes.Add(f); tempB.Remove(f); }
            }

            long mcd = comunes.Count > 0 ? comunes.Aggregate(1L, (acc, x) => acc * x) : 1;

            // Fila de conclusión
            string factAStr   = string.Join(" × ", factA);
            string factBStr   = string.Join(" × ", factB);
            string comunesStr = comunes.Count > 0 ? string.Join(" × ", comunes) : "ninguno";

            int ri = dgvPasos.Rows.Add("Conclusión", "-", "-",
                $"{absA} = {factAStr}  |  {absB} = {factBStr}  →  Factores comunes: {comunesStr}  →  MCD = {mcd}");
            dgvPasos.Rows[ri].DefaultCellStyle.ForeColor = ColNeonGreen;
            dgvPasos.Rows[ri].DefaultCellStyle.Font      = new Font("Consolas", 10F, FontStyle.Bold);

            MostrarResultado(numA, numB, mcd, "🔢",
                $"{absA} = {factAStr}   |   {absB} = {factBStr}   →   Factores comunes: {comunesStr}");
            return mcd;
        }

        private void AggregarFilasFactorizacion(long numero, string etiqueta)
        {
            long n = numero;
            long divisor = 2;
            bool primero = true;

            if (n == 0 || n == 1)
            {
                dgvPasos.Rows.Add($"{etiqueta} = {numero}", divisor, n,
                    numero == 1 ? "1 ya es primo (caso especial)." : "0 no tiene factorización prima significativa.");
                return;
            }

            while (n > 1)
            {
                if (n % divisor == 0)
                {
                    long cociente = n / divisor;
                    int ri = dgvPasos.Rows.Add(
                        primero ? $"{etiqueta} = {numero}" : "",
                        divisor,
                        cociente,
                        $"{n} ÷ {divisor} = {cociente}. {divisor} es factor primo de {numero}."
                    );
                    if (cociente == 1)
                    {
                        dgvPasos.Rows[ri].DefaultCellStyle.ForeColor = ColNeonYellow;
                        dgvPasos.Rows[ri].DefaultCellStyle.Font      = new Font("Consolas", 10F, FontStyle.Bold);
                    }
                    n       = cociente;
                    primero = false;
                }
                else
                {
                    divisor++;
                }
            }
        }

        // ══════════════════════════════════════════════════════════════════════
        //  HELPERS MATEMÁTICOS
        // ══════════════════════════════════════════════════════════════════════
        private static long EuclidesDirecto(long a, long b)
        {
            while (b != 0) { long t = b; b = a % b; a = t; }
            return a;
        }

        private static List<long> ObtenerDivisores(long n)
        {
            var lista = new List<long>();
            if (n == 0) return lista;
            for (long i = 1; i <= Math.Sqrt(n) + 1; i++)
                if (n % i == 0)
                {
                    lista.Add(i);
                    if (i != n / i) lista.Add(n / i);
                }
            lista.Sort();
            return lista;
        }

        private static List<long> FactoresPrimos(long n)
        {
            var lista = new List<long>();
            if (n <= 1) return lista;
            long d = 2;
            while (n > 1) { while (n % d == 0) { lista.Add(d); n /= d; } d++; }
            return lista;
        }

        // ══════════════════════════════════════════════════════════════════════
        //  HELPERS DE UI
        // ══════════════════════════════════════════════════════════════════════
        private void MostrarResultado(long numA, long numB, long mcd, string emoji, string? detalle = null)
        {
            string nombre = infoMetodos[(int)metodoActual].nombre;
            lblResultadoValor.Text      = $"MCD({numA}, {numB}) = {mcd}";
            lblResultadoValor.ForeColor = infoMetodos[(int)metodoActual].color;
            lblExplicacion.Text = detalle
                ?? $"{emoji} {nombre}: el número más grande que divide exactamente a {numA} y a {numB} es {mcd}.";
        }

        private void ResetearResultado()
        {
            lblResultadoValor.Text      = "Ingresa dos números y haz clic en \"CALCULAR\"";
            lblResultadoValor.ForeColor = Color.DarkGray;
            lblExplicacion.Text         = "El MCD es el número más grande que divide exactamente a ambos números sin dejar residuo.";
            ResetarDetalle();
        }

        private void ResetarDetalle()
        {
            lblDetalleSeleccion.Text      = "💡 Haz clic en cualquier fila para leer la explicación completa.";
            lblDetalleSeleccion.ForeColor = ColNeonCyan;
        }

        private void MostrarError(string msg) =>
            MessageBox.Show(msg, "Dato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        // ── Helpers de construcción de controles ──────────────────────────────
        private Panel PanelConBorde(int x, int y, int w, int h, Color bg, Color border, float grosor)
        {
            var p = new Panel { Location = new Point(x, y), Size = new Size(w, h), BackColor = bg };
            p.Paint += (s, e) =>
            {
                using var pen = new Pen(border, grosor);
                e.Graphics.DrawRectangle(pen, 1, 1, p.Width - 3, p.Height - 3);
            };
            return p;
        }

        private TextBox CajaNumero(int x, int y, Color color) => new TextBox
        {
            Location    = new Point(x, y),
            Size        = new Size(150, 32),
            Font        = new Font("Consolas", 14F, FontStyle.Bold),
            BackColor   = Color.FromArgb(12, 9, 22),
            ForeColor   = color,
            BorderStyle = BorderStyle.FixedSingle,
            TextAlign   = HorizontalAlignment.Center
        };

        private Button BotonArcade(string texto, int x, int y, int w, int h, Color bg, Color border)
        {
            var btn = new Button
            {
                Text      = texto,
                Location  = new Point(x, y),
                Size      = new Size(w, h),
                FlatStyle = FlatStyle.Flat,
                Font      = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                BackColor = bg,
                ForeColor = border,
                Cursor    = Cursors.Hand
            };
            btn.FlatAppearance.BorderColor = border;
            btn.FlatAppearance.BorderSize  = 2;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(55, 42, 95);
            return btn;
        }

        private void EstilizarGrid(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.BackColor   = Color.FromArgb(40, 30, 70);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor   = ColNeonYellow;
            dgv.ColumnHeadersDefaultCellStyle.Font        = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment   = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersHeight = 35;

            dgv.DefaultCellStyle.BackColor          = Color.FromArgb(18, 14, 32);
            dgv.DefaultCellStyle.ForeColor          = Color.White;
            dgv.DefaultCellStyle.Font               = new Font("Consolas", 10F, FontStyle.Regular);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(60, 45, 100);
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.DefaultCellStyle.Padding            = new Padding(4, 6, 4, 6);
            dgv.DefaultCellStyle.WrapMode           = DataGridViewTriState.True;
        }

        private void AgregarColumna(string name, string header, float fill, int minW,
            DataGridViewContentAlignment align, bool bold = false, bool wrap = false)
        {
            var col = new DataGridViewTextBoxColumn
            {
                Name         = name,
                HeaderText   = header,
                FillWeight   = fill,
                MinimumWidth = minW
            };
            col.DefaultCellStyle.Alignment = align;
            if (bold) col.DefaultCellStyle.Font = new Font("Consolas", 10F, FontStyle.Bold);
            if (wrap) col.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvPasos.Columns.Add(col);
        }

        private void BtnEjemplo_Click(object? sender, EventArgs e)
        {
            (long, long)[] ejemplos = { (48,18),(406,288),(120,45),(54,24),(1071,462),(180,270) };
            var rnd = new Random();
            var ej  = ejemplos[rnd.Next(ejemplos.Length)];
            txtNumA.Text = ej.Item1.ToString();
            txtNumB.Text = ej.Item2.ToString();
            btnCalcular.PerformClick();
        }

        private void BtnLimpiar_Click(object? sender, EventArgs e)
        {
            txtNumA.Clear();
            txtNumB.Clear();
            dgvPasos.Rows.Clear();
            ResetearResultado();
            txtNumA.Focus();
        }
    }
}
