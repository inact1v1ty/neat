using System;
using System.Collections.Generic;

namespace Neat
{
    public enum RegType
    {
        Widget,
        Component,
        Fragment,
        Element
    }

    public static class Registry
    {
        public static Dictionary<Type, (Type WidgetType, RegType RegType)> registry = new Dictionary<Type, (Type, RegType)>()
        {
            { typeof(Element), (typeof(ElementWidget), RegType.Element  ) },
            { typeof(Frag),    (null,                  RegType.Fragment ) },
            { typeof(Img),     (typeof(ImgWidget),     RegType.Component) }
        };
    }
}
