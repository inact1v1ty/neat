using System.Collections;
using System.Collections.Generic;

namespace Neat
{
    public abstract class Widget<Props> : IWidget<Props>
    {
        public abstract Node Render(Props props);

        public virtual void OnComponentDidMount(Props props) { }

        public virtual void OnComponentWillUnmount(Props props) { }
    }
}
