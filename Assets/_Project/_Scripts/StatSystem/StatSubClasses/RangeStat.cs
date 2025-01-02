namespace Game
{
    public class RangeStat : Stat
    {
        public override StatType Type => StatType.Range;

        public RangeStat() : base(0, "") { }
        public RangeStat(float baseValue, string id) : base(baseValue, id) { }
    }
}