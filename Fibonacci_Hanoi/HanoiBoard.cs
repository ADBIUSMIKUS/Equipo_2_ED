using System.Drawing.Drawing2D;

namespace FibonacciHanoi;

public sealed class HanoiBoard : Control
{
    private readonly Stack<int>[] _torres = [new(), new(), new()];
    private int _cantidad = 5;

    public HanoiBoard()
    {
        DoubleBuffered = true;
        BackColor = Retro.FondoTarjeta;
        Reiniciar(_cantidad);
    }

    public void Reiniciar(int cantidad)
    {
        _cantidad = cantidad;
        foreach (var torre in _torres)
            torre.Clear();

        for (var disco = cantidad; disco >= 1; disco--)
            _torres[0].Push(disco);

        Invalidate();
    }

    public void Mover(Movimiento movimiento)
    {
        var origen = movimiento.Origen - 'A';
        var destino = movimiento.Destino - 'A';
        if (origen is < 0 or > 2 || destino is < 0 or > 2 || origen == destino)
            throw new InvalidOperationException("La torre indicada no existe.");
        if (_torres[origen].Count == 0 || _torres[origen].Peek() != movimiento.Disco)
            throw new InvalidOperationException("Sólo puede moverse el disco superior.");
        if (_torres[destino].Count > 0 && _torres[destino].Peek() < movimiento.Disco)
            throw new InvalidOperationException("Un disco grande no puede quedar sobre uno pequeño.");

        _torres[destino].Push(_torres[origen].Pop());
        Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        var g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        var baseY = Height - 55;
        var gap = Width / 3f;

        using var rod = new Pen(Retro.Cian, 4);
        using var halo = new Pen(Color.FromArgb(55, Retro.Cian), 12);
        using var labelFont = new Font("Consolas", 12, FontStyle.Bold);
        using var labelBrush = new SolidBrush(Retro.Texto);

        for (var i = 0; i < 3; i++)
        {
            var centro = gap * (i + .5f);
            var alto = Math.Max(95, baseY - 75);
            g.DrawLine(halo, centro, alto - 80, centro, baseY);
            g.DrawLine(rod, centro, alto - 80, centro, baseY);
            g.DrawLine(halo, centro - gap * .38f, baseY, centro + gap * .38f, baseY);
            g.DrawLine(rod, centro - gap * .38f, baseY, centro + gap * .38f, baseY);
            var titulo = $"TORRE {(char)('A' + i)}";
            var medida = g.MeasureString(titulo, labelFont);
            g.DrawString(titulo, labelFont, labelBrush, centro - medida.Width / 2, baseY + 12);
        }

        for (var torre = 0; torre < 3; torre++)
        {
            var centro = gap * (torre + .5f);
            var discos = _torres[torre].Reverse().ToArray();
            var altoDisco = Math.Clamp((baseY - 90f) / Math.Max(10, _cantidad), 15f, 29f);
            for (var nivel = 0; nivel < discos.Length; nivel++)
            {
                var disco = discos[nivel];
                var ancho = 40f + (gap * .73f - 40f) * disco / _cantidad;
                var rectangulo = new RectangleF(
                    centro - ancho / 2,
                    baseY - (nivel + 1) * altoDisco,
                    ancho,
                    altoDisco - 3);
                using var relleno = new SolidBrush(ColorDisco(disco));
                using var contorno = new Pen(Color.FromArgb(230, 240, 255), 1);
                using var contornoDisco = new GraphicsPath();
                var redondeo = Rectangle.Round(rectangulo);
                const int radio = 6;
                const int diametro = radio * 2;
                contornoDisco.AddArc(redondeo.Left, redondeo.Top, diametro, diametro, 180, 90);
                contornoDisco.AddArc(redondeo.Right - diametro, redondeo.Top, diametro, diametro, 270, 90);
                contornoDisco.AddArc(redondeo.Right - diametro, redondeo.Bottom - diametro, diametro, diametro, 0, 90);
                contornoDisco.AddArc(redondeo.Left, redondeo.Bottom - diametro, diametro, diametro, 90, 90);
                contornoDisco.CloseFigure();
                g.FillPath(relleno, contornoDisco);
                g.DrawPath(contorno, contornoDisco);
            }
        }
    }

    private static Color ColorDisco(int disco) => (disco % 5) switch
    {
        0 => Retro.Cian,
        1 => Retro.Rosa,
        2 => Retro.Amarillo,
        3 => Color.FromArgb(130, 108, 252),
        _ => Color.FromArgb(73, 223, 153)
    };
}
