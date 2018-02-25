using Invaders.GameModels.Map;
using Invaders.GameProcess;

namespace Invaders.GameModels.Wariors
{
    internal class Infantry : Warior
    {
        protected Infantry(Hexagon place, Player owner) : base(place, owner)
        {
            Protector = delegate (int a) {
                if (Place.Build != null && HealthPoints < a) HealthPoints += 1;
            };
        }

        public override void NewTurn()
        {
            Distance = 1;
        }
    }
}
