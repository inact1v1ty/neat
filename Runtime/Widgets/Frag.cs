using UnityEngine;
using UnityEngine.UI;

namespace Neat
{
    public struct Frag
    {
        public Node[] Children;
    }

    public class FragWidget : Widget<Frag>
    {
        public override Node Render(Frag props)
        {
            return null;
        }
    }
}
