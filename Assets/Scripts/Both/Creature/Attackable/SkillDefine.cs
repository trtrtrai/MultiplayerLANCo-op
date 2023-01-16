namespace Assets.Scripts.Both.Creature.Attackable
{
    public enum SkillName
    {
        FireBall,
        Ragnarok,
        BatSummon,
        Slash,
        Shield,
    }

    public enum TagType
    {
        Attack,
        Effect, //buff, debuff for EffectStatsCalc
        Special, //summon, teleport, immortal
    }

    public enum AttackTag
    {
        Normal, //in this creature: slash, shield, spear...
        Bullet,
        RangeOnThem,
        RangeTarget, //optional
    }

    public enum EffectTag
    {
        Add,
        Multiple,
        Substract,
        Divide,
    }

    public enum SpecialTag
    {
        Summon,
        Teleport,
        Immortal,
    }
}