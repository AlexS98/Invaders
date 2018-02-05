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
        Hexagone Selected { get; set; }
        Player PlayingNow { get; set; }
        Player Light { get; set; }
        Player Dark { get; set; }
        FieldDrawing FieldDrawing { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            btnEnd2.IsEnabled = false;
            FieldDrawing = new FieldDrawing(Canvas);
            Field = new List<Hexagone>
            {
                new Hexagone(new Point(420, 125), 1, 1),
                new Hexagone(new Point(420, 245), 2, 1),
                new Hexagone(new Point(420, 365), 2, 1),
                new Hexagone(new Point(420, 485), 1, 1),
                new Hexagone(new Point(420, 605), 1, 1),

                new Hexagone(new Point(525, 185), 1, 1),
                new Hexagone(new Point(525, 305), 1, 1),
                new Hexagone(new Point(525, 425), 3, 1),
                new Hexagone(new Point(525, 545), 1, 1),

                new Hexagone(new Point(315, 185), 1, 1),
                new Hexagone(new Point(315, 305), 1, 1),
                new Hexagone(new Point(315, 425), 1, 1),
                new Hexagone(new Point(315, 545), 1, 1),

                new Hexagone(new Point(630, 125), 1, 1),
                new Hexagone(new Point(630, 245), 3, 1),
                new Hexagone(new Point(630, 365), 1, 1),
                new Hexagone(new Point(630, 485), 3, 1),
                new Hexagone(new Point(630, 605), 1, 1),

                new Hexagone(new Point(210, 125), 1, 1),
                new Hexagone(new Point(210, 245), 3, 1),
                new Hexagone(new Point(210, 365), 1, 1),
                new Hexagone(new Point(210, 485), 1, 1),
                new Hexagone(new Point(210, 605), 2, 1),

                new Hexagone(new Point(105, 185), 1, 1),
                new Hexagone(new Point(105, 305), 1, 1),
                new Hexagone(new Point(105, 425), 2, 1),
                new Hexagone(new Point(105, 545), 3, 1),

                new Hexagone(new Point(735, 185), 2, 1),
                new Hexagone(new Point(735, 305), 1, 1),
                new Hexagone(new Point(735, 425), 1, 1),
                new Hexagone(new Point(735, 545), 1, 1)
            };

            Light = new Player(true);
            Dark = new Player(false);

            PlayingNow = Light;

            System.Timers.Timer GameTimer = new System.Timers.Timer(100);
            GameTimer.Elapsed += OnTimedEvent;
            GameTimer.Start();
            RefreshField();
        }

        private bool NearCastle(Hexagone place)
        {
            foreach (Hexagone item in Field)
            {
                if (place.IsNeighbor(item) && item.Build != null && item.Build.Owner == PlayingNow)
                {
                    return true;
                }
            }
            return false;
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
            if (Data.Hire != 0 && Selected != null && Selected.Build == null && NearCastle(Selected))
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
                    RefreshField();
                }
            }
            Data.Hire = 0;
        }
        
        private void BtnHire_Click(object sender, RoutedEventArgs e)
        {
            HireWindow hireWindow = new HireWindow
            {
                Owner = this
            };
            hireWindow.Show();
        }

        private void BtnMarket_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Wait in next updates!)");
        }

        private void BtnBuild_Click(object sender, RoutedEventArgs e)
        {
            if (Selected != null)
            {
                Building b = new Castle(Selected, PlayingNow);
                if (PlayingNow.CreateBuilding(b) && !NearCastle(Selected))
                {
                    Selected.AddBuilding(b);
                    GiveRes(b);
                    RefreshField();
                }
            }           
        }

        private void BtnEnd2_Click(object sender, RoutedEventArgs e)
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

        private void BtnEnd1_Click(object sender, RoutedEventArgs e)
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
                        Selected = item;
                        RefreshField();
                        TransitionAbilities(item);
                        AttackAbilities(item);
                    }
                    else if (Selected != null && Selected != item && Selected.Warior != null && item.Warior != null && item.Warior.Owner != Selected.Warior.Owner && Selected.IsNeighbor(item, Selected.Warior.AttackDistance) && Selected.Warior.Owner == PlayingNow)
                    {
                        Selected.Warior.Damaging(item.Warior);
                        Selected = null;
                        RefreshField();
                    }
                    else
                    {
                        Selected = item;
                        RefreshField();
                        TransitionAbilities(item);
                        AttackAbilities(item);
                    }
                    break;
                }
            }
        }

        public void TransitionAbilities(Hexagone item)
        {
            if (item.Warior != null)
            {
                foreach (Hexagone i in Field)
                {
                    if (i.IsNeighbor(item, item.Warior.Distance) && i.Warior == null)
                    {
                        FieldDrawing.DrawEllipse(i, blue: 255);
                    }
                }
            }
        }

        public void AttackAbilities(Hexagone item)
        {
            if (item.Warior != null && !item.Warior.Attacking)
            {
                foreach (Hexagone i in Field)
                {
                    if (i.IsNeighbor(item, item.Warior.AttackDistance) && i.Warior != null && i.Warior.Owner.Side != item.Warior.Owner.Side)
                    {
                        FieldDrawing.DrawEllipse(i, red: 255);
                    }
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
            foreach (Hexagone item in Field)
            {
                FieldDrawing.DrawHex(item);
            }
            if(Selected != null) FieldDrawing.DrawBorder(Selected, red: 255);
        }        
    }
}
