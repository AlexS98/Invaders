using Invaders.GameModels.Map;

namespace Invaders.UIHelpers
{
    internal class ActionHandlers
    {
        public Hexagon Selected { get; set; }

        public void Reset()
        {
            Selected = null;
        }
    }
}
