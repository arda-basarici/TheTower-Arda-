using UnityEngine;

namespace Game
{
    public class SaveSystemInitializer : IInitializable
    {
        public InitializationPhase Phase => InitializationPhase.Global;
        public void Initialize()
        {
            SaveManager.Initialize(SaveHandlerFactory.CreateSaveHandler());
            Debug.Log("Save system initialized.");
        }
    }
}