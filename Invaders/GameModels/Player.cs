using Invaders.GameModels.Additional;
using System.Collections.Generic;

namespace Invaders
{
    public class Player
    {
        List<Wariors> Army;
        List<Building> Buildings;
        public bool Side { private set; get; }
        public int WariorsLimit{ private set; get; }
        public Price PlayerResources { set; get; }
        public int ArmyNow { get { return Army.Count; } }
        public int BuildNow { get { return Buildings.Count; } }

        public Player(bool col)
        {
            Side = col;
            WariorsLimit = 5;
            PlayerResources = new Price(100, 70, 100);
            Army = new List<Wariors>();
            Buildings = new List<Building>();
        }
        public Player(bool col, int limit, Price price)
        {
            Side = col;
            WariorsLimit = limit;
            PlayerResources = price;
            Army = new List<Wariors>();
            Buildings = new List<Building>();
        }

        public void KillWarior(Wariors warior) => Army.Remove(warior);
        public void LostBuild(Building build) => Buildings.Remove(build);
        public void CaptureBuild(Building build) => Buildings.Add(build);
        private void Pay(Price cost) => PlayerResources -= cost;
        public string InfoArmy() => $"Army size: {(Army.Count).ToString()}/{WariorsLimit}";

        public bool HireWarior(Wariors warior)
        {
            if ((WariorsLimit - (Army.Count)) > 0 && Price.EnoughResources(PlayerResources, warior.Cost))
            {
                PlayerResources -= warior.Cost;
                Army.Add(warior);
                return true;
            }
            else { return false; }
        }

        public void CollectResources()
        {
            foreach (Building item in Buildings)
            {
                PlayerResources += item.BringResourses;
            }
        }
        public bool CreateBuilding(Building build)
        {
            if (Price.EnoughResources(PlayerResources, build.Price))
            {
                Pay(build.Price);
                Buildings.Add(build);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void NewTurn()
        {
            foreach (Wariors item in Army)
            {
                item.NewTurn();
            }
            this.CollectResources();
        }
    }
}
