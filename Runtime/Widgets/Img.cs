using UnityEngine;
using UnityEngine.UI;

namespace Neat
{
    public struct Img
    {
        public Color? color;
    }

    public class ImgWidget : Component<Img>
    {
        private static readonly Img Default = new Img
        {
            color = Color.white
        };

        private Image image;

        public override void OnComponentDidMount(GameObject gameObject)
        {
            Debug.Log("Image OnComponentDidMount");
            image = gameObject.AddComponent<Image>();
        }

        public override void OnComponentWillUnmount()
        {
            Debug.Log("Image OnComponentWillUnmount");
            Object.Destroy(image);
            image = null;
        }

        public override void Update()
        {
            Debug.Log("Image Update");

            image.color = Props.color.GetValueOrDefault(Default.color.Value);
        }
    }
}
