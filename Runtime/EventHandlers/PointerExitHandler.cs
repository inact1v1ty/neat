using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Neat.Events
{
    public class PointerExitHandler : EventHandler, IPointerExitHandler
    {
        public void OnPointerExit(PointerEventData eventData)
        {
            this.Invoke();
        }
    }
}
