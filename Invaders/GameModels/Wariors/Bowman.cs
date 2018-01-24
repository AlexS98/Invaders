namespace Invaders
{
    class Bowman : Infantry
    {
        public Bowman(Hexagone place, Player owner) : base (place, owner)
        {
            Distance = 1;
            HP = 3;
            AttackRate = 3;
            AttackDistance = 2;
            Cost = new int[3];
            Cost[0] = 7;
            Cost[1] = 15;
            Cost[2] = 0;
        }

        public override void NewTurn()
        {
            //this.Distance = 1;
            base.NewTurn();
            Attacking = false;
        }
    }
}
