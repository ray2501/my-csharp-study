using System;
using System.Reactive.Linq;
using Gtk;

namespace rxgtksharp
{
    class TimerApp
    {
        Window win;
        DrawingArea da;

        static void Main()
        {
            Application.Init();
            new TimerApp();
            Application.Run();
        }

        TimerApp()
        {
            win = new Window("Timer Example");
            win.SetDefaultSize(260, 110);

            da = new DrawingArea();

            // For DeleteEvent
            Observable.FromEventPattern<DeleteEventArgs>(win, "DeleteEvent")
            .Subscribe(delegate
            {
                Application.Quit();
            });

            // For Timer
            Observable.Interval(TimeSpan.FromSeconds(1))
            .Subscribe(_ =>
            {
                Gtk.Application.Invoke(delegate
                {
                    da.QueueDraw();
                });
            });

            // For DrawingArea event
            Observable.FromEventPattern<DrawnArgs>(da, "Drawn")
            .Subscribe(args =>
            {
                Cairo.Context cr = args.EventArgs.Cr;

                cr.SetSourceRGB(0.5, 0.5, 0.5);
                cr.Paint();

                cr.SetFontSize(48);
                cr.MoveTo(20, 68);
                string date = DateTime.Now.ToString("HH-mm-ss");
                cr.SetSourceRGB(0.2, 0.23, 0.9);
                cr.ShowText(date);
                cr.Fill();

                ((IDisposable)cr).Dispose();

                args.EventArgs.RetVal = true;
            });

            win.Add(da);
            win.ShowAll();
        }
    }
}
