using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Neat.Events
{
    public class UpdateSelectedHandler : EventHandler, IUpdateSelectedHandler
    {
        public void OnUpdateSelected(BaseEventData eventData)
        {
            this.Invoke();
        }
    }
}
