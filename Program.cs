using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Ejercicio4_Monedas;

static class Program
{
    [STAThread]
    static void Main(string[] args)
    {
        ApplicationConfiguration.Initialize();
        Form1 form = new Form1();

        if (args.Length > 0 && args[0].ToLower() == "--screenshot")
        {
            form.Shown += (s, e) =>
            {
                // Cargar valores del ejemplo para la captura
                var btnEjemplo = form.Controls.Find("btnProbarEjemplo", true)[0] as Button;
                btnEjemplo?.PerformClick();

                form.Refresh();
                Application.DoEvents();

                string targetDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "capturas");
                Directory.CreateDirectory(targetDir);

                Bitmap bmp = new Bitmap(form.Width, form.Height);
                form.DrawToBitmap(bmp, new Rectangle(0, 0, form.Width, form.Height));
                bmp.Save(Path.Combine(targetDir, "diseno_fintech_monedas.png"), System.Drawing.Imaging.ImageFormat.Png);

                Application.Exit();
            };
        }

        Application.Run(form);
    }
}