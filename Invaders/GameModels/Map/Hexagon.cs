using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Invaders.GameModels.Buildings;
using Invaders.GameModels.Wariors;

namespace Invaders.GameModels.Map
{
    internal sealed class Hexagon
    {
        public Point Center { get; }
        public HexType Type { get; }
        public Wariors.Warior Warior { get; set; }
        public Building Build { get; private set; }
        public List<Point> Additional { get; set; }
        public bool? Owner { get; set; }

        public Hexagon(Point center, HexType type, int number)
        {
            Center = center;
            Type = type;
            Warior = null;
            Build = null;
            Owner = null;
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

        public void AddWarior(Warior soldier) => Warior = soldier;

        public void AddBuilding(Building building) => Build = building;

        public bool IsNeighbor(Hexagon neighbor, int distan = 1) =>
            Math.Sqrt(Math.Pow(Center.X - neighbor.Center.X, 2) + Math.Pow(Center.Y - neighbor.Center.Y, 2)) < distan * 150;

        public bool NearBuilding(IEnumerable<Hexagon> map, int distance = 1)
        {
            return map.Any(item => IsNeighbor(item, distance) && item.Build != null);
        }
    }
}
