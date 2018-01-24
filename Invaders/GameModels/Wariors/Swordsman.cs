namespace Invaders
{
    class Swordsman : Infantry 
    {
        public Swordsman(Hexagone place, Player owner) : base(place, owner)
        {
            Distance = 1;
            HP = 4;
            AttackRate = 2;
            AttackDistance = 1;
            Cost = new int[3];
            Cost[0] = 10;
            Cost[1] = 10;
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
