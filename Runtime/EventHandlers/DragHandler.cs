using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Neat.Events
{
    public class DragHandler : EventHandler, IDragHandler
    {
        public void OnDrag(PointerEventData eventData)
        {
            this.Invoke();
        }
    }
}
