using System;
using System.Windows;
using System.Windows.Media;

namespace Invaders
{
    enum HexType
    {

    }
    public class Hexagone
    {
        public Point Center;
        public int Type;
        public int Number;
        public Wariors Warior;
        public Building Build;

        public Hexagone(Point center, int type, int number)
        {
            Center = center;
            Number = number;
            Type = type;
            Warior = null;
            Build = null;
        }

        public PointCollection PointCollection()
        {
            PointCollection hexagone = new PointCollection
            {
                new Point(-70 + Center.X, 0 + Center.Y),
                new Point(-35 + Center.X, 60 + Center.Y),
                new Point(35 + Center.X, 60 + Center.Y),
                new Point(70 + Center.X, 0 + Center.Y),
                new Point(35 + Center.X, -60 + Center.Y),
                new Point(-35 + Center.X, -60 + Center.Y)
            };
            return hexagone;
        }

        public bool MouseHit(Point hit) => 
            Math.Sqrt(Math.Pow(Center.X - hit.X, 2) + Math.Pow(Center.Y - hit.Y, 2)) < 60;

        public void AddWarior(Wariors soldier) => Warior = soldier;

        public void AddBuilding(Building building) => Build = building;

        public bool IsNeighbor(Hexagone neighbor, int distan = 1) =>
            Math.Sqrt(Math.Pow(Center.X - neighbor.Center.X, 2) + Math.Pow(Center.Y - neighbor.Center.Y, 2)) < distan * 150;
    }
}
