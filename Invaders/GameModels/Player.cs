using System.Collections.Generic;

namespace Invaders
{
    public class Player
    {
        List<Wariors> Army;
        List<Building> Buildings;
        public bool Color { private set; get; }
        public int WariorsLimit{ private set; get; }
        public int Gold { set; get; }
        public int Wood { set; get; }
        public int Wheat { set; get; }
        public int ArmyNow { get { return Army.Count; } }
        public int BuildNow { get { return Buildings.Count; } }

        public Player(bool col)
        {
            Color = col;
            WariorsLimit = 5;
            Gold = 100;
            Wood = 70;
            Wheat = 100;
            Army = new List<Wariors>();
            Buildings = new List<Building>();
        }
        public Player(bool col, int limit, int gold, int wood, int wheat)
        {
            Color = col;
            WariorsLimit = limit;
            Gold = gold;
            Wood = wood;
            Wheat = wheat;
            Army = new List<Wariors>();
            Buildings = new List<Building>();
        }
        public bool HireWarior(Wariors warior)
        {
            bool success = false;
            if ((WariorsLimit - (Army.Count)) > 0 && EnoughResources(warior.Cost)) 
            {
                Army.Add(warior);
                this.Wheat -= warior.Cost[0];
                this.Gold -= warior.Cost[1];
                this.Wood -= warior.Cost[2];
                success = true;
            }
            return success;
        }

        public bool EnoughResources(int[] price)
        {
            if (price[0] > this.Wheat || price[1] > this.Gold || price[2] > this.Wood) return false;
            else return true;
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
                Wheat += item.BringResourses[0];
                Gold += item.BringResourses[1];
                Wood += item.BringResourses[2];
            }
        }
        public bool CreateBuilding(Building build)
        {
            if (EnoughResources(build.Price))
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
        private void Pay(int[] cost)
        {
            Gold -= cost[1];
            Wood -= cost[2];
            Wheat -= cost[0];
        }
    }
}
