using System;
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
        private ActionHandlers GameHandler { get; set; }
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
            GameHandler = new ActionHandlers
            {
                Selected = null,
                CurrentGame = CurrentGame
            };
            RefreshField();
        }

        

        private void OnHireEvent(object sender, HireEventArgs e)
        {
            if (GameHandler.Selected == null || !GameHandler.Selected.NearBuilding(CurrentGame.Map) ||
                GameHandler.Selected.Owner.Side != CurrentGame.PlayingNow.Side) return;
            Warior warior;
            switch (e.WariorType)
            {
                case "Knight":
                    warior = new Knight(GameHandler.Selected, CurrentGame.PlayingNow);
                    break;
                case "Swordsman":
                    warior = new Swordsman(GameHandler.Selected, CurrentGame.PlayingNow);
                    break;
                case "Bowman":
                    warior = new Bowman(GameHandler.Selected, CurrentGame.PlayingNow);
                    break;
                default:
                    warior = null;
                    break;
            }
            if (warior != null && CurrentGame.PlayingNow.HireWarior(warior))
            {
                GameHandler.Selected.AddWarior(warior);
                RefreshField();
                TransitionAbilities(GameHandler.Selected);
                AttackAbilities(GameHandler.Selected);
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
            if (GameHandler.Selected == null) return;
            if (!GameHandler.Selected.NearBuilding(CurrentGame.Map))
            {
                Building b = new Castle(GameHandler.Selected, CurrentGame.PlayingNow);
                if (!CurrentGame.PlayingNow.CreateBuilding(b)) return;
                GameHandler.Selected.AddBuilding(b);
                foreach (Hexagon item in CurrentGame.Map)
                    if (item.IsNeighbor(GameHandler.Selected) && item.Owner == null)
                        item.Owner = CurrentGame.PlayingNow;
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
            GameHandler.Selected = null;
            CurrentGame.EndTurn();
            RefreshField();
            LbTurn.Content = $"TURN: {CurrentGame.Turn / 2}";
            if (CurrentGame.PlayingNow.ArmyNow == 0 && CurrentGame.PlayingNow.BuildNow == 0
                                                    && CurrentGame.PlayingNow.PlayerResources.Wood < 50)
            {
                MessageBox.Show($"{CurrentGame.PlayingNow.Enemy.Name} PLAYER IS WINNER!");
            }
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed)
            {
                GameHandler.Selected = null;
                RefreshField();
            }
            else
            {
                foreach (Hexagon item in CurrentGame.Map)
                {
                    if (!item.MouseHit(e.GetPosition(Canvas))) continue;
                    if (GameHandler.Selected != null && GameHandler.Selected != item && GameHandler.Selected.Warior != null
                        && item.Warior == null && GameHandler.Selected.IsNeighbor(item) && 
                        GameHandler.Selected.Warior.Owner == CurrentGame.PlayingNow)
                    {
                        GameHandler.Selected.Warior.Move(item);
                        if (item.Build != null && item.Build.Owner != CurrentGame.PlayingNow)
                        {
                            item.Build.Capture(CurrentGame.PlayingNow);
                            foreach (Hexagon i in CurrentGame.Map)
                                if (i.IsNeighbor(item))
                                    i.Owner = CurrentGame.PlayingNow;
                            item.Build.CalculateResources(CurrentGame.Map);
                        }
                        GameHandler.Selected = item;
                        RefreshField();
                        TransitionAbilities(item);
                        AttackAbilities(item);
                    }
                    else if (GameHandler.Selected != null && GameHandler.Selected != item && GameHandler.Selected.Warior != null
                             && item.Warior != null && item.Warior.Owner != GameHandler.Selected.Warior.Owner &&
                             GameHandler.Selected.IsNeighbor(item, GameHandler.Selected.Warior.AttackDistance) && 
                             GameHandler.Selected.Warior.Owner == CurrentGame.PlayingNow)
                    {
                        GameHandler.Selected.Warior.Damaging(item.Warior);
                        GameHandler.Selected = null;
                        RefreshField();
                    }
                    else
                    {
                        GameHandler.Selected = item;
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
            foreach (Hexagon i in CurrentGame.Map)
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
            foreach (Hexagon i in CurrentGame.Map)
            {
                if (i.IsNeighbor(item, item.Warior.AttackDistance) && i.Warior != null && i.Warior.Owner.Side != item.Warior.Owner.Side)
                {
                    FieldDrawing.DrawEllipse(i, 255, strokeThickness: 3, aim: true);
                }
            }
        }

        private void RefreshField()
        {
            Canvas.Children.Clear();
            Model = GameHandler.ToUiModel();
            LbName.Content = Model.Name;
            LbArmy.Content = "Army: " + Model.Army;
            LbWheat.Content = "Wheat: " + Model.Wheat;
            LbWood.Content = "Wood: " + Model.Wood;
            LbGold.Content = "Gold: " + Model.Gold;

            LbBuildingResGold.Content = Model.BuildingResGold;
            LbBuildingResWheat.Content = Model.BuildingResWheat;
            LbBuildingResWood.Content = Model.BuildingResWood;
            LbBuildingType.Content = Model.BuildingType;

            LbWariorAttackDistance.Content = Model.WariorAttackDistance;
            LbWariorAttackRate.Content = Model.WariorAttackRate;
            LbWariorAttacking.Content = Model.WariorAttacking;
            LbWariorDistance.Content = Model.WariorDistance;
            LbWariorOwner.Content = Model.WariorOwner;
            LbWariorType.Content = Model.WariorType;

            LbHexBuild.Content = Model.HexBuild;
            LbHexOwner.Content = Model.HexOwner;
            LbHexType.Content = Model.HexType;
            LbHexWarior.Content = Model.HexWarior;

            foreach (Hexagon item in CurrentGame.Map)
            {
                FieldDrawing.DrawHex(item);
            }
            FieldDrawing.DrawBoundaries(CurrentGame.Map);
            if(GameHandler.Selected != null) FieldDrawing.DrawBorder(GameHandler.Selected, 255, 0, 0);
        }

        private void BtnMenu_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
