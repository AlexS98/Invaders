using Invaders.GameModels.Additional;

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
            Cost = new Price(wheat: 10, wood: 10, gold: 2);
        }

        public override void NewTurn()
        {
            Protector?.Invoke(5);
            base.NewTurn();
            Attacking = false;
        }
    }
}
