using Invaders.GameModels.Additional;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Invaders
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Hexagone> Field;
        Hexagone Selected;
        Player PlayingNow;
        Player white;
        Player black;

        public MainWindow()
        {
            InitializeComponent();
            btnEnd2.IsEnabled = false;
            Field = new List<Hexagone>();

            Field.Add(new Hexagone(new Point(420, 125), 1, 1));
            Field.Add(new Hexagone(new Point(420, 245), 2, 1));
            Field.Add(new Hexagone(new Point(420, 365), 2, 1));
            Field.Add(new Hexagone(new Point(420, 485), 1, 1));
            Field.Add(new Hexagone(new Point(420, 605), 1, 1));

            Field.Add(new Hexagone(new Point(525, 185), 1, 1));
            Field.Add(new Hexagone(new Point(525, 305), 1, 1));
            Field.Add(new Hexagone(new Point(525, 425), 3, 1));
            Field.Add(new Hexagone(new Point(525, 545), 1, 1));

            Field.Add(new Hexagone(new Point(315, 185), 1, 1));
            Field.Add(new Hexagone(new Point(315, 305), 1, 1));
            Field.Add(new Hexagone(new Point(315, 425), 1, 1));
            Field.Add(new Hexagone(new Point(315, 545), 1, 1));

            Field.Add(new Hexagone(new Point(630, 125), 1, 1));
            Field.Add(new Hexagone(new Point(630, 245), 3, 1));
            Field.Add(new Hexagone(new Point(630, 365), 1, 1));
            Field.Add(new Hexagone(new Point(630, 485), 3, 1));
            Field.Add(new Hexagone(new Point(630, 605), 1, 1));

            Field.Add(new Hexagone(new Point(210, 125), 1, 1));
            Field.Add(new Hexagone(new Point(210, 245), 3, 1));
            Field.Add(new Hexagone(new Point(210, 365), 1, 1));
            Field.Add(new Hexagone(new Point(210, 485), 1, 1));
            Field.Add(new Hexagone(new Point(210, 605), 2, 1));

            Field.Add(new Hexagone(new Point(105, 185), 1, 1));
            Field.Add(new Hexagone(new Point(105, 305), 1, 1));
            Field.Add(new Hexagone(new Point(105, 425), 2, 1));
            Field.Add(new Hexagone(new Point(105, 545), 3, 1));

            Field.Add(new Hexagone(new Point(735, 185), 2, 1));
            Field.Add(new Hexagone(new Point(735, 305), 1, 1));
            Field.Add(new Hexagone(new Point(735, 425), 1, 1));
            Field.Add(new Hexagone(new Point(735, 545), 1, 1));

            white = new Player(true);
            black = new Player(false);

            PlayingNow = white;

            System.Timers.Timer GameTimer = new System.Timers.Timer();
            GameTimer.Interval = 100;
            GameTimer.Elapsed += OnTimedEvent;
            GameTimer.Start();

            RefreshField();
        }

        private bool NearCastle(Hexagone place)
        {
            bool b = false;
            foreach (Hexagone item in Field)
            {
                if (place.IsNeighbor(item) && item.Build != null && item.Build.Owner == PlayingNow)
                {
                    b = true;
                    break;
                }
            }
            return b;
        }

        private void GiveRes(Building build)
        {
            build.BringResourses = new Price();
            foreach (Hexagone item in Field)
            {
                if (build.Place.IsNeighbor(item))
                {
                    switch (item.Type)
                    {
                        case 1:
                            build.BringResourses[0] += 10;
                            break;
                        case 2:
                            build.BringResourses[1] += 10;
                            break;
                        case 3:
                            build.BringResourses[2] += 10;
                            break;

                    }
                }
            }
        }
        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            if (Data.Hire != 0)
            {                
                if (Selected != null && Selected.Build == null && NearCastle(Selected))
                {
                    Wariors warior;
                    switch (Data.Hire)
                    {
                        case 1:
                            warior = new Knight(Selected, PlayingNow);
                            break;
                        case 2:
                            warior = new Swordsman(Selected, PlayingNow);
                            break;
                        case 3:
                            warior = new Bowman(Selected, PlayingNow);
                            break;
                        default:
                            warior = null;
                            break;
                    }
                    if (warior != null && PlayingNow.HireWarior(warior))
                    {
                        Selected.AddWarior(warior);
                        Selected = null;
                    }
                }
                Data.Hire = 0;
            }
        }
        
        private void btnHire_Click(object sender, RoutedEventArgs e)
        {
            HireWindow hireWindow = new HireWindow();
            hireWindow.Owner = this;
            hireWindow.Show();
        }

        private void btnMarket_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Wait in next updates!)");
        }

        private void btnBuild_Click(object sender, RoutedEventArgs e)
        {
            if (Selected != null)
            {
                Building b = new Castle(Selected, PlayingNow);
                if (PlayingNow.CreateBuilding(b) && !NearCastle(Selected))
                {
                    Selected.AddBuilding(b);
                    GiveRes(b);
                }
            }           
        }

        private void btnEnd2_Click(object sender, RoutedEventArgs e)
        {
            PlayingNow = white;
            btnEnd1.IsEnabled = true;
            btnEnd2.IsEnabled = false;
            Selected = null;
            black.NewTurn();
            RefreshField();
            if (PlayingNow.ArmyNow == 0 && PlayingNow.BuildNow == 0 && PlayingNow.Resources.Wood < 50)
            {
                MessageBox.Show("BLACK PLAYER IS WINNER!");
            }
        }

        private void btnEnd1_Click(object sender, RoutedEventArgs e)
        {
            PlayingNow = black;
            btnEnd2.IsEnabled = true;
            btnEnd1.IsEnabled = false;
            Selected = null;
            white.NewTurn();
            RefreshField();
            if (PlayingNow.ArmyNow == 0 && PlayingNow.BuildNow == 0 && PlayingNow.Resources.Wood < 50)
            {
                MessageBox.Show("WHITE PLAYER IS WINNER!");
            }
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            RefreshField();
            foreach (Hexagone item in Field)
            {
                if (item.MouseHit(e.GetPosition(Canvas))) 
                {
                    if (Selected != null && Selected != item && Selected.Warior != null && item.Warior == null && Selected.IsNeighbor(item) && Selected.Warior.Owner == PlayingNow)
                    {
                        Selected.Warior.Move(item);
                        if (item.Build != null && item.Build.Owner != PlayingNow)
                        {
                            item.Build.Capture(PlayingNow);
                        }
                        DrawHex(Selected);
                        Selected = null;
                        DrawHex(item);
                    }
                    else if (Selected != null && Selected != item && Selected.Warior != null && item.Warior != null && item.Warior.Owner != Selected.Warior.Owner && Selected.IsNeighbor(item, Selected.Warior.AttackDistance) && Selected.Warior.Owner == PlayingNow)
                    {
                        Selected.Warior.Damaging(item.Warior);
                        DrawHex(Selected);
                        Selected = null;
                        DrawHex(item);
                    }
                    else
                    {
                        Selected = item;
                        DrawHex(item, true);
                    }
                    break; 
                }
            }
            
        }
        private void DrawLine(Point Start, Point End, Color Color)
        {
            Line l = new Line();
            l.X1 = Start.X;
            l.X2 = End.X;
            l.Y1 = Start.Y;
            l.Y2 = End.Y;
            l.Stroke = new SolidColorBrush(Color);
            l.StrokeThickness = 3;
            Canvas.Children.Add(l); 
        }

        public void RefreshField()
        {
            InfoA.Content = white.InfoArmy();
            InfoB.Content = black.InfoArmy();
            InfoA_gold.Content = "Gold: " +   white.Resources.Gold;
            InfoA_wood.Content = "Wood: " +   white.Resources.Wood;
            InfoA_wheat.Content = "Wheat: " + white.Resources.Wheat;
            InfoB_gold.Content = "Gold: " +   black.Resources.Gold;
            InfoB_wood.Content = "Wood: " +   black.Resources.Wood;
            InfoB_wheat.Content = "Wheat: " + black.Resources.Wheat;
            foreach (Hexagone item in Field) DrawHex(item);
        }

        private void DrawWarior(Wariors warior)
        {
            Point position = warior.Place.Center;
            Image image1 = new Image();
            TextBlock textBlock = new TextBlock();
            if (warior is Knight)
            {
                if (warior.Owner.Color) image1.Source = new BitmapImage(new Uri(@"D:\\Projects\Old\Lab_5\Lab_5\images\white_horse.png"));
                else image1.Source = new BitmapImage(new Uri(@"D:\\Projects\Old\Lab_5\Lab_5\images\black_horse.png"));
            }
            else if (warior is Swordsman)
            {
                if (warior.Owner.Color) image1.Source = new BitmapImage(new Uri(@"D:\\Projects\Old\Lab_5\Lab_5\images\white_swords.png"));
                else image1.Source = new BitmapImage(new Uri(@"D:\\Projects\Old\Lab_5\Lab_5\images\black_swords.png"));
            }
            else if (warior is Bowman)
            {
                if (warior.Owner.Color) image1.Source = new BitmapImage(new Uri(@"D:\\Projects\Old\Lab_5\Lab_5\images\white_bow.png"));
                else image1.Source = new BitmapImage(new Uri(@"D:\\Projects\Old\Lab_5\Lab_5\images\black_bow.png"));
            }
            Canvas.SetTop(image1, position.Y - 40);
            Canvas.SetLeft(image1, position.X - 40);
            Canvas.Children.Add(image1);

            textBlock.Text = " HP:" + warior.HP + "; D:" + warior.Distance + "; A?:";
            if (warior.Attacking) textBlock.Text += "+";
            else textBlock.Text += "-";
            textBlock.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            Canvas.SetLeft(textBlock, position.X - 40);
            Canvas.SetTop(textBlock, position.Y + 37);
            Canvas.Children.Add(textBlock);
        }

        private void DrawCastle(Building build)
        {
            Point position = build.Place.Center;
            Image image1 = new Image();
            if (build.Owner.Color) image1.Source = new BitmapImage(new Uri(@"D:\\Projects\Old\Lab_5\Lab_5\images\white_castle.png"));
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

        private void DrawHex(Hexagone Hexag, bool select = false)
        {
            double X = Hexag.Center.X;
            double Y = Hexag.Center.Y;
            Polygon p = new Polygon();
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
            p.Points = Hex;
            p.Fill = new SolidColorBrush(Color.FromRgb(r, g, b));
            Canvas.Children.Add(p);

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
    }
}
