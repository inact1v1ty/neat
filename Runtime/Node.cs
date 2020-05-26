﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Neat
{
    public abstract class Node
    {
    }

    public class UINode: Node
    {
        public string Name { get; set; }
        public bool Leaf { get; set; }
        public Node[] Children { get; set; }
    }

    public class EventNode: Node
    {
        public string Event { get; set; }
        public Action<RectTransform> Callback { get; set; }
    }

    public class SetNode : Node
    {
        public Action<Transform> Callback { get; set; }
    }

    public class OrderNode : Node
    {
        public int Order { get; set; }
    }

    public class GetOrderNode : Node
    {
        public Func<RectTransform, int> GetOrder { get; set; }
    }
}