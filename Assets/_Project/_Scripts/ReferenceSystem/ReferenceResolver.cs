
using Game;
using UnityEngine;
public static class ReferenceResolver
{
    private static ReferenceRegistry registry = new ReferenceRegistry();

    public static void Register(ReferenceKeys key, GameObject obj) => registry.Register(key, obj);
    public static void Unregister(ReferenceKeys key) => registry.Unregister(key);
    public static T Get<T>(ReferenceKeys key) where T : Component
    {
        GameObject obj = registry.Get(key);
        return obj != null ? obj.GetComponent<T>() : null;
    }
    public static void ClearDestroyed() => registry.ClearDestroyed();
}




