using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Neat.Events
{
    public class InitializePotentialDragHandler : EventHandler, IInitializePotentialDragHandler
    {
        public void OnInitializePotentialDrag(PointerEventData eventData)
        {
            this.Invoke();
        }
    }
}
