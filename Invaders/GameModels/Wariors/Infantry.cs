using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colonizators
{
    class Infantry : Wariors
    {
           protected Infantry(Hexagone place, Player owner)
            : base (place, owner)
        {

        }

           public void Protector()
           {
               if (this.Place.Build != null && this.HP < 5)
               {
                   this.HP += 1;
               }
           }
           public override void NewTurn() 
           {
               this.Distance = 1;
               this.Protector();
           }  
    }
}
