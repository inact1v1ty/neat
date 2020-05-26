using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Neat.Events
{
    public class SelectHandler : EventHandler, ISelectHandler
    {
        public void OnSelect(BaseEventData eventData)
        {
            this.Invoke();
        }
    }
}
