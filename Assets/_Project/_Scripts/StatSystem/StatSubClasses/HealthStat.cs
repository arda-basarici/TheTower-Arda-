namespace Game
{
    public class HealthStat : Stat
    {
        public override StatType Type => StatType.Health;

        public HealthStat() : base(0) { }
        public HealthStat(float baseValue) : base(baseValue) { }
    }
}