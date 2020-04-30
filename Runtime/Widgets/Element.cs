using UnityEngine;
using UnityEngine.UI;

namespace Neat
{
    public struct Element
    {
        public Node[] Children;
        public Vector2? position;
        public Vector2? anchorMin;
        public Vector2? anchorMax;
        public Vector2? pivot;

        public static Element Full(params Node[] children)
        {
            return new Element()
            {
                Children = children,
                position = Vector2.zero,
                anchorMin = Vector2.zero,
                anchorMax = Vector2.one
            };
        }
    }

    public class ElementWidget : Component<Element>
    {
        private static readonly Element Default = new Element
        {
            Children = null,
            position = Vector2.zero,
            anchorMin = Helper.Center,
            anchorMax = Helper.Center,
            pivot = Helper.Center
        };

        RectTransform rectTransform;

        public override void OnComponentDidMount(GameObject gameObject)
        {
            rectTransform = gameObject.GetComponent<RectTransform>();
        }
        public override void OnComponentWillUnmount()
        {
            rectTransform = null;
        }

        public override void Update()
        {
            #region position
            if (PrevProps.position.HasValue != Props.position.HasValue ||
                Props.position.HasValue && PrevProps.position.Value != Props.position.Value)
            {
                if (Props.position.HasValue)
                {
                    rectTransform.position = Props.position.Value;
                }
                else
                {
                    rectTransform.position = Default.position.Value;
                }
            }
            #endregion

            #region anchorMin
            if (PrevProps.anchorMin.HasValue != Props.anchorMin.HasValue ||
                Props.anchorMin.HasValue && PrevProps.anchorMin.Value != Props.anchorMin.Value)
            {
                if (Props.anchorMin.HasValue)
                {
                    rectTransform.anchorMin = Props.anchorMin.Value;
                }
                else
                {
                    rectTransform.anchorMin = Default.anchorMin.Value;
                }
            }
            #endregion

            #region anchorMax
            if (PrevProps.anchorMax.HasValue != Props.anchorMax.HasValue ||
                Props.anchorMax.HasValue && PrevProps.anchorMax.Value != Props.anchorMax.Value)
            {
                if (Props.anchorMax.HasValue)
                {
                    rectTransform.anchorMax = Props.anchorMax.Value;
                }
                else
                {
                    rectTransform.anchorMax = Default.anchorMax.Value;
                }
            }
            #endregion

            #region pivot
            if (PrevProps.pivot.HasValue != Props.pivot.HasValue ||
                Props.pivot.HasValue && PrevProps.pivot.Value != Props.pivot.Value)
            {
                if (Props.pivot.HasValue)
                {
                    rectTransform.pivot = Props.pivot.Value;
                }
                else
                {
                    rectTransform.pivot = Default.pivot.Value;
                }
            }
            #endregion
        }
    }
}
