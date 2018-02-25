using Invaders.GameModels.Map;

namespace Invaders.GameModels.Additional
{
    class GameEvent
    {
        public Hexagon EventPlace { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return Message;
        }
    }
}
