using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Neat.Events
{
    public class PointerEnterHandler : EventHandler, IPointerEnterHandler
    {
        public void OnPointerEnter(PointerEventData eventData)
        {
            this.Invoke();
        }
    }
}
