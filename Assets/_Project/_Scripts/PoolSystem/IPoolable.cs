using UnityEngine;

namespace Game
{
    public interface IPoolable
    {

        /// <summary>
        /// Called before the object is reused from the pool.
        /// </summary>
        void OnSpawn() { }

        /// <summary>
        /// Called when the object is spawned from the pool.
        /// </summary>
        void OnReturn() { }


        /// <summary>
        /// Called when the object is returned to the pool.
        /// </summary>
        void ResetForPooling() { }
    }
}