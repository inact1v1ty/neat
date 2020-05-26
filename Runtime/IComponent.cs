using System.Collections;
using System.Collections.Generic;

namespace Neat
{
    public interface IComponent<T>
    {
        void Update(T component);
    }
}
