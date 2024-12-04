using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public static class EnemyManager
    {
        private static readonly List<Enemy> enemies = new List<Enemy>();

        public static void AddEnemy(Enemy enemy)
        {
            if (enemy != null && !enemies.Contains(enemy))
            {
                enemies.Add(enemy);
            }
        }

        public static void RemoveEnemy(Enemy enemy)
        {
            if (enemy != null && enemies.Contains(enemy))
            {
                enemies.Remove(enemy);
            }
        }

        public static List<Enemy> GetAllEnemies()
        {
            return new List<Enemy>(enemies);
        }

        public static Enemy GetClosestEnemy(Vector3 position)
        {
            Enemy closestEnemy = null;
            float closestDistance = float.MaxValue;

            foreach (Enemy enemy in enemies)
            {
                if (enemy == null) continue;

                float distance = Vector3.Distance(position, enemy.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemy;
                }
            }

            return closestEnemy;
        }       

        public static void Clear()
        {
            enemies.Clear();
        }
    }
}
