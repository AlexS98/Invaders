using System;
using System.Collections.Generic;
using System.Windows;
using Invaders.GameModels.Additional;
using Invaders.GameModels.Map;
using Invaders.GameProcess;

namespace Invaders.GameModels.Buildings
{
    internal abstract class Building
    {
        public GameResources BringResourses { get; protected set; }
        public GameResources Price { get; protected set; }
        public Hexagon Place { get; }
        public Player Owner { private set; get; }

        public void Capture(Player player)
        {
            Owner.LostBuild(this);
            Owner = player;
            player.CaptureBuild(this);
            MessageBox.Show("Building is captured!");
        }

        protected Building(Hexagon pl, Player ow )
        {
            Price = new GameResources(wood: 50, gold: 20);
            Place = pl;
            Owner = ow;
        }

        public void CalculateResources(IList<Hexagon> map)
        {
            if (map == null) throw new ArgumentNullException(nameof(map));
            BringResourses = new GameResources();
            foreach (var item in map)
            {
                if (Place.IsNeighbor(item) && item.Owner == Owner.Side)
                    BringResourses[(int)item.Type - 1] += 10;
            }
        }
    }
}
