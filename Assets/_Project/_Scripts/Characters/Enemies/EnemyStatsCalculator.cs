using System;
using System.Threading;

namespace Game
{
    public static class EnemyStatsCalculator
    {
        private const double HealthExponent = 2.13;
        private const double HealthLinearFactor = 0.8;
        private const double HealthExponentialFactor = 0.05;

        private const double DamageExponent = 2.007;
        private const double DamageLinearFactor = 0.8;
        private const double DamageExponentialFactor = 0.021;

        public static double CalculateHealth(int wave, int tier)
        {
            double tierMultiplier = GetTierMultiplier(tier);
            double waveExponential = Math.Pow(wave, HealthExponent);
            double linearComponent = HealthLinearFactor * wave;
            double baseHealth = (HealthExponentialFactor * waveExponential) + linearComponent + 1.5;
            return baseHealth * tierMultiplier;
        }

        public static double CalculateDamage(int wave, int tier)
        {
            double tierMultiplier = GetTierMultiplier(tier);
            double waveExponential = Math.Pow(wave, DamageExponent);
            double linearComponent = DamageLinearFactor * wave;
            double baseDamage = (DamageExponentialFactor * waveExponential) + linearComponent + 1.07;

            return baseDamage * tierMultiplier;
        }

        private static double GetTierMultiplier(int tier)
        {
            if (tier <= 1) return 1.0;

            double r = (1 + 15.5 * (tier - 1)) * (Math.Pow(1.43, tier - 2) + 0.2 * (tier - 1));

            if (tier >= 5) r *= 1.05;
            if (tier >= 6) r *= 1.11;
            if (tier >= 7) r *= 1.2;
            if (tier >= 8) r *= 1.38;
            if (tier >= 9) r *= 1.75;
            if (tier >= 10) r *= 4.3;
            if (tier >= 11) r *= 41;
            if (tier >= 12) r *= 410;
            if (tier >= 13) r *= 9000;
            if (tier >= 14) r *= 5000;
            if (tier >= 15) r *= 1000;

            return r;
        }


    }
}