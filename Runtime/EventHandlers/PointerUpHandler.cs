﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Neat.Events
{
    public class PointerUpHandler : EventHandler, IPointerUpHandler
    {
        public void OnPointerUp(PointerEventData eventData)
        {
            this.Invoke();
        }
    }
}
