using Game;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneData", menuName = "Scene Manager/Scene Data")]
public class SceneData : ScriptableObject
{
    public SceneName sceneName;      
    public GameStateType targetState;
    //public SceneInitializer initializerPrefab;
}
