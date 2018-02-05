using Invaders.GameModels.Additional;

namespace Invaders
{
    public abstract class Building
    {
        public Price BringResourses { get; set; }
        public Price Price { get; set; }
        public Hexagone Place { set; get; }
        public Player Owner { set; get; }

        public void Capture(Player player)
        {
            Owner.LostBuild(this);
            Owner = player;
            player.CaptureBuild(this);
        }
        protected Building(Hexagone pl, Player ow )
        {
            Price = new Price(wood: 50, gold: 20);
            Place = pl;
            Owner = ow;
        }
    }
}
