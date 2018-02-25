using Invaders.GameModels.Map;
using Invaders.UIHelpers;
using System.Collections.Generic;
using System.Windows;

namespace Invaders.GameProcess
{
    internal sealed class Game
    {
        private IList<Hexagon> _map = new List<Hexagon>();
        private Player Player1 { get; }
        private Player Player2 { get; }
        public IList<Hexagon> Map
        {
            get { return _map; }
            private set { _map = value; }
        }
        public int Turn { get; private set; }
        public Player PlayingNow { get; private set; }
        public ActionHandlers Handlers { get; }

        public Game(StartGameModel model, Point canvasSize)
        {
            int difficulty = (int)model.MapSize;
            Player1 = new Player(true, model.FirstName, (difficulty - 15) / 2, model.StartResources.Copy);
            Player2 = new Player(false, model.SecondName, (difficulty - 15) / 2, model.StartResources.Copy);
            Player1.Enemy = Player2;
            Player2.Enemy = Player1;
            var creator = new MapCreator();
            creator.CreateMap(ref _map, model.MapSize, canvasSize);
            PlayingNow = Player1;
            Handlers = new ActionHandlers();
        }

        public void EndTurn()
        {
            Turn++;
            PlayingNow = PlayingNow.Enemy;
            PlayingNow.NewTurn();
        }

        public UiModel ToUiModel()
        {
            return PlayingNow.ToUiModel();
        }
    }
}
