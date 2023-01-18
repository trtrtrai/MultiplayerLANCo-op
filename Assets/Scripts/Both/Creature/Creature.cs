using Assets.Scripts.Both.Creature.Attackable;
using Assets.Scripts.Both.Creature.Status;
using Assets.Scripts.Both.Scriptable;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

namespace Assets.Scripts.Both.Creature
{
    [Serializable]
    public abstract class Creature : NetworkBehaviour, ICreatureBuild, ICreature
    {
        [SerializeField] protected string creatureName;
        [SerializeField] protected List<Stats> status;
        [SerializeField] protected Attackable.Attackable attackable;
        [SerializeField] protected CreatureForm form;

        public string Name => creatureName;
        public int SkillSlot => attackable.SkillSlot;
        public bool TouchDamage => attackable.TouchDamage;
        public CreatureForm Form => form;

        public virtual IStats GetStats(StatsType type)
        {
            var statsType = Type.GetType(type.ToString());
            if (statsType is null) return null;

            for (int i = 0; i < status.Count; i++)
            {
                if (status[i].GetType().Name == statsType.ToString())
                {
                    return status[i];
                }
            }

            return null;
        }

        public List<ISkill> GetSkills()
        {
            return attackable.Skills.Select(s => (ISkill)s).ToList();
        }

        public bool ActivateSkill(SkillName skillName)
        {
            ISkillActive skill = attackable.Skills.FirstOrDefault((s) => s.SkillName == skillName);

            if (skill is null)
            {
                Debug.Log(creatureName + " does not have " + skillName);
                return false;
            }

            skill.Activate(this); //this finc will return bool after
            return true;
        }

        public virtual void InitName(string name)
        {
            creatureName = name;
            this.name = name; // GameObject name
        }

        public virtual void InitStatus(List<Stats> status)
        {
            this.status = status;
        }

        public virtual void InitAttack(Attackable.Attackable attackable)
        {
            this.attackable = attackable;
        }
    }

    public enum CreatureForm
    {
        Character,
        Boss,
        Enemy,
        Mobs,
        Ally
    }

    public interface ICreatureBuild
    {
        void InitName(string name);
        void InitStatus(List<Stats> status);
        void InitAttack(Attackable.Attackable attackable);
    }

    public interface ICreature
    {
        public string Name { get; }
        public int SkillSlot { get; }
        public bool TouchDamage { get; }
        public CreatureForm Form { get; }

        IStats GetStats(StatsType type);
        public List<ISkill> GetSkills();
        public bool ActivateSkill(SkillName skillName);
    }
}