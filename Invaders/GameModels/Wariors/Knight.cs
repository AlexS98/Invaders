using Invaders.GameModels.Additional;
using Invaders.GameModels.Map;
using Invaders.GameProcess;

namespace Invaders.GameModels.Wariors
{
    internal sealed class Knight : Warior
    {
        public Knight(Hexagon place, Player owner) : base (place, owner)
        {
            Distance = 2;
            HealthPoints = 6;
            AttackRate = 4;
            AttackDistance = 1;
            Cost = new GameResources(wheat: 40, wood: 10, gold: 25);
        }

        public override void NewTurn()
        {
            Distance = 2;
            Attacking = false;
        }
    }
}
