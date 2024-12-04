namespace Game
{
    public class DamageStat : Stat
    {
        public override StatType Type => StatType.Damage;

        public DamageStat() : base(0) { }
        public DamageStat(float baseValue) : base(baseValue) { }
    }
}