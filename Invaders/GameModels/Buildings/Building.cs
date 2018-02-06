using Invaders.GameModels.Additional;
using System.Windows;

namespace Invaders
{
    public abstract class Building
    {
        public Resources BringResourses { get; set; }
        public Resources Price { get; set; }
        public Hexagone Place { set; get; }
        public Player Owner { set; get; }

        public void Capture(Player player)
        {
            Owner.LostBuild(this);
            Owner = player;
            player.CaptureBuild(this);
            MessageBox.Show("Building is captured!");
        }
        protected Building(Hexagone pl, Player ow )
        {
            Price = new Resources(wood: 50, gold: 20);
            Place = pl;
            Owner = ow;
        }
    }
}
