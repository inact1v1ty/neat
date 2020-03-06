using UnityEngine;
using UnityEngine.UI;
using static Neat.DSL;

namespace Neat
{
    public struct Img
    {
        public Color Color;
        public Node[] Children;
    }

    public class ImgWidget : NativeWidget<Img>
    {
        private Image image;

        public override void OnComponentDidMount(GameObject gameObject, Img props)
        {
            Debug.Log("Image OnComponentDidMount");
            image = gameObject.AddComponent<Image>();
        }

        public override void OnComponentWillUnmount(GameObject gameObject, Img props)
        {
            Debug.Log("Image OnComponentWillUnmount");
            image = null;
        }

        public override Node Render(Img props)
        {
            Debug.Log("Image Render");
            if (props.Children != null)
            {
                return r(new Frag() { Children = props.Children });
            }
            return null;
        }

        public override void Update(Img props)
        {
            Debug.Log("Image Update");
            image.color = props.Color;
        }
    }
}
