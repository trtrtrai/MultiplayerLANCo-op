using Assets.Scripts.Both.Creature.Attackable;
using Assets.Scripts.Both.Creature.Status;
using Assets.Scripts.Both.Scriptable;
using Assets.Scripts.Server.Contruction.Builders;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CreatureDirector : MonoBehaviour
{
    public static CreatureDirector Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private CreatureBuilder builder;
    private readonly string path = "AssetObjects/Creatures/";

    public CreatureBuilder Builder { set => builder = value; }

    public void CharacterBuild(CharacterClass charClass)
    {
        //Game Object
        builder.InstantiateGameObject();

        //Load scriptable object
        var script = Resources.Load<CharacterModel>(path + "Players/" + charClass.ToString());

        //Init property
        builder.GiveName(script.CreatureName);

        var status = new List<Stats>();
        script.Status.ForEach(i => {
            var statsT = Type.GetType(i.Type.ToString());
            if (statsT is null) return;

            status.Add((Stats)Activator.CreateInstance(statsT, i.Amount));
        });
        builder.GiveStatus(status);

        var attackable = new Attackable();
        attackable.TouchDamage = script.TouchDamage;
        attackable.SkillSlot = script.SkillSlot;
        var skills = new List<Skill>()
        {
            new Skill(SkillName.Slash),
            new Skill(SkillName.Shield),
            new Skill(SkillName.FireBall),
        };
        attackable.Skills = skills;
        builder.GiveAttackable(attackable);
    }
}
