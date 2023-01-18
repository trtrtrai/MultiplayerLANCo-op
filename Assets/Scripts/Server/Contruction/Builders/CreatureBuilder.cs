using Assets.Scripts.Both.Creature.Attackable;
using Assets.Scripts.Both.Creature;
using System.Collections.Generic;
using Assets.Scripts.Both.Creature.Status;

namespace Assets.Scripts.Server.Contruction.Builders
{
    public abstract class CreatureBuilder
    {
        protected ICreatureBuild creature = null;

        public abstract void InstantiateGameObject();

        public virtual void GiveName(string name)
        {
            creature.InitName(name);
        }

        public virtual void GiveStatus(List<Stats> status)
        {
            creature.InitStatus(status);
        }

        public virtual void GiveAttackable(Attackable attackable)
        {
            creature.InitAttack(attackable);
        }

        public virtual ICreature Release()
        {
            var rs = creature;

            creature = null;

            return (ICreature)rs;
        }
    }
}