using System.Collections.Generic;
using Invaders.GameModels.Additional;
using Invaders.GameModels.Buildings;
using Invaders.GameModels.Wariors;
using Invaders.UIHelpers;

namespace Invaders.GameProcess
{
    internal sealed class Player
    {
        private List<Warior> _army;
        private List<Building> _buildings;
        public string Name { get; }
        public bool Side { get; }
        public int WariorsLimit{ get; }
        public GameResources PlayerResources { private set; get; }
        public int ArmyNow { get { return _army.Count; } }
        public int BuildNow { get { return _buildings.Count; } }
        public Player Enemy { get; set; }

        public Player(bool side, string name)
        {
            Name = name;
            Side = side;
            WariorsLimit = 5;
            PlayerResources = new GameResources(100, 70, 100);
            _army = new List<Warior>();
            _buildings = new List<Building>();
        }

        public Player(bool side, string name, int limit, GameResources resources)
        {
            Name = name;
            Side = side;
            WariorsLimit = limit;
            PlayerResources = resources;
            _army = new List<Warior>();
            _buildings = new List<Building>();
        }

        public void KillWarior(Warior warior) => _army.Remove(warior);

        public void LostBuild(Building build) => _buildings.Remove(build);

        public void CaptureBuild(Building build) => _buildings.Add(build);

        private void Pay(GameResources cost) => PlayerResources -= cost;

        public string InfoArmy() => $"Army size: {_army.Count}/{WariorsLimit}";

        public bool HireWarior(Warior warior)
        {
            if (WariorsLimit - _army.Count > 0 && GameResources.EnoughResources(PlayerResources, warior.Cost))
            {
                PlayerResources -= warior.Cost;
                _army.Add(warior);
                return true;
            }
            else
            {
                return false;
            }
        }

        private void CollectResources()
        {
            foreach (Building item in _buildings)
            {
                PlayerResources += item.BringResourses;
            }
        }
        public bool CreateBuilding(Building build)
        {
            if (GameResources.EnoughResources(PlayerResources, build.Price))
            {
                Pay(build.Price);
                _buildings.Add(build);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void NewTurn()
        {
            foreach (Warior item in _army)
            {
                item.NewTurn();
            }
            CollectResources();
        }

        public UiModel ToUiModel()
        {
            return new UiModel()
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
