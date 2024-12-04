namespace Game
{
    public class FireRateStat : Stat
    {
        public override StatType Type => StatType.FireRate;

        public FireRateStat() : base(0) { }
        public FireRateStat(float baseValue) : base(baseValue) { }
    }
}