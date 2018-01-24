namespace Invaders
{
    public abstract class Building
    {
        public int[] BringResourses;
        protected int[] price;

        public Hexagone Place { set; get; }
        public Player Owner { set; get; }
        public int[] Price
        {
            set { for (int i = 0; i < 3; i++) price[i] = value[i]; }
            get { return price; }
        }

        public void Capture(Player player)
        {
            this.Owner.LostBuild(this);
            this.Owner = player;
            player.CaptureBuild(this);
        }
        protected Building(Hexagone pl, Player ow )
        {
            price = new int[3];
            price[0] = 0;
            price[1] = 20;
            price[2] = 50;
            this.Place = pl;
            this.Owner = ow;
        }
    }
}
