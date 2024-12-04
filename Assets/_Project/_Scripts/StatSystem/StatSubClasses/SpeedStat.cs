namespace Game
{
    public class SpeedStat : Stat
    {
        public override StatType Type => StatType.Speed;

        public SpeedStat() : base(0) { }
        public SpeedStat(float baseValue) : base(baseValue) { }
    }
}