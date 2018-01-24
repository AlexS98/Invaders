using System;
using System.Windows;

namespace Colonizators
{
    class Hexagone
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
        public bool MouseHit(Point hit)
        {
            bool here = false;
            if (Math.Sqrt(Math.Pow(Center.X - hit.X, 2) + Math.Pow(Center.Y - hit.Y, 2)) < 60) here = true;
            return here;
        }
        public void AddWarior(Wariors soldier)
        {
            Warior = soldier;
        }

        public void AddBuilding(Building building)
        {
            Build = building;
        }

        public bool IsNeighbor(Hexagone neighbor, int distan = 1)
        {
            bool Is = false;
            int limit = distan * 150;
            if (Math.Sqrt(Math.Pow(this.Center.X - neighbor.Center.X, 2) + Math.Pow(this.Center.Y - neighbor.Center.Y, 2)) < limit) Is = true;
            return Is;
        }
    }
}
