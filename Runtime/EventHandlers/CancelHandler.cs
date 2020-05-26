using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Neat.Events
{
    public class CancelHandler : EventHandler, ICancelHandler
    {
        public void OnCancel(BaseEventData eventData)
        {
            this.Invoke();
        }
    }
}
