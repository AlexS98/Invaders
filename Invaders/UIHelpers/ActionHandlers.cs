using Invaders.GameModels.Map;
using Invaders.GameProcess;

namespace Invaders.UIHelpers
{
    internal class ActionHandlers
    {
        public Hexagon Selected { get; set; }
        public Game CurrentGame { get; set; }

        public void Reset()
        {
            Selected = null;
        }

        public UiModel ToUiModel()
        {
            UiModel model = new UiModel();
            model = CurrentGame.PlayingNow.ToUiModel();
            if (Selected == null) return model;
            model.HexBuild = "Build: " + (Selected.Build?.GetType().Name ?? "None");
            model.HexWarior = "Warior: " + (Selected.Warior?.GetType().Name ?? "None");
            model.HexType = "Type: " + Selected.Type;
            model.HexOwner = "Owner: " + (Selected.Owner?.Name ?? "None");
            if (Selected.Warior != null)
            {
                model.WariorAttackDistance = "AttackDistance: " + Selected.Warior.AttackDistance;
                model.WariorAttackRate = "Atack rate: " + Selected.Warior.AttackRate;
                model.WariorAttacking = "Attacking: " + (Selected.Warior.Attacking ? "yes" : "no");
                model.WariorDistance = "Distance: " + Selected.Warior.Distance;
                model.WariorOwner = "Owner: " + Selected.Warior.Owner.Name;
                model.WariorType = "Type: " + Selected.Warior.GetType().Name;
            }

            if (Selected.Build != null)
            {
                model.BuildingType = "Type: " + Selected.Build.GetType().Name;
                model.BuildingResGold = "Gold: " + Selected.Build.BringResourses.Gold;
                model.BuildingResWheat = "Wheat: " + Selected.Build.BringResourses.Wheat;
                model.BuildingResWood = "Wood: " + Selected.Build.BringResourses.Wood;
                model.BuildingNum = "0";
            }
            return model;
        }
    }
}
