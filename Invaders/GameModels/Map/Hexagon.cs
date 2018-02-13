using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Invaders
{
    enum HexType
    {
        Field = 1,
        Forest = 2,
        Mountain = 3
    }

    internal sealed class Hexagon
    {
        public Point Center { get; set; }
        public HexType Type { get; set; }
        public int Number { get; set; }
        public Wariors Warior { get; set; }
        public Building Build { get; set; }
        public List<Point> Additional { get; set; }

        public Hexagon(Point center, HexType type, int number)
        {
            Center = center;
            Number = number;
            Type = type;
            Warior = null;
            Build = null;
            Additional = new List<Point>();
        }

        public PointCollection PointCollection()
        {
            return new PointCollection
            {
                new Point(-70 + Center.X, 0 + Center.Y),
                new Point(-35 + Center.X, 60 + Center.Y),
                new Point(35 + Center.X, 60 + Center.Y),
                new Point(70 + Center.X, 0 + Center.Y),
                new Point(35 + Center.X, -60 + Center.Y),
                new Point(-35 + Center.X, -60 + Center.Y)
            };
        }

        public bool MouseHit(Point hit) => 
            Math.Sqrt(Math.Pow(Center.X - hit.X, 2) + Math.Pow(Center.Y - hit.Y, 2)) < 60;

        public void AddWarior(Wariors soldier) => Warior = soldier;

        public void AddBuilding(Building building) => Build = building;

        public bool IsNeighbor(Hexagon neighbor, int distan = 1) =>
            Math.Sqrt(Math.Pow(Center.X - neighbor.Center.X, 2) + Math.Pow(Center.Y - neighbor.Center.Y, 2)) < distan * 150;
    }
}
