using Invaders.GameModels.Additional;
using Invaders.GameModels.Map;
using Invaders.GameProcess;
using Invaders.UIHelpers;
using System.Windows;
using System.Windows.Input;

namespace Invaders
{
    public partial class MainWindow : Window
    {
        Game CurrentGame { get; set; } 
        DrawingHandler FieldDrawing { get; set; }
        UIModel Model { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            FieldDrawing = new DrawingHandler(Canvas);
            StartGameModel GameModel = new StartGameModel
            {
                FirstName = "Light",
                SecondName = "Dark",
                MapSize = MapSize.Small,
                StartResources = new GameResources(wood: 50, gold: 20)
            };
            StartGame(GameModel);
        }        

        private void StartGame(StartGameModel GameModel)
        {
            CurrentGame = new Game(GameModel, new Point(Canvas.Width, Canvas.Height));
            Model = CurrentGame.ToUIModel();
            RefreshField();
        }

        private void GiveRes(Building build)
        {
            build.BringResourses = new GameResources();
            foreach (Hexagon item in CurrentGame.Map)
                if (build.Place.IsNeighbor(item))
                    build.BringResourses[(int)item.Type - 1] += 10;
        }

        private void OnHireEvent(object sender, HireEventArgs e)
        {
            if (CurrentGame.Handlers.Selected != null && CurrentGame.Handlers.Selected.NearBuilding(CurrentGame.Map))
            {
                Wariors warior;
                switch (e.WariorType)
                {
                    case "Knight":
                        warior = new Knight(CurrentGame.Handlers.Selected, CurrentGame.PlayingNow);
                        break;
                    case "Swordsman":
                        warior = new Swordsman(CurrentGame.Handlers.Selected, CurrentGame.PlayingNow);
                        break;
                    case "Bowman":
                        warior = new Bowman(CurrentGame.Handlers.Selected, CurrentGame.PlayingNow);
                        break;
                    default:
                        warior = null;
                        break;
                }
                if (warior != null && CurrentGame.PlayingNow.HireWarior(warior))
                {
                    CurrentGame.Handlers.Selected.AddWarior(warior);
                }
                else { }
                RefreshField();
            }
        }
        
        private void BtnHire_Click(object sender, RoutedEventArgs e)
        {
            HireWindow hireWindow = new HireWindow
            {
                Owner = this
            };
            hireWindow.HireWarior += OnHireEvent;
            hireWindow.Show();
        }

        private void BtnMarket_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Wait in next updates");
        }

        private void BtnBuild_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentGame.Handlers.Selected != null)
            {
                Building b = new Castle(CurrentGame.Handlers.Selected, CurrentGame.PlayingNow);
                if (CurrentGame.PlayingNow.CreateBuilding(b) && !CurrentGame.Handlers.Selected.NearBuilding(CurrentGame.Map))
                {
                    CurrentGame.Handlers.Selected.AddBuilding(b);
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
            CurrentGame.Handlers.Selected = null;
            CurrentGame.EndTurn();
            Model = CurrentGame.ToUIModel();
            RefreshField();
            lbTurn.Content = $"TURN: {CurrentGame.Turn / 2}";
            if (CurrentGame.PlayingNow.ArmyNow == 0 && CurrentGame.PlayingNow.BuildNow == 0 && CurrentGame.PlayingNow.PlayerResources.Wood < 50)
            {
                MessageBox.Show($"{CurrentGame.PlayingNow.Enemy.Name} PLAYER IS WINNER!");
            }
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed)
            {
                CurrentGame.Handlers.Selected = null;
                RefreshField();
            }
            else
            {
                foreach (Hexagon item in CurrentGame.Map)
                {
                    if (item.MouseHit(e.GetPosition(Canvas)))
                    {
                        if (CurrentGame.Handlers.Selected != null && CurrentGame.Handlers.Selected != item && CurrentGame.Handlers.Selected.Warior != null && item.Warior == null && CurrentGame.Handlers.Selected.IsNeighbor(item) && CurrentGame.Handlers.Selected.Warior.Owner == CurrentGame.PlayingNow)
                        {
                            CurrentGame.Handlers.Selected.Warior.Move(item);
                            if (item.Build != null && item.Build.Owner != CurrentGame.PlayingNow)
                            {
                                item.Build.Capture(CurrentGame.PlayingNow);
                            }
                            CurrentGame.Handlers.Selected = item;
                            RefreshField();
                            TransitionAbilities(item);
                            AttackAbilities(item);
                        }
                        else if (CurrentGame.Handlers.Selected != null && CurrentGame.Handlers.Selected != item && CurrentGame.Handlers.Selected.Warior != null && item.Warior != null && item.Warior.Owner != CurrentGame.Handlers.Selected.Warior.Owner && CurrentGame.Handlers.Selected.IsNeighbor(item, CurrentGame.Handlers.Selected.Warior.AttackDistance) && CurrentGame.Handlers.Selected.Warior.Owner == CurrentGame.PlayingNow)
                        {
                            CurrentGame.Handlers.Selected.Warior.Damaging(item.Warior);
                            CurrentGame.Handlers.Selected = null;
                            RefreshField();
                        }
                        else
                        {
                            CurrentGame.Handlers.Selected = item;
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
                foreach (Hexagon i in CurrentGame.Map)
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
                foreach (Hexagon i in CurrentGame.Map)
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
            Model = CurrentGame.PlayingNow.ToUIModel();
            lbName.Content = Model.Name;
            lbArmy.Content = "Army: " + Model.Army;
            lbWheat.Content = "Wheat: " + Model.Wheat;
            lbWood.Content = "Wood: " + Model.Wood;
            lbGold.Content = "Gold: " + Model.Gold;
            foreach (Hexagon item in CurrentGame.Map)
            {
                FieldDrawing.DrawHex(item);
            }
            if(CurrentGame.Handlers.Selected != null) FieldDrawing.DrawBorder(CurrentGame.Handlers.Selected, red: 255);
        }

        private void BtnMenu_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
