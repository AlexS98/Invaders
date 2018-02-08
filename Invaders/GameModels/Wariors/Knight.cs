using Invaders.GameModels.Additional;

namespace Invaders
{
    internal sealed class Knight : Wariors
    {
        public Knight(Hexagon place, Player owner) : base (place, owner)
        {
            Distance = 2;
            HP = 6;
            AttackRate = 4;
            AttackDistance = 1;
            Cost = new Resources(wheat: 40, wood: 10, gold: 25);
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
