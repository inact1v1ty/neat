using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Neat.Events
{
    public class ScrollHandler : EventHandler, IScrollHandler
    {
        public void OnScroll(PointerEventData eventData)
        {
            this.Invoke();
        }
    }
}
