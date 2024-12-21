using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "SceneRegistry", menuName = "Scene Manager/Scene Registry")]
    public class SceneRegistry : ScriptableObject
    {
        public List<SceneData> scenes = new List<SceneData>();

        public SceneData GetSceneData(SceneName sceneName)
        {
            return scenes.Find(scene => scene.sceneName == sceneName);
        }
    }
}