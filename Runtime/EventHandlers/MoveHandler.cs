using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Neat.Events
{
    public class MoveHandler : EventHandler, IMoveHandler
    {
        public void OnMove(AxisEventData eventData)
        {
            this.Invoke();
        }
    }
}
