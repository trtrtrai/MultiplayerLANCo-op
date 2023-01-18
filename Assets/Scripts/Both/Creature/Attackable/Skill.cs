using System;
using Unity.Netcode;
using UnityEngine;

namespace Assets.Scripts.Both.Creature.Attackable
{
    [Serializable]
    public class Skill : ISkill, ISkillActive
    {
        [SerializeField] private SkillName skillName;

        public SkillName SkillName => skillName;
        public string Description { get; set; }

        public Skill(SkillName skillName)
        {
            this.skillName = skillName;
            Description = "";
        }

        public Skill(SkillName skillName, string description)
        {
            this.skillName = skillName;
            Description = description;
        }

        public void Activate(Creature owner)
        {
            GameController.Instance.CastSpell(SkillName, owner.GetComponent<NetworkObject>());
        }
    }

    public interface ISkill
    {
        SkillName SkillName { get; }
        string Description { get; }   
    }

    public interface ISkillActive
    {
        void Activate(Creature owner);
    }
}