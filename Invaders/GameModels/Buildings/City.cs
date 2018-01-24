using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colonizators
{
    class City : Building
    {
        public City(Hexagone hex, Player owner)
            : base(hex, owner)
        {

        }
    }
}
