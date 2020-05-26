using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Neat.Events
{
    public class DropHandler : EventHandler, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            this.Invoke();
        }
    }
}
