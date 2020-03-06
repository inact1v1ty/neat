using System;
using System.Collections.Generic;

namespace Neat
{
    public static class Registry
    {
        public static Dictionary<Type, (Type WidgetType, bool Native)> widgets = new Dictionary<Type, (Type, bool)>()
        {
            { typeof(Img), (typeof(ImgWidget), true) }
        };
    }
}
