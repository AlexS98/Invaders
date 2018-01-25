using Invaders.GameModels.Additional;

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
            Cost = new Price(wheat: 15, wood: 7, gold: 4);
        }

        public override void NewTurn()
        {
            base.NewTurn();
            Protector?.Invoke(3);
            Attacking = false;
        }
    }
}
