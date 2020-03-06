using System.Collections;
using System.Collections.Generic;

namespace Neat
{
    public static class DSL
    {
        public static Node<Props> r<Props>(Props props)
        {
            if (props is Frag)
            {
                // Well, that's bad code
                return (Node<Props>)(object)new FragNode()
                {
                    props = (Frag)(object)props
                };
            }
            return new Node<Props>()
            {
                props = props
            };
        }
    }
}
