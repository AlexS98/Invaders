using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Invaders.UIHandlers
{
    class FieldDrawing
    {
        public Canvas Canvas { get; set; }

        public FieldDrawing(Canvas canvas)
        {
            Canvas = canvas;
        }

        public void DrawWarior(Wariors warior, Point point = new Point())
        {
            Point position = (point.X == 0 && point.Y == 0) ? warior.Place.Center : point;
            Image image1 = new Image();
            TextBlock textBlock = new TextBlock();
            if (warior is Knight)
            {
                if (warior.Owner.Side) image1.Source = new BitmapImage(new Uri(@"D:\\Projects\Old\Lab_5\Lab_5\images\white_horse.png"));
                else image1.Source = new BitmapImage(new Uri(@"D:\\Projects\Old\Lab_5\Lab_5\images\black_horse.png"));
            }
            else if (warior is Swordsman)
            {
                if (warior.Owner.Side) image1.Source = new BitmapImage(new Uri(@"D:\\Projects\Old\Lab_5\Lab_5\images\white_swords.png"));
                else image1.Source = new BitmapImage(new Uri(@"D:\\Projects\Old\Lab_5\Lab_5\images\black_swords.png"));
            }
            else if (warior is Bowman)
            {
                if (warior.Owner.Side) image1.Source = new BitmapImage(new Uri(@"D:\\Projects\Old\Lab_5\Lab_5\images\white_bow.png"));
                else image1.Source = new BitmapImage(new Uri(@"D:\\Projects\Old\Lab_5\Lab_5\images\black_bow.png"));
            }
            Canvas.SetTop(image1, position.Y - 40);
            Canvas.SetLeft(image1, position.X - 40);
            Canvas.Children.Add(image1);

            textBlock.Text = " HP:" + warior.HP + "; D:" + warior.Distance + "; A?:" + ((warior.Attacking) ? "+" : "-");
            textBlock.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            Canvas.SetLeft(textBlock, position.X - 40);
            Canvas.SetTop(textBlock, position.Y + 37);
            Canvas.Children.Add(textBlock);
        }

        public void DrawCastle(Building build)
        {
            Point position = build.Place.Center;
            Image image1 = new Image();
            if (build.Owner.Side) image1.Source = new BitmapImage(new Uri(@"D:\\Projects\Old\Lab_5\Lab_5\images\white_castle.png"));
            else image1.Source = new BitmapImage(new Uri(@"D:\\Projects\Old\Lab_5\Lab_5\images\black_castle.png"));
            Canvas.SetTop(image1, position.Y - 45);
            Canvas.SetLeft(image1, position.X - 40);
            Canvas.Children.Add(image1);
            if (build.Place.Warior == null)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = "wh:" + build.BringResourses[0] + ";g:" + build.BringResourses[1] + ";w:" + build.BringResourses[2];
                textBlock.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                Canvas.SetLeft(textBlock, position.X - 40);
                Canvas.SetTop(textBlock, position.Y + 37);
                Canvas.Children.Add(textBlock);
            }
        }

        public void DrawHex(Hexagone Hexag, bool select = false)
        {
            double X = Hexag.Center.X;
            double Y = Hexag.Center.Y;
            Polygon polygon = new Polygon();
            PointCollection Hex = new PointCollection();
            byte r, g, b;
            r = g = b = 255;

            if (Hexag.Type == 1) r = b = 0;
            else if (Hexag.Type == 3) { r = 255; b = 63; g = 133; }
            else if (Hexag.Type == 2) { r = 255; b = 0; g = 215; }
            else r = g = b = 0;

            Hex.Add(new Point(-70 + X, 0 + Y));
            Hex.Add(new Point(-35 + X, 60 + Y));
            Hex.Add(new Point(35 + X, 60 + Y));
            Hex.Add(new Point(70 + X, 0 + Y));
            Hex.Add(new Point(35 + X, -60 + Y));
            Hex.Add(new Point(-35 + X, -60 + Y));
            polygon.Points = Hex;
            polygon.Fill = new SolidColorBrush(Color.FromRgb(r, g, b));
            Canvas.Children.Add(polygon);

            r = g = b = 0;
            if (select) r = 255;
            for (int i = 0; i < 6; i++)
            {
                if (i != 5) DrawLine(Hex[i], Hex[i + 1], Color.FromRgb(r, 0, 0));
                else DrawLine(Hex[i], Hex[0], Color.FromRgb(r, 0, 0));
            }
            if (Hexag.Build != null) DrawCastle(Hexag.Build);
            if (Hexag.Warior != null) DrawWarior(Hexag.Warior);
        }

        public void DrawLine(Point Start, Point End, Color Color)
        {
            Line line = new Line();
            line.X1 = Start.X;
            line.X2 = End.X;
            line.Y1 = Start.Y;
            line.Y2 = End.Y;
            line.Stroke = new SolidColorBrush(Color);
            line.StrokeThickness = 3;
            Canvas.Children.Add(line);
        }
    }
}
