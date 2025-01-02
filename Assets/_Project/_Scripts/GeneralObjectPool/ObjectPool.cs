using System.Collections.Generic;

namespace Game
{
    public class ObjectPool<T> where T : class, new()
    {
        private readonly Stack<T> pool = new Stack<T>();

        public T Get()
        {
            return pool.Count > 0 ? pool.Pop() : new T();
        }

        public void Release(T item)
        {
            pool.Push(item);
        }
    }
}