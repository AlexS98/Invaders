using Invaders.GameModels.Additional;

namespace Invaders
{
    internal sealed class Castle : Building
    {
        public Castle(Hexagon hex, Player owner) : base(hex, owner)
        {
            Price = new GameResources(wood: 50, gold: 20);                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 
        }

        public void SetCollection(int wheat, int wood, int gold)
        {
            BringResourses = new GameResources(wheat, wood, gold);
        }

        public void SetCollection(GameResources cost)
        {
            BringResourses = cost;
        }
    }
}
