using Invaders.GameModels.Exceptions;

namespace Invaders.GameModels.Additional
{
    internal sealed class GameResources
    {
        private int _wheat;
        private int _wood;
        private int _gold;

        public int Wheat
        {
            get
            {
                return _wheat;
            }
            set
            {
                if (value < 0)
                {
                    throw new GameException("Invalid resource value");
                }
                else
                {
                    _wheat = value;
                }
            }
        }
        public int Wood
        {
            get
            {
                return _wood;
            }
            set
            {
                if (value < 0)
                {
                    throw new GameException("Invalid resource value");
                }
                else
                {
                    _wood = value;
                }
            }
        }
        public int Gold
        {
            get
            {
                return _gold;
            }
            set
            {
                if (value < 0)
                {
                    throw new GameException("Invalid resource value");
                }
                else
                {
                    _gold = value;
                }
            }
        }

        public GameResources(int wheat = 0, int wood = 0, int gold = 0)
        {
            Wheat = wheat;
            Wood = wood;
            Gold = gold;
        }

        public GameResources(int[] price)
        {
            Wheat = price[0];
            Wood = price[1];
            Gold = price[2];
        }

        public int this[int n]
        {
            get
            {
                switch (n)
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
            set
            {
                if (value < 0) throw new GameException("Invalid resource value");
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

        public static GameResources operator +(GameResources p1, GameResources p2) =>
            new GameResources(p1.Wheat + p2.Wheat, p1.Wood + p2.Wood, p1.Gold + p2.Gold);

        public static GameResources operator -(GameResources p1, GameResources p2)
        {
            if (EnoughResources(p1, p2))
                return new GameResources(p1.Wheat - p2.Wheat, p1.Wood - p2.Wood, p1.Gold - p2.Gold);
            else
                throw new GameException("Not enough resources");
        }

        public static bool EnoughResources(GameResources p1, GameResources p2) =>
            p1.Wheat >= p2.Wheat && p1.Wood >= p2.Wood && p1.Gold >= p2.Gold;

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (GetType() != obj.GetType()) return false;
            if (ReferenceEquals(this, obj)) return true;
            GameResources r = (GameResources)obj;
            for (int i = 0; i < 3; i++)
            {
                if (this[i].Equals(r[i])) return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            var hashCode = -1947974804;
            hashCode = hashCode * -1521134295 + Wheat.GetHashCode();
            hashCode = hashCode * -1521134295 + Wood.GetHashCode();
            hashCode = hashCode * -1521134295 + Gold.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return $"wh:{this[0]};w:{this[1]};g:{this[2]}";
        }

        public GameResources Copy => new GameResources(Wheat, Wood, Gold);
    }
}
