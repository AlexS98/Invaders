using Invaders.GameModels.Exceptions;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Invaders.UIHelpers
{
    internal sealed class DrawingHandler
    {
        private Canvas Canvas { get; set; }

        public DrawingHandler(Canvas canvas)
        {
            Canvas = canvas;
        }

        public void DrawWarior(Wariors warior, Point point = new Point(), bool description = true)
        {
            Point position = (point.X == 0 && point.Y == 0) ? warior.Place.Center : point;
            Image image = WariorPicture(warior);
            Canvas.SetTop(image, position.Y - 40);
            Canvas.SetLeft(image, position.X - 40);
            Canvas.Children.Add(image);

            if (description)
            {
                TextBlock textBlock = new TextBlock
                {
                    Text = $" HP:{warior.HP}; D:{warior.Distance}; A?:" + ((warior.Attacking) ? "+" : "-"),
                    Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0))
                };
                Canvas.SetLeft(textBlock, position.X - 40);
                Canvas.SetTop(textBlock, position.Y + 37);
                Canvas.Children.Add(textBlock);
            }
        }

        private Image WariorPicture(Wariors warior)
        {
            Image image = new Image();
            if (warior is Knight)
            {
                image.Source = new BitmapImage(new Uri(((warior.Owner.Side) ? "images/white_horse.png" : "images/black_horse.png"), UriKind.Relative));
            }
            else if (warior is Swordsman)
            {
                image.Source = new BitmapImage(new Uri(((warior.Owner.Side) ? "images/white_swords.png" : "images/black_swords.png"), UriKind.Relative));
            }
            else if (warior is Bowman)
            {
                image.Source = new BitmapImage(new Uri(((warior.Owner.Side) ? "images/white_bow.png" : "images/black_bow.png"), UriKind.Relative));
            }
            else
            {
                throw new GameException("Unknown warior type");
            }
            return image;
        }

        public void DrawCastle(Building build, bool description = true)
        {
            Point position = build.Place.Center;
            Image image1 = new Image
            {
                Source = new BitmapImage(new Uri(((build.Owner.Side) ? "images/white_castle.png" : "images/black_castle.png"), UriKind.Relative))
            };
            Canvas.SetTop(image1, position.Y - 45);
            Canvas.SetLeft(image1, position.X - 40);
            Canvas.Children.Add(image1);

            if (build.Place.Warior == null && description)
            {
                TextBlock textBlock = new TextBlock
                {
                    Text = build.BringResourses.ToString(),
                    Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0))
                };
                Canvas.SetLeft(textBlock, position.X - 40);
                Canvas.SetTop(textBlock, position.Y + 37);
                Canvas.Children.Add(textBlock);
            }
        }

        public void DrawHex(Hexagon hexagone)
        {
            Polygon polygon = new Polygon();
            PointCollection hex = hexagone.PointCollection();
            polygon.Points = hex;

            byte r, g, b;
            if ((int)hexagone.Type == 1) { r = b = 0; g = 255; }
            else if ((int)hexagone.Type == 2) { r = 255; b = 63; g = 133; }
            else if ((int)hexagone.Type == 3) { r = 255; b = 0; g = 215; }
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

        public void DrawBorder(Hexagon hexagone, byte red = 0, byte green = 0, byte blue = 0)
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

        public void DrawEllipse(Hexagon hexagon, byte red = 0, byte green = 0, byte blue = 0, byte strokeThickness = 2, bool aim = false)
        {
            byte r = red;
            byte g = green;
            byte b = blue;
            Ellipse ellipse = new Ellipse
            {
                Width = 110,
                Height = 110,
                Stroke = new SolidColorBrush(Color.FromRgb(r,g,b)),
                StrokeThickness = strokeThickness
            };
            Canvas.SetLeft(ellipse, hexagon.Center.X - 55);
            Canvas.SetTop(ellipse, hexagon.Center.Y - 55);
            Canvas.Children.Add(ellipse);

            if (aim)
            {
                double Kx, Ky, k, c, Tx, Ty, D, X, L = 31;
                double Cy = hexagon.Center.Y;
                double Cx = hexagon.Center.X;
                foreach (Point item in hexagon.PointCollection())
                {
                    Ky = item.Y;
                    Kx = item.X;
                    k = (Cy - Ky) / (Cx - Kx);
                    c = Cy - k * Cx;
                    X = Ky - c;
                    D = 4 * Math.Pow(X * k + Kx, 2) - 4 * (k * k + 1) * (Kx * Kx + X * X - L * L);
                    if (D < 0) throw new GameException("Error in aim!");
                    Tx = (Kx > Cx) ? (2 * (k * X + Kx) - Math.Sqrt(D)) : (2 * (k * X + Kx) + Math.Sqrt(D));
                    Tx /= (2 * (k * k + 1));
                    Ty = k * Tx + c;
                    DrawLine(item, new Point(Tx, Ty), Color.FromRgb(r, g, b));
                }
            }
        }

        public void DrawLine(Point Start, Point End, Color Color, byte strokeThickness = 3)
        {
            Line line = new Line
            {
                X1 = Start.X,
                X2 = End.X,
                Y1 = Start.Y,
                Y2 = End.Y,
                Stroke = new SolidColorBrush(Color),
                StrokeThickness = strokeThickness
            };
            Canvas.Children.Add(line);
        }

        public void MoveAnimation(Hexagon start, Hexagon end)
        {

        }
    }
}
