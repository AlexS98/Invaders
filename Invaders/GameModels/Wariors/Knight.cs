using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colonizators
{
    class Knight : Wariors
    {
        public Knight(Hexagone place, Player owner)
            : base (place, owner)
        {
            this.Distance = 2;
            this.HP = 6;
            this.AttackRate = 4;
            this.AttackDistance = 1;
            this.Cost = new int[3];
            this.Cost[0] = 15;
            this.Cost[1] = 20;
            this.Cost[2] = 0;
        }

        public override void NewTurn()
        {
            this.Distance = 2;
            this.Attacking = false;
        }
    }
}
