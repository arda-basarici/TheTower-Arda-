using System.Collections;
using UnityEngine;

namespace Game
{
    public class PoolCleaner : MonoBehaviour
    {
        private float cleaningInterval;

        /// <summary>
        /// Initializes the PoolCleaner with the specified parameters.
        /// </summary>
        /// <param name="inactivityThreshold">Time in seconds before a pool is considered inactive.</param>
        public void Initialize(float cleaningInterval)
        {
            this.cleaningInterval = cleaningInterval;
            StartCoroutine(CleanupRoutine());
        }

        private IEnumerator CleanupRoutine()
        {
            while (true)
            {
                PoolManager.CleanUpPools();
                yield return new WaitForSeconds(cleaningInterval);
            }
        }
    }
}
