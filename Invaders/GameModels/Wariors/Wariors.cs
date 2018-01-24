using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colonizators
{
    abstract class Wariors
    {
        Hexagone place;
        int attack;
        int hp;
        Player owner;
        int distance;
        bool at;
        int adist;
        public int[] Cost;

        public int Distance
        {
            set { distance = value; }
            get { return distance; }
        }
        public int AttackDistance
        {
            set { adist = value; }
            get { return adist; }
        }
        public bool Attacking
        {
            set { at = value; }
            get { return at; }
        }
        public int HP
        {
            set { hp = value; }
            get { return hp; }
        }
        public int AttackRate
        {
            set { attack = value; }
            get { return attack; }
        }
        public Hexagone Place
        {
            set { place = value; }
            get { return place; }
        }
        public Player Owner
        {
            set { owner = value; }
            get { return owner; }
        }

        
        public void Damaging(Wariors attacked)
        {
            if (attacked.Owner != this.Owner && !Attacking)
            {
                attacked.HP -= this.AttackRate;
                Attacking = true;
                if (attacked.HP <= 0)
                {
                    attacked.Place.Warior = null;
                    attacked.Owner.KillWarior(attacked);
                }
            }
        }
        public void Move(Hexagone NewPlace)
        {
            if (NewPlace.Warior == null && this.distance != 0)
            {
                Place.Warior = null;
                this.Place = NewPlace;
                NewPlace.Warior = this;
                distance--;
            }
        }

        public void Attack()
        {
 
        }
        public abstract void NewTurn();


        protected Wariors(Hexagone place, Player owner)             
        {
            Place = place;
            Owner = owner;
            Attacking = false;            
            int[] Cost = new int[3];
            for (int i = 0; i < 3; i++)
            {
                Cost[i] = 0;
            }
        }
    }
}
