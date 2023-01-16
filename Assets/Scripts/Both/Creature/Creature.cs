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
    [RequireComponent(typeof(NetworkObject))]
    public abstract class Creature : MonoBehaviour
    {
        [SerializeField] protected string creatureName;
        [SerializeField] protected List<Stats> status;
        [SerializeField] protected Attackable.Attackable attackable;
        [SerializeField] protected bool isInit = false;

        public string Name => creatureName;
        public int SkillSlot => attackable.SkillSlot;
        public bool TouchDamage => attackable.TouchDamage;
        public bool IsInit => isInit;

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

        public List<SkillName> GetSkills()
        {
            return attackable.Skills.GroupBy(x => x.SkillName).Select(x => x.Key).ToList();
        }

        public bool ActivateSkill(SkillName skillName)
        {
            ISkill skill = attackable.Skills.FirstOrDefault((s) => s.SkillName == skillName);

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
}