using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Neat.Events
{
    public class BeginDragHandler : EventHandler, IBeginDragHandler
    {
        public void OnBeginDrag(PointerEventData eventData)
        {
            this.Invoke();
        }
    }
}
