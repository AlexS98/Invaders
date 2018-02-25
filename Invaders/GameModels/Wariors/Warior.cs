using Invaders.GameModels.Additional;
using Invaders.GameModels.Map;
using Invaders.GameProcess;

namespace Invaders.GameModels.Wariors
{
    internal delegate void Protection(int hp);

    internal abstract class Warior
    {
        public GameResources Cost;
        public int Distance { protected set; get; }
        public int AttackDistance { protected set; get; }
        public bool Attacking { protected set; get; }
        public int HealthPoints { protected set; get; }
        protected int AttackRate { set; private get; }
        public Hexagon Place { private set; get; }
        public Player Owner { get; }
        protected Protection Protector;

        public void Damaging(Warior attacked)
        {
            if (attacked.Owner == Owner || Attacking) return;
            attacked.HealthPoints -= AttackRate;
            Attacking = true;
            if (attacked.HealthPoints > 0) return;
            attacked.Place.Warior = null;
            attacked.Owner.KillWarior(attacked);
        }
        public void Move(Hexagon newPlace)
        {
            if (newPlace.Warior != null || Distance == 0) return;
            Place.Warior = null;
            Place = newPlace;
            newPlace.Warior = this;
            Distance--;
        }

        public abstract void NewTurn();

        protected Warior(Hexagon place, Player owner)             
        {
            Place = place;
            Owner = owner;
            Attacking = false;
            Cost = new GameResources();
        }
    }
}
