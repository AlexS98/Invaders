using Invaders.GameModels.Exceptions;

namespace Invaders.GameModels.Additional
{
    public class Price
    {
        public int Wheat { get; set; }
        public int Wood { get; set; }
        public int Gold { get; set; }

        public Price(int wheat = 0, int wood = 0, int gold = 0)
        {
            Wheat = wheat;
            Wood = wood;
            Gold = gold;
        }

        public Price(int[] price)
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
                        return 0;
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

        public static Price operator +(Price p1, Price p2)
        {
            return new Price(p1.Wheat + p2.Wheat, p1.Wood + p2.Wood, p1.Gold + p2.Gold);
        }

        public static Price operator -(Price p1, Price p2)
        {
            if (EnoughResources(p1, p2))
                return new Price(p1.Wheat - p2.Wheat, p1.Wood - p2.Wood, p1.Gold - p2.Gold);
            else
                throw new GameException("Not enough resources");
        }

        public static bool EnoughResources(Price p1, Price p2) => 
            p1.Wheat > p2.Wheat & p1.Wood > p2.Wood & p1.Gold > p2.Gold;
    }
}
