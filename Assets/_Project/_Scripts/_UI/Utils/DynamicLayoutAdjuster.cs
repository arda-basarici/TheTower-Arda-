using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(LayoutGroup))]
    [ExecuteAlways]
    public class DynamicLayoutManager : MonoBehaviour
    {
        [Header("Base Settings")]
        public float referenceAspectRatio = 16f / 9f;

        [Header("Padding Settings")]
        public bool adjustPadding = true;
        public float minPadding = 10f;
        public float maxPadding = 50f;

        [Header("Spacing Settings")]
        public bool adjustSpacing = true;
        public float minSpacing = 5f;
        public float maxSpacing = 30f;

        [Header("Scaling Settings")]
        public bool adjustScaling = true;
        public float minScale = 0.5f;
        public float maxScale = 1f;

        private LayoutGroup layoutGroup;
        private RectTransform parentRect;

        private float previousAspectRatio = 0;

        private void Awake()
        {
            layoutGroup = GetComponent<LayoutGroup>();
            parentRect = GetComponent<RectTransform>();
        }

        private void Start()
        {
            AdjustLayout();
        }

        private void Update()
        {
            AdjustLayout();
        }

        private void AdjustLayout()
        {
            float screenAspectRatio = (float)Screen.width / Screen.height;

            if (screenAspectRatio == previousAspectRatio)
            {
                return;
            }
            else
            {
                previousAspectRatio = screenAspectRatio;
            }

            float aspectRatioFactor = screenAspectRatio / referenceAspectRatio;

            // Calculate dynamic values
            float dynamicPadding = Mathf.Lerp(minPadding, maxPadding, aspectRatioFactor);
            float dynamicSpacing = Mathf.Lerp(minSpacing, maxSpacing, aspectRatioFactor);
            float dynamicScale = Mathf.Lerp(minScale, maxScale, aspectRatioFactor);

            // Apply adjustments
            AdjustPaddingAndSpacing(dynamicPadding, dynamicSpacing);

            if (adjustScaling)
            {
                ScaleChildElements(dynamicScale);
            }
        }

        private void AdjustPaddingAndSpacing(float dynamicPadding, float dynamicSpacing)
        {
            if (layoutGroup is HorizontalLayoutGroup horizontalLayoutGroup)
            {
                AdjustHorizontalLayout(horizontalLayoutGroup, dynamicPadding, dynamicSpacing);
            }
            else if (layoutGroup is VerticalLayoutGroup verticalLayoutGroup)
            {
                AdjustVerticalLayout(verticalLayoutGroup, dynamicPadding, dynamicSpacing);
            }
            else if (layoutGroup is GridLayoutGroup gridLayoutGroup)
            {
                AdjustGridLayout(gridLayoutGroup, dynamicPadding, dynamicSpacing);
            }
        }

        private void AdjustHorizontalLayout(HorizontalLayoutGroup horizontalLayoutGroup, float dynamicPadding, float dynamicSpacing)
        {
            if (adjustPadding)
            {
                horizontalLayoutGroup.padding.left = Mathf.RoundToInt(dynamicPadding);
                horizontalLayoutGroup.padding.right = Mathf.RoundToInt(dynamicPadding);
            }
            if (adjustSpacing)
            {
                horizontalLayoutGroup.spacing = dynamicSpacing;
            }
        }

        private void AdjustVerticalLayout(VerticalLayoutGroup verticalLayoutGroup, float dynamicPadding, float dynamicSpacing)
        {
            if (adjustPadding)
            {
                verticalLayoutGroup.padding.top = Mathf.RoundToInt(dynamicPadding);
                verticalLayoutGroup.padding.bottom = Mathf.RoundToInt(dynamicPadding);
            }
            if (adjustSpacing)
            {
                verticalLayoutGroup.spacing = dynamicSpacing;
            }
        }

        private void AdjustGridLayout(GridLayoutGroup gridLayoutGroup, float dynamicPadding, float dynamicSpacing)
        {
            if (adjustPadding)
            {
                gridLayoutGroup.padding.left = Mathf.RoundToInt(dynamicPadding);
                gridLayoutGroup.padding.right = Mathf.RoundToInt(dynamicPadding);
            }
            if (adjustSpacing)
            {
                gridLayoutGroup.spacing = new Vector2(dynamicSpacing, dynamicSpacing);
            }
        }

        private void ScaleChildElements(float dynamicScale)
        {
            foreach (RectTransform child in parentRect)
            {
                child.localScale = Vector3.one * dynamicScale;
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            previousAspectRatio = 0;
        }
#endif
    }

}