using Invaders.GameModels.Additional;
using Invaders.UIHandlers;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Invaders
{
    public partial class MainWindow : Window
    {
        List<Hexagone> Field { get; set; }
        Hexagone Selected;
        Player PlayingNow;
        Player Light { get; set; }
        Player Dark { get; set; }
        FieldDrawing FieldDrawing { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            btnEnd2.IsEnabled = false;
            FieldDrawing = new FieldDrawing(Canvas);
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

            Light = new Player(true);
            Dark = new Player(false);

            PlayingNow = Light;

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
            PlayingNow = Light;
            btnEnd1.IsEnabled = true;
            btnEnd2.IsEnabled = false;
            Selected = null;
            Dark.NewTurn();
            RefreshField();
            if (PlayingNow.ArmyNow == 0 && PlayingNow.BuildNow == 0 && PlayingNow.PlayerResources.Wood < 50)
            {
                MessageBox.Show("BLACK PLAYER IS WINNER!");
            }
        }

        private void btnEnd1_Click(object sender, RoutedEventArgs e)
        {
            PlayingNow = Dark;
            btnEnd2.IsEnabled = true;
            btnEnd1.IsEnabled = false;
            Selected = null;
            Light.NewTurn();
            RefreshField();
            if (PlayingNow.ArmyNow == 0 && PlayingNow.BuildNow == 0 && PlayingNow.PlayerResources.Wood < 50)
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
                        FieldDrawing.DrawHex(Selected);
                        Selected = null;
                        FieldDrawing.DrawHex(item);
                    }
                    else if (Selected != null && Selected != item && Selected.Warior != null && item.Warior != null && item.Warior.Owner != Selected.Warior.Owner && Selected.IsNeighbor(item, Selected.Warior.AttackDistance) && Selected.Warior.Owner == PlayingNow)
                    {
                        Selected.Warior.Damaging(item.Warior);
                        FieldDrawing.DrawHex(Selected);
                        Selected = null;
                        FieldDrawing.DrawHex(item);
                    }
                    else
                    {
                        Selected = item;
                        FieldDrawing.DrawHex(item, true);
                    }
                    break; 
                }
            }
            
        }
        

        public void RefreshField()
        {
            InfoA.Content = Light.InfoArmy();
            InfoB.Content = Dark.InfoArmy();
            InfoA_gold.Content = "Gold: " +   Light.PlayerResources.Gold;
            InfoA_wood.Content = "Wood: " +   Light.PlayerResources.Wood;
            InfoA_wheat.Content = "Wheat: " + Light.PlayerResources.Wheat;
            InfoB_gold.Content = "Gold: " +   Dark.PlayerResources.Gold;
            InfoB_wood.Content = "Wood: " +   Dark.PlayerResources.Wood;
            InfoB_wheat.Content = "Wheat: " + Dark.PlayerResources.Wheat;
            foreach (Hexagone item in Field) FieldDrawing.DrawHex(item);
        }

        
    }
}
