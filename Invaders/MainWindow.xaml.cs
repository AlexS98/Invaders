using Invaders.GameModels.Additional;
using Invaders.GameModels.Buildings;
using Invaders.GameModels.Map;
using Invaders.GameModels.Wariors;
using Invaders.GameProcess;
using Invaders.UIHelpers;
using System.Windows;
using System.Windows.Input;

namespace Invaders
{
    public partial class MainWindow : Window
    {
        private Game CurrentGame { get; set; }
        private DrawingHandler FieldDrawing { get; }
        private UiModel Model { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            FieldDrawing = new DrawingHandler(Canvas);
            var gameModel = new StartGameModel
            {
                FirstName = "Light",
                SecondName = "Dark",
                MapSize = MapSize.Big,
                StartResources = new GameResources(wood: 50, gold: 20)
            };
            StartGame(gameModel);
        }        

        private void StartGame(StartGameModel gameModel)
        {
            CurrentGame = new Game(gameModel, new Point(Canvas.Width, Canvas.Height));
            Model = CurrentGame.ToUiModel();
            RefreshField();
        }

        

        private void OnHireEvent(object sender, HireEventArgs e)
        {
            if (CurrentGame.Handlers.Selected == null || !CurrentGame.Handlers.Selected.NearBuilding(CurrentGame.Map) ||
                CurrentGame.Handlers.Selected.Owner != CurrentGame.PlayingNow.Side) return;
            Warior warior;
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
                RefreshField();
                TransitionAbilities(CurrentGame.Handlers.Selected);
                AttackAbilities(CurrentGame.Handlers.Selected);
            }
            else
            {
                RefreshField();
            }
        }
        
        private void BtnHire_Click(object sender, RoutedEventArgs e)
        {
            var hireWindow = new HireWindow
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
            if (CurrentGame.Handlers.Selected == null) return;
            if (!CurrentGame.Handlers.Selected.NearBuilding(CurrentGame.Map))
            {
                Building b = new Castle(CurrentGame.Handlers.Selected, CurrentGame.PlayingNow);
                if (!CurrentGame.PlayingNow.CreateBuilding(b)) return;
                CurrentGame.Handlers.Selected.AddBuilding(b);
                foreach (var item in CurrentGame.Map)
                    if (item.IsNeighbor(CurrentGame.Handlers.Selected) && item.Owner == null)
                        item.Owner = CurrentGame.PlayingNow.Side;
                b.CalculateResources(CurrentGame.Map);
                RefreshField();
            }
            else
            {
                MessageBox.Show("It's not enough resorces or near other building!");
            }
        }

        private void BtnEnd1_Click(object sender, RoutedEventArgs e)
        {
            CurrentGame.Handlers.Selected = null;
            CurrentGame.EndTurn();
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
                foreach (var item in CurrentGame.Map)
                {
                    if (!item.MouseHit(e.GetPosition(Canvas))) continue;
                    if (CurrentGame.Handlers.Selected != null && CurrentGame.Handlers.Selected != item && CurrentGame.Handlers.Selected.Warior != null && item.Warior == null && CurrentGame.Handlers.Selected.IsNeighbor(item) && CurrentGame.Handlers.Selected.Warior.Owner == CurrentGame.PlayingNow)
                    {
                        CurrentGame.Handlers.Selected.Warior.Move(item);
                        if (item.Build != null && item.Build.Owner != CurrentGame.PlayingNow)
                        {
                            item.Build.Capture(CurrentGame.PlayingNow);
                            foreach (var i in CurrentGame.Map)
                                if (i.IsNeighbor(item))
                                    i.Owner = CurrentGame.PlayingNow.Side;
                            item.Build.CalculateResources(CurrentGame.Map);
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

        private void TransitionAbilities(Hexagon item)
        {
            if (item.Warior == null) return;
            foreach (var i in CurrentGame.Map)
            {
                if (i.IsNeighbor(item, item.Warior.Distance) && i.Warior == null)
                {
                    FieldDrawing.DrawEllipse(i, blue: 255);
                }
            }
        }

        private void AttackAbilities(Hexagon item)
        {
            if (item.Warior == null || item.Warior.Attacking) return;
            foreach (var i in CurrentGame.Map)
            {
                if (i.IsNeighbor(item, item.Warior.AttackDistance) && i.Warior != null && i.Warior.Owner.Side != item.Warior.Owner.Side)
                {
                    FieldDrawing.DrawEllipse(i, red: 255, strokeThickness: 3, aim: true);
                }
            }
        }

        private void RefreshField()
        {
            Canvas.Children.Clear();
            Model = CurrentGame.PlayingNow.ToUiModel();
            lbName.Content = Model.Name;
            lbArmy.Content = "Army: " + Model.Army;
            lbWheat.Content = "Wheat: " + Model.Wheat;
            lbWood.Content = "Wood: " + Model.Wood;
            lbGold.Content = "Gold: " + Model.Gold;
            foreach (var item in CurrentGame.Map)
            {
                FieldDrawing.DrawHex(item);
            }
            FieldDrawing.DrawBoundaries(CurrentGame.Map);
            if(CurrentGame.Handlers.Selected != null) FieldDrawing.DrawBorder(CurrentGame.Handlers.Selected, 255, 0, 0);
        }

        private void BtnMenu_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
