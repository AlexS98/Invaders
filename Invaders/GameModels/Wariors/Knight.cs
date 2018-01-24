namespace Invaders
{
    class Knight : Wariors
    {
        public Knight(Hexagone place, Player owner) : base (place, owner)
        {
            Distance = 2;
            HP = 6;
            AttackRate = 4;
            AttackDistance = 1;
            Cost = new int[3];
            Cost[0] = 15;
            Cost[1] = 20;
            Cost[2] = 0;
        }

        public override void NewTurn()
        {
            Distance = 2;
            Attacking = false;
        }
    }
}
