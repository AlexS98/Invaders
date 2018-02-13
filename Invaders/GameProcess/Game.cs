using System.Collections.Generic;

namespace Invaders.GameProcess
{
    internal sealed class Game
    {
        public Player Player1 { private set; get; }
        public Player Player2 { private set; get; }
        public IList<Hexagon> Map { private set; get; }
        private Player PlayNow { get; set; }

        public Game(bool playNow = true)
        {
        }
    }
}
