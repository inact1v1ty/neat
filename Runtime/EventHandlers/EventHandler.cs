using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Neat.Events
{
    public class EventHandler : MonoBehaviour
    {
        protected Action<Transform> callback;

        public void Setup(Action<Transform> callback)
        {
            this.callback = callback;
        }

        protected void Invoke()
        {
            var _callback = callback;
            callback = null;
            _callback?.Invoke(transform);
        }

        public void TearDown()
        {
            Destroy(this);
        }
    }
}
