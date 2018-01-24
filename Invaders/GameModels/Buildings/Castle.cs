using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colonizators
{
    class Castle : Building
    {
        public Castle(Hexagone hex, Player owner)
            : base(hex, owner)
        {
            price = new int[3];
            price[0] = 0;
            price[1] = 20;
            price[2] = 50;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  
        }

        public void SetCollection(int wheat, int gold, int wood)
        {
            BringResourses = new int[3];
            this.BringResourses[0] = wheat;
            this.BringResourses[1] = gold;
            this.BringResourses[2] = wood;
        }
    }
}
