using Invaders.GameModels.Map;
using Invaders.GameProcess;

namespace Invaders.GameModels.Buildings
{
    class City : Building
    {
        public City(Hexagon hex, Player owner) : base(hex, owner)
        { }
    }
}
