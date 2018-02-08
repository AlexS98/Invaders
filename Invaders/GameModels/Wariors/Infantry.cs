namespace Invaders
{
    internal class Infantry : Wariors
    {
        protected Infantry(Hexagon place, Player owner) : base(place, owner)
        {
            Protector = delegate (int a) {
                if (Place.Build != null && HP < a) HP += 1;
            };
        }

        public override void Attack()
        {
            throw new System.NotImplementedException();
        }

        public override void NewTurn()
        {
            Distance = 1;
        }
    }
}
