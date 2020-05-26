using System.Collections;
using System.Collections.Generic;

namespace Neat
{
    public static class DSL
    {
        public static Node R<Props, T>(Props props)
            where Props : IComponent<T>
        {
            if (props is IComponent<T>)
            {

            }

            return null;
        }

        public static Node R<Props>(Props props)
            where Props : IWidget
        {
            if (props is IWidget)
            {

            }

            return null;
        }
    }
}
