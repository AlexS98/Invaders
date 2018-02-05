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
        private Canvas Canvas { get; set; }

        public FieldDrawing(Canvas canvas)
        {
            Canvas = canvas;
        }

        public void DrawWarior(Wariors warior, Point point = new Point())
        {
            Point position = (point.X == 0 && point.Y == 0) ? warior.Place.Center : point;
            Image image1 = new Image();
            if (warior is Knight)
            {
                image1.Source = new BitmapImage(new Uri(((warior.Owner.Side) ? "images/white_horse.png" : "images/black_horse.png"), UriKind.Relative));
            }
            else if (warior is Swordsman)
            {
                image1.Source = new BitmapImage(new Uri(((warior.Owner.Side) ? "images/white_swords.png" : "images/black_swords.png"), UriKind.Relative));
            }
            else if (warior is Bowman)
            {
                image1.Source = new BitmapImage(new Uri(((warior.Owner.Side) ? "images/white_bow.png" : "images/black_bow.png"), UriKind.Relative));
            }
            Canvas.SetTop(image1, position.Y - 40);
            Canvas.SetLeft(image1, position.X - 40);
            Canvas.Children.Add(image1);

            TextBlock textBlock = new TextBlock
            {
                Text = $" HP:{warior.HP}; D:{warior.Distance}; A?:" + ((warior.Attacking) ? "+" : "-"),
                Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0))
            };
            Canvas.SetLeft(textBlock, position.X - 40);
            Canvas.SetTop(textBlock, position.Y + 37);
            Canvas.Children.Add(textBlock);
        }

        public void DrawCastle(Building build)
        {
            Point position = build.Place.Center;
            Image image1 = new Image
            {
                Source = new BitmapImage(new Uri(((build.Owner.Side) ? "images/white_castle.png" : "images/black_castle.png"), UriKind.Relative))
            };
            Canvas.SetTop(image1, position.Y - 45);
            Canvas.SetLeft(image1, position.X - 40);
            Canvas.Children.Add(image1);
            if (build.Place.Warior == null)
            {
                TextBlock textBlock = new TextBlock
                {
                    Text = $"wh:{build.BringResourses[0]};w:{build.BringResourses[1]};g:{build.BringResourses[2]}",
                    Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0))
                };
                Canvas.SetLeft(textBlock, position.X - 40);
                Canvas.SetTop(textBlock, position.Y + 37);
                Canvas.Children.Add(textBlock);
            }
        }

        public void DrawHex(Hexagone hexagone)
        {
            Polygon polygon = new Polygon();
            PointCollection hex = hexagone.PointCollection();
            polygon.Points = hex;

            byte r, g, b;
            if (hexagone.Type == 1) { r = b = 0; g = 255; }
            else if (hexagone.Type == 2) { r = 255; b = 0; g = 215; }
            else if (hexagone.Type == 3) { r = 255; b = 63; g = 133; }
            else r = g = b = 0;

            polygon.Fill = new SolidColorBrush(Color.FromRgb(r, g, b));
            Canvas.Children.Add(polygon);

            if (hexagone.Build != null)
            {
                DrawCastle(hexagone.Build);
            }

            if (hexagone.Warior != null)
            {
                DrawWarior(hexagone.Warior);
            }

            DrawBorder(hexagone, 0);
        }

        public void DrawBorder(Hexagone hexagone, byte red = 0, byte green = 0, byte blue = 0)
        {
            PointCollection hex = hexagone.PointCollection();
            byte r = red;
            byte g = green;
            byte b = blue;
            for (int i = 0; i < 6; i++)
            {
                if (i != 5)
                {
                    DrawLine(hex[i], hex[i + 1], Color.FromRgb(r, g, b));
                }
                else
                {
                    DrawLine(hex[i], hex[0], Color.FromRgb(r, g, b));
                }
            }
        }

        public void DrawEllipse(Hexagone hexagone, byte red = 0, byte green = 0, byte blue = 0)
        {
            byte r = red;
            byte g = green;
            byte b = blue;
            Ellipse ellipse = new Ellipse
            {
                Width = 110,
                Height = 110,
                Stroke = new SolidColorBrush(Color.FromRgb(r,g,b)),
                StrokeThickness = 2
            };
            Canvas.SetLeft(ellipse, hexagone.Center.X - 55);
            Canvas.SetTop(ellipse, hexagone.Center.Y - 55);
            Canvas.Children.Add(ellipse);
        }

        public void DrawLine(Point Start, Point End, Color Color)
        {
            Line line = new Line
            {
                X1 = Start.X,
                X2 = End.X,
                Y1 = Start.Y,
                Y2 = End.Y,
                Stroke = new SolidColorBrush(Color),
                StrokeThickness = 3
            };
            Canvas.Children.Add(line);
        }
    }
}
