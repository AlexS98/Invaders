using Invaders.GameModels.Additional;
using Invaders.GameModels.Exceptions;
using System.Collections.Generic;

namespace Invaders
{
    public class Player
    {
        List<Wariors> Army;
        List<Building> Buildings;
        public bool Color { private set; get; }
        public int WariorsLimit{ private set; get; }
        public Price Resources { set; get; }
        public int ArmyNow { get { return Army.Count; } }
        public int BuildNow { get { return Buildings.Count; } }

        public Player(bool col)
        {
            Color = col;
            WariorsLimit = 5;
            Resources = new Price(100, 70, 100);
            Army = new List<Wariors>();
            Buildings = new List<Building>();
        }
        public Player(bool col, int limit, Price price)
        {
            Color = col;
            WariorsLimit = limit;
            Resources = price;
            Army = new List<Wariors>();
            Buildings = new List<Building>();
        }
        public bool HireWarior(Wariors warior)
        {
            if ((WariorsLimit - (Army.Count)) > 0)
            {
                try {
                    Resources -= warior.Cost;
                    Army.Add(warior);
                    return true;
                }
                catch (GameException)
                {
                    return false;
                }
            }
            return false;
        }
        
        public void KillWarior(Wariors warior)
        {
            Army.Remove(warior);            
        }
        public void LostBuild(Building build)
        {
            Buildings.Remove(build);
        }
        public void CaptureBuild(Building build)
        {
            Buildings.Add(build);
        }

        public void CollectResources()
        {
            foreach (Building item in Buildings)
            {
                Resources += item.BringResourses;
            }
        }
        public bool CreateBuilding(Building build)
        {
            if (Price.EnoughResources(Resources, build.Price))
            {
                Pay(build.Price);
                Buildings.Add(build);
                return true;
            }
            else return false;
        }
        public string InfoArmy()
        {
            string l = "Army size: " + (Army.Count).ToString() + "/" + WariorsLimit;
            return l;
        }

        public void NewTurn()
        {
            foreach (Wariors item in Army)
            {
                item.NewTurn();
            }
            this.CollectResources();
        }
        private void Pay(Price cost)
        {
            Resources -= cost;
        }
    }
}
