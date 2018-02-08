using Invaders.GameModels.Additional;
using System.Windows;

namespace Invaders
{
    internal abstract class Building
    {
        public Resources BringResourses { get; set; }
        public Resources Price { get; set; }
        public Hexagon Place { set; get; }
        public Player Owner { set; get; }

        public void Capture(Player player)
        {
            Owner.LostBuild(this);
            Owner = player;
            player.CaptureBuild(this);
            MessageBox.Show("Building is captured!");
        }
        protected Building(Hexagon pl, Player ow )
        {
            Price = new Resources(wood: 50, gold: 20);
            Place = pl;
            Owner = ow;
        }
    }
}
