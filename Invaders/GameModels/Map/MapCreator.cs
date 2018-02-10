using System.Collections.Generic;
using System.Windows;

namespace Invaders.GameModels.Map
{
    enum MapSize
    {
        Small,
        Normal,
        Big
    }
    internal sealed class MapCreator
    {
        public static IList<Hexagon> GetMap()
        {
            return new List<Hexagon>
            {
                new Hexagon(new Point(420, 125), HexType.Field, 1),
                new Hexagon(new Point(420, 245), HexType.Forest, 1),
                new Hexagon(new Point(420, 365), HexType.Forest, 1),
                new Hexagon(new Point(420, 485), HexType.Field, 1),
                new Hexagon(new Point(420, 605), HexType.Field, 1),

                new Hexagon(new Point(525, 185), HexType.Field, 1),
                new Hexagon(new Point(525, 305), HexType.Field, 1),
                new Hexagon(new Point(525, 425), HexType.Mountain, 1),
                new Hexagon(new Point(525, 545), HexType.Field, 1),

                new Hexagon(new Point(315, 185), HexType.Field, 1),
                new Hexagon(new Point(315, 305), HexType.Field, 1),
                new Hexagon(new Point(315, 425), HexType.Field, 1),
                new Hexagon(new Point(315, 545), HexType.Field, 1),

                new Hexagon(new Point(630, 125), HexType.Field, 1),
                new Hexagon(new Point(630, 245), HexType.Mountain, 1),
                new Hexagon(new Point(630, 365), HexType.Field, 1),
                new Hexagon(new Point(630, 485), HexType.Mountain, 1),
                new Hexagon(new Point(630, 605), HexType.Field, 1),

                new Hexagon(new Point(210, 125), HexType.Field, 1),
                new Hexagon(new Point(210, 245), HexType.Mountain, 1),
                new Hexagon(new Point(210, 365), HexType.Field, 1),
                new Hexagon(new Point(210, 485), HexType.Field, 1),
                new Hexagon(new Point(210, 605), HexType.Forest, 1),

                new Hexagon(new Point(105, 185), HexType.Field, 1),
                new Hexagon(new Point(105, 305), HexType.Field, 1),
                new Hexagon(new Point(105, 425), HexType.Forest, 1),
                new Hexagon(new Point(105, 545), HexType.Mountain, 1),

                new Hexagon(new Point(735, 185), HexType.Forest, 1),
                new Hexagon(new Point(735, 305), HexType.Field, 1),
                new Hexagon(new Point(735, 425), HexType.Field, 1),
                new Hexagon(new Point(735, 545), HexType.Field, 1)
            };
        }

        public static IList<Hexagon> CreateMap(MapSize mapSize, Point canvasSize)
        {
            return new List<Hexagon>(); 
        }
    }
}
