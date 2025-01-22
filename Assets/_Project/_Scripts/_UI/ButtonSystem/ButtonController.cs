using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    [RequireComponent(typeof(UnityEngine.UI.Button))]
    public abstract class ButtonController : MonoBehaviour
    {
        public Button button;
        
        protected virtual void Awake()
        {
            button = GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(OnClick);
            }
        }
        protected abstract void OnClick();

        protected virtual void OnDestroy()
        {
            var button = GetComponent<Button>();
            if (button != null)
            {
                button.onClick.RemoveListener(OnClick);
            }
        }
    }
}
