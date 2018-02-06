namespace Invaders.GameModels.Additional
{
    class GameEvent
    {
        public Hexagone EventPlace { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return Message;
        }
    }
}
