using Invaders.GameModels.Buildings;
using Invaders.GameModels.Exceptions;
using Invaders.GameModels.Map;
using Invaders.GameModels.Wariors;
using System;
using System.Collections.Generic;
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

        private void DrawWarior(Warior warior, Point point = new Point(), bool description = true)
        {
            Point position = (Math.Abs(point.X) < 0.01 && Math.Abs(point.Y) < 0.01) ? warior.Place.Center : point;
            Image image = WariorPicture(warior);
            Canvas.SetTop(image, position.Y - 40);
            Canvas.SetLeft(image, position.X - 40);
            Canvas.Children.Add(image);

            if (!description) return;
            var textBlock = new TextBlock
            {
                Text = $" HP:{warior.HealthPoints}; D:{warior.Distance}; A?:" + ((warior.Attacking) ? "+" : "-"),
                Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0))
            };
            Canvas.SetLeft(textBlock, position.X - 40);
            Canvas.SetTop(textBlock, position.Y + 37);
            Canvas.Children.Add(textBlock);
        }

        private Image WariorPicture(Warior warior)
        {
            var image = new Image();
            switch(warior.GetType().Name)
            {
                case "Knight":
                    image.Source = new BitmapImage(new Uri(((warior.Owner.Side) ? "images/white_horse.png" : "images/black_horse.png"), UriKind.Relative));
                    break;
                case "Swordsman":
                    image.Source = new BitmapImage(new Uri(((warior.Owner.Side) ? "images/white_swords.png" : "images/black_swords.png"), UriKind.Relative));
                    break;
                case "Bowman":
                    image.Source = new BitmapImage(new Uri(((warior.Owner.Side) ? "images/white_bow.png" : "images/black_bow.png"), UriKind.Relative));
                    break;
                default:
                    throw new GameException("Unknown warior type");

            }
            return image;
        }

        private void DrawCastle(Building build, bool description = true)
        {
            Point position = build.Place.Center;
            var image1 = new Image
            {
                Source = new BitmapImage(new Uri(build.Owner.Side ? "images/white_castle.png" : "images/black_castle.png", UriKind.Relative))
            };
            Canvas.SetTop(image1, position.Y - 45);
            Canvas.SetLeft(image1, position.X - 40);
            Canvas.Children.Add(image1);

            if (build.Place.Warior != null || !description) return;
            var textBlock = new TextBlock
            {
                Text = build.BringResourses.ToString(),
                Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0))
            };
            Canvas.SetLeft(textBlock, position.X - 40);
            Canvas.SetTop(textBlock, position.Y + 37);
            Canvas.Children.Add(textBlock);
        }

        public void DrawHex(Hexagon hexagone)
        {
            var polygon = new Polygon();
            PointCollection hex = hexagone.PointCollection();
            polygon.Points = hex;

            byte r, g, b;
            switch ((int)hexagone.Type)
            {
                case 1:
                    r = b = 0; g = 255;
                    break;
                case 2:
                    r = b = 0; g = 200;
                    break;
                case 3:
                    r = 255; b = 0; g = 215;
                    break;
                default:
                    r = g = b = 0;
                    break;
            }

            polygon.Fill = new SolidColorBrush(Color.FromRgb(r, g, b));
            Canvas.Children.Add(polygon);

            if (hexagone.Type == HexType.Forest)
            {
                foreach (Point item in hexagone.Additional)
                {
                    var image1 = new Image
                    {
                        Source = new BitmapImage(new Uri("images/tree.png", UriKind.Relative))
                    };
                    if (hexagone.Build != null || hexagone.Warior != null)
                    {
                        if (!(item.Y - hexagone.Center.Y <= 20)) continue;
                        Canvas.SetTop(image1, item.Y);
                        Canvas.SetLeft(image1, item.X);
                        Canvas.Children.Add(image1);
                    }
                    else
                    {                        
                        Canvas.SetTop(image1, item.Y);
                        Canvas.SetLeft(image1, item.X);
                        Canvas.Children.Add(image1);
                    }
                }
            }

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

        public void DrawBorder(Hexagon hexagone, byte red = 105, byte green = 105, byte blue = 105)
        {
            PointCollection hex = hexagone.PointCollection();
            byte r = red;
            byte g = green;
            byte b = blue;
            for (int i = 0; i < 6; i++)
            {
                DrawLine(hex[i], i != 5 ? hex[i + 1] : hex[0], Color.FromRgb(r, g, b));
            }
        }

        public void DrawEllipse(Hexagon hexagon, byte red = 0, byte green = 0, byte blue = 0, byte strokeThickness = 2, bool aim = false)
        {
            byte r = red, g = green, b = blue;
            var ellipse = new Ellipse
            {
                Width = 110,
                Height = 110,
                Stroke = new SolidColorBrush(Color.FromRgb(r,g,b)),
                StrokeThickness = strokeThickness
            };
            Canvas.SetLeft(ellipse, hexagon.Center.X - 55);
            Canvas.SetTop(ellipse, hexagon.Center.Y - 55);
            Canvas.Children.Add(ellipse);

            if (!aim) return;
            double kx, ky, k, c, tx, ty, d, x, l = 31, cy = hexagon.Center.Y, cx = hexagon.Center.X;
            foreach (Point item in hexagon.PointCollection())
            {
                ky = item.Y;
                kx = item.X;
                k = (cy - ky) / (cx - kx);
                c = cy - k * cx;
                x = ky - c;
                d = 4 * Math.Pow(x * k + kx, 2) - 4 * (k * k + 1) * (kx * kx + x * x - l * l);
                if (d < 0) throw new GameException("Error in aim!");
                tx = (kx > cx) ? (2 * (k * x + kx) - Math.Sqrt(d)) : (2 * (k * x + kx) + Math.Sqrt(d));
                tx /= (2 * (k * k + 1));
                ty = k * tx + c;
                DrawLine(item, new Point(tx, ty), Color.FromRgb(r, g, b));
            }
        }

        private void DrawLine(Point start, Point end, Color color, byte strokeThickness = 3, bool dash = false)
        {
            Line line = new Line
            {
                X1 = start.X,
                X2 = end.X,
                Y1 = start.Y,
                Y2 = end.Y,
                Stroke = new SolidColorBrush(color),
                StrokeThickness = strokeThickness,
                StrokeDashArray = dash ? new DoubleCollection { 0.625, 0.625 } : new DoubleCollection()             
            };
            Canvas.Children.Add(line);
        }

        public void DrawBoundaries(IList<Hexagon> map)
        {
            foreach (Hexagon selected in map)
            {
                foreach(Hexagon item in map)
                {
                    if (selected.Owner == null || selected == item || !selected.IsNeighbor(item) ||
                        selected.Owner == item.Owner) continue;
                    Color color = (bool) selected.Owner.Side ? Color.FromRgb(255, 255, 255) : Color.FromRgb(0, 0, 0);
                    Point p1 = new Point(), p2 = new Point();
                    PointCollection pointCollection = selected.PointCollection();
                    if(Math.Abs(selected.Center.X - item.Center.X) < 0.01)
                    {
                        if (selected.Center.Y < item.Center.Y)
                        {
                            p1 = pointCollection[1];
                            p2 = pointCollection[2];
                        }
                        else
                        {
                            p1 = pointCollection[4];
                            p2 = pointCollection[5];
                        }
                    }
                    else if (selected.Center.X > item.Center.X)
                    {
                        if (selected.Center.Y < item.Center.Y)
                        {
                            p1 = pointCollection[0];
                            p2 = pointCollection[1];
                        }
                        else
                        {
                            p1 = pointCollection[5];
                            p2 = pointCollection[0];
                        }
                    }
                    else if (selected.Center.X < item.Center.X)
                    {
                        if (selected.Center.Y < item.Center.Y)
                        {
                            p1 = pointCollection[2];
                            p2 = pointCollection[3];
                        }
                        else
                        {
                            p1 = pointCollection[3];
                            p2 = pointCollection[4];
                        }
                    }
                    DrawLine(p1, p2, color, 8, true);
                }
            }
        }
    }
}
