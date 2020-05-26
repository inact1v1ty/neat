using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Neat.Events
{
    public class DeselectHandler : EventHandler, IDeselectHandler
    {
        public void OnDeselect(BaseEventData eventData)
        {
            this.Invoke();
        }
    }
}
