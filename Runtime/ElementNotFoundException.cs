using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Neat
{
    public class ElementNotFoundException : Exception
    {
        public ElementNotFoundException(string message) : base(message)
        {

        }
    }
}
