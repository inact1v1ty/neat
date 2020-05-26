using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Neat.Events
{
    public class PointerDownHandler : EventHandler, IPointerDownHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            this.Invoke();
        }
    }
}
