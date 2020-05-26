using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Neat.Events
{
    public class PointerClickHandler : EventHandler, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            this.Invoke();
        }
    }
}
