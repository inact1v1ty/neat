using System.Collections;
using System.Collections.Generic;

namespace Neat
{
    public interface IWidget<P>
    {
        P Props { get; }
        P PrevProps { get; }
    }
}
