using Invaders.GameModels.Additional;
using Invaders.GameModels.Map;
using Invaders.UIHelpers;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Invaders
{
    public partial class MainWindow : Window
    {
        IList<Hexagon> map = new List<Hexagon>();
        Hexagon Selected { get; set; }
        Player PlayingNow { get; set; }
        Player Light { get; set; }
        Player Dark { get; set; }
        DrawingHandler FieldDrawing { get; set; }
        UIModel Model { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            FieldDrawing = new DrawingHandler(Canvas);
            MapCreator mapCreator = new MapCreator();
            MapSize difficulty = MapSize.Big; 
            mapCreator.CreateMap(ref map, difficulty, new Point(Canvas.Width, Canvas.Height));
            int i = map.Count;
            Light = new Player(true, (int)(difficulty - 15) / 2, new GameResources(wood: 50, gold: 20), "Light");
            Dark = new Player(false, (int)(difficulty - 15) / 2, new GameResources(wood: 50, gold: 20), "Dark");

            Light.Enemy = Dark;
            Dark.Enemy = Light;

            PlayingNow = Light;
            Model = PlayingNow.ToUIModel();

            System.Timers.Timer GameTimer = new System.Timers.Timer(100);
            GameTimer.Elapsed += OnTimedEvent;
            GameTimer.Start();
            RefreshField();
        }        

        private bool NearCastle(Hexagon place)
        {
            foreach (Hexagon item in map)
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
            build.BringResourses = new GameResources();
            foreach (Hexagon item in map)
            {
                if (build.Place.IsNeighbor(item))
                    build.BringResourses[(int)item.Type - 1] += 10;
            }
        }

        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            if (Data.Hire != 0 && Selected != null && NearCastle(Selected))
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
            MessageBox.Show("Wait in next updates");
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
                else
                {
                    MessageBox.Show("It's not enough resorces or near other building!");
                }
            }           
        }

        private void BtnEnd1_Click(object sender, RoutedEventArgs e)
        {
            PlayingNow = PlayingNow.Enemy;
            Selected = null;
            PlayingNow.NewTurn();
            Model = PlayingNow.ToUIModel();
            RefreshField();
            if (PlayingNow.ArmyNow == 0 && PlayingNow.BuildNow == 0 && PlayingNow.PlayerResources.Wood < 50)
            {
                MessageBox.Show($"{PlayingNow.Enemy.Name} PLAYER IS WINNER!");
            }
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed)
            {
                Selected = null;
                RefreshField();
            }
            else
            {
                foreach (Hexagon item in map)
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
        }

        internal void TransitionAbilities(Hexagon item)
        {
            if (item.Warior != null)
            {
                foreach (Hexagon i in map)
                {
                    if (i.IsNeighbor(item, item.Warior.Distance) && i.Warior == null)
                    {
                        FieldDrawing.DrawEllipse(i, blue: 255);
                    }
                }
            }
        }

        internal void AttackAbilities(Hexagon item)
        {
            if (item.Warior != null && !item.Warior.Attacking)
            {
                foreach (Hexagon i in map)
                {
                    if (i.IsNeighbor(item, item.Warior.AttackDistance) && i.Warior != null && i.Warior.Owner.Side != item.Warior.Owner.Side)
                    {
                        FieldDrawing.DrawEllipse(i, red: 255, strokeThickness: 3, aim: true);
                    }
                }
            }
        }

        public void RefreshField()
        {
            Canvas.Children.Clear();
            Model = PlayingNow.ToUIModel();
            lbName.Content = Model.Name;
            lbArmy.Content = "Army: " + Model.Army;
            lbWheat.Content = "Wheat: " + Model.Wheat;
            lbWood.Content = "Wood: " + Model.Wood;
            lbGold.Content = "Gold: " + Model.Gold;
            foreach (Hexagon item in map)
            {
                FieldDrawing.DrawHex(item);
            }
            if(Selected != null) FieldDrawing.DrawBorder(Selected, red: 255);
        }

        private void BtnMenu_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
