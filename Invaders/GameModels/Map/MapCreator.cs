using Invaders.GameModels.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;

namespace Invaders.GameModels.Map
{
    enum MapSize
    {
        Small = 31,
        Normal = 38,
        Big = 50
    }
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
            for (int i = 0; i < columns; i++)
            {
                if (num > hexCount) throw new GameException("MapCreator error");
                if (pair)
                    for (int l = 0; l < biggerColumn - 1; l++)
                    {
                        map.Add(new Hexagon(new Point(80 + i * 105, 125 + 120 * l), HexTypeGenerator(), num));
                        num++;
                    }
                else
                    for (int l = 0; l < biggerColumn; l++)
                    {
                        map.Add(new Hexagon(new Point(80 + i * 105, 65 + 120 * l), HexTypeGenerator(), num));
                        num++;
                    }
                pair = !pair;
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
    }
}
