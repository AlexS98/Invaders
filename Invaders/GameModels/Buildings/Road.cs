using Invaders.GameModels.Map;
using Invaders.GameProcess;

namespace Invaders.GameModels.Buildings
{
    class Road : Building
    {
        public Road(Hexagon hex, Player owner) : base(hex, owner)
        { }
    }
}
