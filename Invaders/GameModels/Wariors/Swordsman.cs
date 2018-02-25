using Invaders.GameModels.Additional;
using Invaders.GameModels.Map;
using Invaders.GameProcess;

namespace Invaders.GameModels.Wariors
{
    internal sealed class Swordsman : Infantry 
    {
        public Swordsman(Hexagon place, Player owner) : base(place, owner)
        {
            Distance = 1;
            HealthPoints = 4;
            AttackRate = 2;
            AttackDistance = 1;
            Cost = new GameResources(wheat: 10, wood: 10, gold: 2);
        }

        public override void NewTurn()
        {
            base.NewTurn();
            Protector?.Invoke(5);
            Attacking = false;
        }
    }
}
