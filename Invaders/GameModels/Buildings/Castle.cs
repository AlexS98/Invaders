using Invaders.GameModels.Additional;

namespace Invaders
{
    class Castle : Building
    {
        public Castle(Hexagone hex, Player owner) : base(hex, owner)
        {
            Price = new Resources(wood: 50, gold: 20);                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 
        }

        public void SetCollection(int wheat, int wood, int gold)
        {
            BringResourses = new Resources(wheat, wood, gold);
        }

        public void SetCollection(Resources cost)
        {
            BringResourses = cost;
        }
    }
}
