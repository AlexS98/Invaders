using Invaders.GameModels.Additional;
using Invaders.GameModels.Map;
using Invaders.GameProcess;

namespace Invaders.GameModels.Wariors
{
    internal sealed class Bowman : Infantry
    {
        public Bowman(Hexagon place, Player owner) : base (place, owner)
        {
            Distance = 1;
            HealthPoints = 3;
            AttackRate = 3;
            AttackDistance = 2;
            Cost = new GameResources(wheat: 15, wood: 7, gold: 4);
        }

        public override void NewTurn()
        {
            base.NewTurn();
            Protector?.Invoke(3);
            Attacking = false;
        }
    }
}
