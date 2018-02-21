using Invaders.GameModels.Additional;
using Invaders.GameModels.Map;

namespace Invaders.UIHelpers
{
    internal class StartGameModel
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public MapSize MapSize { get; set; }
        public GameResources StartResources { get; set; }
    }
}
