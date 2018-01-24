namespace Invaders
{
    class Infantry : Wariors
    {
        protected Infantry(Hexagone place, Player owner) : base(place, owner)
        {

        }

        public void Protector()
        {
            if (Place.Build != null && HP < 5)
            {
                HP += 1;
            }
        }

        public override void NewTurn()
        {
            Distance = 1;
            Protector();
        }
    }
}
