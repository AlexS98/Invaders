using Invaders.GameModels.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;

namespace Invaders.GameModels.Map
{
    internal sealed class MapCreator
    {
        public void CreateMap(ref IList<Hexagon> map, MapSize mapSize, Point canvasSize)
        {
            int hexCount = (int)mapSize;
            int biggerColumn = (hexCount < 35) ? 5 : 6;
            double c = (hexCount - biggerColumn + 1) / (double)(biggerColumn * 2 - 1);
            bool pair = (c - (int)c) == 0;
            int num = 0;
            c = (hexCount - (pair ? (biggerColumn - 1) : biggerColumn)) / (biggerColumn * 2 - 1);
            int columns = 2 * (int)c + 1;
            int xIndent = (int)(canvasSize.X - columns * 105) / 2;
            int yIndent = (int)(canvasSize.Y - biggerColumn * 120) / 2;
            for (int i = 0; i < columns; i++)
            {
                if (num > hexCount) throw new GameException("MapCreator error");
                if (pair)
                    for (int l = 0; l < biggerColumn - 1; l++)
                    {
                        map.Add(new Hexagon(new Point(xIndent + 53 + i * 105, yIndent + 120 + 120 * l), HexTypeGenerator(), num));
                        num++;
                    }
                else
                    for (int l = 0; l < biggerColumn; l++)
                    {
                        map.Add(new Hexagon(new Point(xIndent + 53 + i * 105, yIndent + 60 + 120 * l), HexTypeGenerator(), num));
                        num++;
                    }
                pair = !pair;
            }
            for(int i = 0; i < map.Count; i++)
            {
                if (map[i].Type == HexType.Forest)
                {
                    double x = map[i].Center.X;
                    double y = map[i].Center.Y;
                    map[i].Additional = new List<Point>
                    {
                        new Point(x-35, y -60),
                        new Point(x+20,y-50),
                        new Point(x-5, y -55),
                        new Point(x-45,y-35),
                        new Point(x-15, y -25),
                        new Point(x+15,y-20),
                        new Point(x-60, y -10),
                        new Point(x+40,y-10),
                        new Point(x+25, y +20),
                        new Point(x-45,y+15),
                        new Point(x-15, y+5),
                        new Point(x+5, y+5),
                    };
                }
            }
        }

        private HexType HexTypeGenerator(int whFreq = 55, int wFreq = 25, int gFreq = 20)
        {
            Random random = new Random();
            Thread.Sleep(7);
            int r = random.Next(0, whFreq + wFreq + gFreq);
            if (r < whFreq) return (HexType)1;
            else if (r < (wFreq + whFreq)) return (HexType)2;
            else return (HexType)3;
        }


        //private void GenerateAdditional(Hexagon item, int i, int count)
        //{
        //    double x, y;
        //    for (int k = 0; k < count; k++)
        //    {
        //        Random random = new Random(i + k + DateTime.Now.Millisecond);
        //        do {
        //            Thread.Sleep(1);
        //            x = item.Center.X - 55 + 20 * random.Next(0, 4) + random.Next(0, 8);
        //            y = item.Center.Y - 55 + 20 * random.Next(0, 4) + random.Next(0, 8);
        //        }
        //        while (!CheckAdditional(item, new Point(x, y)));
        //        item.Additional.Add(new Point(x, y));
        //    }
        //}

        //private bool CheckAdditional(Hexagon item, Point point)
        //{
        //    foreach (var i in item.Additional)
        //    {
        //        if (point.X >= i.X && point.X <= i.X + 20 && point.Y >= i.Y && point.Y <= i.Y + 20) return false;
        //        if (point.X + 20 >= i.X && point.X + 20 <= i.X + 20 && point.Y >= i.Y && point.Y <= i.Y + 20) return false;
        //        if (point.X >= i.X && point.X <= i.X + 20 && point.Y + 20 >= i.Y && point.Y + 20 <= i.Y + 20) return false;
        //        if (point.X + 20 >= i.X && point.X + 20 <= i.X + 20 && point.Y + 20 >= i.Y && point.Y + 20 <= i.Y + 20) return false;
        //    }
        //    return true;
        //} 
    }
}
