namespace Colonizators
{
    class Bowman : Infantry
    {
        public Bowman(Hexagone place, Player owner)
            : base (place, owner)
        {
            this.Distance = 1;
            this.HP = 3;
            this.AttackRate = 3;
            this.AttackDistance = 2;
            this.Cost = new int[3];
            this.Cost[0] = 7;
            this.Cost[1] = 15;
            this.Cost[2] = 0;
        }

        public override void NewTurn()
        {
            //this.Distance = 1;
            base.NewTurn();
            this.Attacking = false;
        }
    }
}
