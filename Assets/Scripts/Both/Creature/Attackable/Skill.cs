using System;
using Unity.Netcode;
using UnityEngine;

namespace Assets.Scripts.Both.Creature.Attackable
{
    [Serializable]
    public class Skill : ISkill
    {
        [SerializeField] private SkillName skillName;
        [SerializeField] private string description;

        public SkillName SkillName => skillName;
        public string Description => description;

        public void Activate(Creature owner)
        {
            GameController.Instance.CastSpell(SkillName, owner.GetComponent<NetworkObject>());
        }
    }

    public interface ISkill
    {
        SkillName SkillName { get; }
        string Description { get; }
        void Activate(Creature owner);
    }
}