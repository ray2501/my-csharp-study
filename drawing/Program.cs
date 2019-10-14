using System;
using System.Drawing;

namespace drawing
{
    class Program
    {
        static void Main(string[] args)
        {
           Image image = new Bitmap(1024, 768);
           Graphics graph = Graphics.FromImage(image);
           graph.Clear(Color.Azure);

           Pen pen = new Pen(Brushes.Black);
           graph.DrawLines(pen, new Point[] { new Point(10,10), new Point(800, 600) });

           image.Save("graph.jpeg", System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}
