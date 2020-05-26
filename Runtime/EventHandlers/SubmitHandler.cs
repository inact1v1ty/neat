using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Neat.Events
{
    public class SubmitHandler : EventHandler, ISubmitHandler
    {
        public void OnSubmit(BaseEventData eventData)
        {
            this.Invoke();
        }
    }
}
