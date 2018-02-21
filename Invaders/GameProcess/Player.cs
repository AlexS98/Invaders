using Invaders.GameModels.Additional;
using Invaders.UIHelpers;
using System.Collections.Generic;
using System.Windows;

namespace Invaders
{
    internal sealed class Player
    {
        List<Wariors> army;
        List<Building> buildings;
        public string Name { get; set; }
        public bool Side { private set; get; }
        public int WariorsLimit{ private set; get; }
        public GameResources PlayerResources { set; get; }
        public int ArmyNow { get { return army.Count; } }
        public int BuildNow { get { return buildings.Count; } }
        public Player Enemy { get; set; }

        public Player(bool side, string name)
        {
            Name = name;
            Side = side;
            WariorsLimit = 5;
            PlayerResources = new GameResources(100, 70, 100);
            army = new List<Wariors>();
            buildings = new List<Building>();
        }

        public Player(bool side, string name, int limit, GameResources resources)
        {
            Name = name;
            Side = side;
            WariorsLimit = limit;
            PlayerResources = resources;
            army = new List<Wariors>();
            buildings = new List<Building>();
        }

        public void KillWarior(Wariors warior) => army.Remove(warior);

        public void LostBuild(Building build) => buildings.Remove(build);

        public void CaptureBuild(Building build) => buildings.Add(build);

        private void Pay(GameResources cost) => PlayerResources -= cost;

        public string InfoArmy() => $"Army size: {(army.Count).ToString()}/{WariorsLimit}";

        public bool HireWarior(Wariors warior)
        {
            if ((WariorsLimit - (army.Count)) > 0 && GameResources.EnoughResources(PlayerResources, warior.Cost))
            {
                PlayerResources -= warior.Cost;
                army.Add(warior);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void CollectResources()
        {
            foreach (Building item in buildings)
            {
                PlayerResources += item.BringResourses;
            }
        }
        public bool CreateBuilding(Building build)
        {
            if (GameResources.EnoughResources(PlayerResources, build.Price))
            {
                Pay(build.Price);
                buildings.Add(build);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void NewTurn()
        {
            foreach (Wariors item in army)
            {
                item.NewTurn();
            }
            CollectResources();
        }

        public UIModel ToUIModel()
        {
            return new UIModel()
            {
                Name = Name,
                Wheat = PlayerResources[0].ToString(),
                Wood = PlayerResources[1].ToString(),
                Gold = PlayerResources[2].ToString(),
                Army = $"{ArmyNow}/{WariorsLimit}"
            };
        }
    }
}
