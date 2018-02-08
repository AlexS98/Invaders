using Invaders.GameModels.Additional;

namespace Invaders
{
    internal delegate void Protection(int hp);

    internal abstract class Wariors
    {
        public Resources Cost;
        public int Distance { set; get; }
        public int AttackDistance { protected set; get; }
        public bool Attacking { set; get; }
        public int HP { set; get; }
        public int AttackRate { protected set; get; }
        public Hexagon Place { set; get; }
        public Player Owner { set; get; }
        public Protection Protector;

        public void Damaging(Wariors attacked)
        {
            if (attacked.Owner != Owner && !Attacking)
            {
                attacked.HP -= AttackRate;
                Attacking = true;
                if (attacked.HP <= 0)
                {
                    attacked.Place.Warior = null;
                    attacked.Owner.KillWarior(attacked);
                }
            }
        }
        public void Move(Hexagon NewPlace)
        {
            if (NewPlace.Warior == null && Distance != 0)
            {
                Place.Warior = null;
                Place = NewPlace;
                NewPlace.Warior = this;
                Distance--;
            }
        }

        public abstract void Attack();

        public abstract void NewTurn();


        protected Wariors(Hexagon place, Player owner)             
        {
            Place = place;
            Owner = owner;
            Attacking = false;
            Cost = new Resources();
        }
    }
}
