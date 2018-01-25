using Invaders.GameModels.Additional;

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
            Cost = new Price(wheat: 20, gold: 15);
        }

        public override void Attack()
        {
            throw new System.NotImplementedException();
        }

        public override void NewTurn()
        {
            Distance = 2;
            Attacking = false;
        }
    }
}
