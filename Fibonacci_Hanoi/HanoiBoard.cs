using System.Drawing.Drawing2D;

namespace FibonacciHanoi;

public class HanoiBoard : Control
{
    private readonly Stack<int>[] _torres = [new(), new(), new()];
    private int _cantidad = 5;
    private int? _origenSeleccionado;
    private int? _torrePrevisualizada;

    public event Action? SeleccionManualIniciada;
    public event Action<Movimiento>? MovimientoManualRealizado;

    public int? DiscoSeleccionado => _origenSeleccionado is int origen ? _torres[origen].Peek() : null;
    public int? TorrePrevisualizada => _torrePrevisualizada;
    public bool EstaResuelto => _torres[2].Count == _cantidad;

    public HanoiBoard()
    {
        DoubleBuffered = true;
        BackColor = Retro.FondoTarjeta;
        Reiniciar(_cantidad);
    }

    public void Reiniciar(int cantidad)
    {
        if (cantidad is < 1 or > 10)
            throw new ArgumentOutOfRangeException(nameof(cantidad));

        _cantidad = cantidad;
        _origenSeleccionado = null;
        _torrePrevisualizada = null;
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
        _origenSeleccionado = null;
        _torrePrevisualizada = null;
        Invalidate();
    }

    protected override void OnMouseClick(MouseEventArgs e)
    {
        base.OnMouseClick(e);
        if (e.Button != MouseButtons.Left)
            return;

        if (_origenSeleccionado is null)
        {
            var origen = DiscoSuperiorEn(e.Location);
            if (origen is null)
                return;

            SeleccionManualIniciada?.Invoke();
            _origenSeleccionado = origen;
            _torrePrevisualizada = null;
            Invalidate();
            return;
        }

        var destino = TorreEn(e.Location);
        if (destino is null)
            return;

        if (destino == _origenSeleccionado)
        {
            _origenSeleccionado = null;
            _torrePrevisualizada = null;
            Invalidate();
            return;
        }

        if (!DestinoValido(destino.Value))
            return;

        var movimiento = new Movimiento(
            DiscoSeleccionado!.Value,
            (char)('A' + _origenSeleccionado.Value),
            (char)('A' + destino.Value));
        Mover(movimiento);
        MovimientoManualRealizado?.Invoke(movimiento);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);
        var destino = TorreEn(e.Location);
        var nuevaVistaPrevia = destino is not null && DestinoValido(destino.Value) ? destino : null;
        if (nuevaVistaPrevia != _torrePrevisualizada)
        {
            _torrePrevisualizada = nuevaVistaPrevia;
            Invalidate();
        }

        Cursor = _origenSeleccionado is null
            ? DiscoSuperiorEn(e.Location) is null ? Cursors.Default : Cursors.Hand
            : nuevaVistaPrevia is null ? Cursors.Default : Cursors.Hand;
    }

    protected override void OnMouseLeave(EventArgs e)
    {
        base.OnMouseLeave(e);
        _torrePrevisualizada = null;
        Cursor = Cursors.Default;
        Invalidate();
    }

    private int? DiscoSuperiorEn(Point posicion)
    {
        for (var torre = 0; torre < 3; torre++)
        {
            if (_torres[torre].Count == 0)
                continue;

            var superior = _torres[torre].Peek();
            if (RectanguloDisco(torre, _torres[torre].Count - 1, superior).Contains(posicion))
                return torre;
        }

        return null;
    }

    private int? TorreEn(Point posicion)
    {
        if (posicion.X < 0 || posicion.X >= Width || posicion.Y < 0 || posicion.Y >= Height)
            return null;

        return (int)(posicion.X / (Width / 3f));
    }

    private bool DestinoValido(int destino)
    {
        if (_origenSeleccionado is null || destino == _origenSeleccionado)
            return false;

        var disco = DiscoSeleccionado!.Value;
        return _torres[destino].Count == 0 || _torres[destino].Peek() > disco;
    }

    private RectangleF RectanguloDisco(int torre, int nivel, int disco)
    {
        var baseY = Height - 55;
        var gap = Width / 3f;
        var alto = Math.Clamp((baseY - 90f) / Math.Max(10, _cantidad), 15f, 29f);
        var ancho = 40f + (gap * .73f - 40f) * disco / _cantidad;
        return new RectangleF(
            gap * (torre + .5f) - ancho / 2,
            baseY - (nivel + 1) * alto,
            ancho,
            alto - 3);
    }

    private static GraphicsPath Contorno(RectangleF rectangulo)
    {
        var path = new GraphicsPath();
        var rect = Rectangle.Round(rectangulo);
        const int diametro = 12;
        path.AddArc(rect.Left, rect.Top, diametro, diametro, 180, 90);
        path.AddArc(rect.Right - diametro, rect.Top, diametro, diametro, 270, 90);
        path.AddArc(rect.Right - diametro, rect.Bottom - diametro, diametro, diametro, 0, 90);
        path.AddArc(rect.Left, rect.Bottom - diametro, diametro, diametro, 90, 90);
        path.CloseFigure();
        return path;
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
            var altoVarilla = Math.Max(95, baseY - 75);
            g.DrawLine(halo, centro, altoVarilla - 80, centro, baseY);
            g.DrawLine(rod, centro, altoVarilla - 80, centro, baseY);
            g.DrawLine(halo, centro - gap * .38f, baseY, centro + gap * .38f, baseY);
            g.DrawLine(rod, centro - gap * .38f, baseY, centro + gap * .38f, baseY);
            var titulo = $"TORRE {(char)('A' + i)}";
            var medida = g.MeasureString(titulo, labelFont);
            g.DrawString(titulo, labelFont, labelBrush, centro - medida.Width / 2, baseY + 12);
        }

        for (var torre = 0; torre < 3; torre++)
        {
            var discos = _torres[torre].Reverse().ToArray();
            for (var nivel = 0; nivel < discos.Length; nivel++)
            {
                var disco = discos[nivel];
                var rectangulo = RectanguloDisco(torre, nivel, disco);
                var seleccionado = _origenSeleccionado == torre && nivel == discos.Length - 1;
                using var relleno = new SolidBrush(seleccionado ? Color.FromArgb(151, 160, 176) : ColorDisco(disco));
                using var contorno = new Pen(Color.FromArgb(230, 240, 255), 1);
                using var contornoDisco = Contorno(rectangulo);
                g.FillPath(relleno, contornoDisco);
                g.DrawPath(contorno, contornoDisco);
            }
        }

        if (_torrePrevisualizada is int destino && DiscoSeleccionado is int discoElegido)
        {
            var fantasma = RectanguloDisco(destino, _torres[destino].Count, discoElegido);
            using var relleno = new SolidBrush(Color.FromArgb(110, 166, 174, 190));
            using var borde = new Pen(Color.FromArgb(235, 218, 225, 239), 2) { DashStyle = DashStyle.Dash };
            using var contorno = Contorno(fantasma);
            g.FillPath(relleno, contorno);
            g.DrawPath(borde, contorno);
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
