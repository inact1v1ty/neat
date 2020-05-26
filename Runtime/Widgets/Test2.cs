using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neat
{
    public struct Test2 : IComponent<string>
    {
        public string text;

        public void Update(string component)
        {
            //throw new NotImplementedException();
            DSL.R(new Test2());


        }
    }
}
