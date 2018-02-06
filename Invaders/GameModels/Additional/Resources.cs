using Invaders.GameModels.Exceptions;

namespace Invaders.GameModels.Additional
{
    public class Resources
    {
        public int Wheat { get; set; }
        public int Wood { get; set; }
        public int Gold { get; set; }

        public Resources(int wheat = 0, int wood = 0, int gold = 0)
        {
            Wheat = wheat;
            Wood = wood;
            Gold = gold;
        }

        public Resources(int[] price)
        {
            Wheat = price[0];
            Wood = price[1];
            Gold = price[2];
        }

        public int this [int n]
        {
            get {
                switch(n)
                {
                    case 0:
                        return Wheat;
                    case 1:
                        return Wood;
                    case 2:
                        return Gold;
                    default:
                        throw new GameException("Invalid index");
                }
            }
            set {
                switch (n)
                {
                    case 0:
                        Wheat = value;
                        break;
                    case 1:
                        Wood = value;
                        break;
                    case 2:
                        Gold = value;
                        break;
                    default:
                        throw new GameException("Invalid index");
                }
            }
        }

        public static Resources operator +(Resources p1, Resources p2) => 
            new Resources(p1.Wheat + p2.Wheat, p1.Wood + p2.Wood, p1.Gold + p2.Gold);

        public static Resources operator -(Resources p1, Resources p2)
        {
            if (EnoughResources(p1, p2))
                return new Resources(p1.Wheat - p2.Wheat, p1.Wood - p2.Wood, p1.Gold - p2.Gold);
            else
                throw new GameException("Not enough resources");
        }

        public static bool EnoughResources(Resources p1, Resources p2) => 
            p1.Wheat > p2.Wheat & p1.Wood > p2.Wood & p1.Gold > p2.Gold;
    }
}
