using Invaders.GameModels.Additional;

namespace Invaders
{
    internal sealed class Bowman : Infantry
    {
        public Bowman(Hexagon place, Player owner) : base (place, owner)
        {
            Distance = 1;
            HP = 3;
            AttackRate = 3;
            AttackDistance = 2;
            Cost = new Resources(wheat: 15, wood: 7, gold: 4);
        }

        public override void NewTurn()
        {
            base.NewTurn();
            Protector?.Invoke(3);
            Attacking = false;
        }
    }
}
