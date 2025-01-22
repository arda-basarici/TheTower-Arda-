using UnityEngine;

namespace Game
{
       public class BattleButton : ButtonController
    {
        protected override async void OnClick()
        {
           await SceneManager.LoadSceneAsync(SceneName.Game);
        }
    }
}
