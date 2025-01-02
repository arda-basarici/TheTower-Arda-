using System;

namespace Game
{
    public struct StatState : IState
    {
        public float CurrentValue { get; set; }
        public StatType StatType { get; set; }
    }
}