using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Neat
{
    public class NativeWidget<Props> : IWidget<Props>
    {
        public virtual Node Render(Props props) { return null; }

        public virtual void Update(Props props) { }

        public virtual void OnComponentDidMount(GameObject gameObject, Props props) { }

        public virtual void OnComponentWillUnmount(GameObject gameObject, Props props) { }
    }
}
