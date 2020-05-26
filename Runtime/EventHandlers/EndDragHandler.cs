using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Neat.Events
{
    public class EndDragHandler : EventHandler, IEndDragHandler
    {
        public void OnEndDrag(PointerEventData eventData)
        {
            this.Invoke();
        }
    }
}
