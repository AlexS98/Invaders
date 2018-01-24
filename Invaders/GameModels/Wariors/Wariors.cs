namespace Invaders
{
    public abstract class Wariors
    {
        public int[] Cost;

        public int Distance { set; get; }
        public int AttackDistance { set; get; }
        public bool Attacking { set; get; }
        public int HP { set; get; }
        public int AttackRate { set; get; }
        public Hexagone Place { set; get; }
        public Player Owner { set; get; }


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
            if (NewPlace.Warior == null && this.Distance != 0)
            {
                Place.Warior = null;
                this.Place = NewPlace;
                NewPlace.Warior = this;
                Distance--;
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
