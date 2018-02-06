using System.Collections.Generic;

namespace Invaders.GameProcess
{
    internal class Game
    {
        public Player Player1 { private set; get; }
        public Player Player2 { private set; get; }
        public List<Hexagone> Map { private set; get; }
    }
}
