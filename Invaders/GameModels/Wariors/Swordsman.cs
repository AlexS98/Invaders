using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colonizators
{
    class Swordsman : Infantry 
    {
        public Swordsman(Hexagone place, Player owner)
            : base (place, owner)
        {
            this.Distance = 1;
            this.HP = 4;
            this.AttackRate = 2;
            this.AttackDistance = 1;
            this.Cost = new int[3];
            this.Cost[0] = 10;
            this.Cost[1] = 10;
            this.Cost[2] = 0;
        }

        public override void NewTurn()
        {
            //this.Distance = 1;
            base.NewTurn();
            this.Attacking = false;
        }
    }
}
