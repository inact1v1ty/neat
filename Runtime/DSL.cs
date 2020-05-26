using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Neat
{
    public static class DSL
    {
        public static UINode Draw(string name, params Node[] children)
        {
            return new UINode() { Name = name, Leaf = false, Children = children };
        }

        public static UINode DrawLeaf(string name, params Node[] children)
        {
            return new UINode() { Name = name, Leaf = true, Children = children };
        }

        public static Node Event(string name, Action<RectTransform> callback)
        {
            return new EventNode() { Event = name, Callback = callback };
        }

        #region Events
        public static EventNode OnClick(Action<RectTransform> callback)
        {
            return new EventNode() { Event = "Click", Callback = callback };
        }
        public static EventNode OnPointerDown(Action<RectTransform> callback)
        {
            return new EventNode() { Event = "PointerDown", Callback = callback };
        }
        public static EventNode OnPointerUp(Action<RectTransform> callback)
        {
            return new EventNode() { Event = "PointerUp", Callback = callback };
        }
        public static EventNode OnPointerEnter(Action<RectTransform> callback)
        {
            return new EventNode() { Event = "PointerEnter", Callback = callback };
        }
        public static EventNode OnPointerExit(Action<RectTransform> callback)
        {
            return new EventNode() { Event = "PointerExit", Callback = callback };
        }
        public static EventNode OnInitializePotentialDrag(Action<RectTransform> callback)
        {
            return new EventNode() { Event = "InitializePotentialDrag", Callback = callback };
        }
        public static EventNode OnBeginDrag(Action<RectTransform> callback)
        {
            return new EventNode() { Event = "BeginDrag", Callback = callback };
        }
        public static EventNode OnDrag(Action<RectTransform> callback)
        {
            return new EventNode() { Event = "Drag", Callback = callback };
        }
        public static EventNode OnEndDrag(Action<RectTransform> callback)
        {
            return new EventNode() { Event = "EndDrag", Callback = callback };
        }
        public static EventNode OnDrop(Action<RectTransform> callback)
        {
            return new EventNode() { Event = "Drop", Callback = callback };
        }
        public static EventNode OnScroll(Action<RectTransform> callback)
        {
            return new EventNode() { Event = "Scroll", Callback = callback };
        }
        public static EventNode OnUpdateSelected(Action<RectTransform> callback)
        {
            return new EventNode() { Event = "UpdateSelected", Callback = callback };
        }
        public static EventNode OnSelect(Action<RectTransform> callback)
        {
            return new EventNode() { Event = "Select", Callback = callback };
        }
        public static EventNode OnDeselect(Action<RectTransform> callback)
        {
            return new EventNode() { Event = "Deselect", Callback = callback };
        }
        public static EventNode OnMove(Action<RectTransform> callback)
        {
            return new EventNode() { Event = "Move", Callback = callback };
        }
        public static EventNode OnSubmit(Action<RectTransform> callback)
        {
            return new EventNode() { Event = "Submit", Callback = callback };
        }
        public static EventNode OnCancel(Action<RectTransform> callback)
        {
            return new EventNode() { Event = "Cancel", Callback = callback };
        }
        #endregion

        public static SetNode Set<T>(Action<T> callback)
        {
            Action<Transform> cb = (tr) =>
            {
                var t = tr.GetComponent<T>();
                callback(t);
            };

            return new SetNode() { Callback = cb };
        }

        public static SetNode SetTransform(Action<RectTransform> callback)
        {
            Action<Transform> cb = (tr) =>
            {
                var rt = (RectTransform)tr;
                callback(rt);
            };
            return new SetNode() { Callback = cb };
        }

        public static Node Do(Func<Node> func)
        {
            return func();
        }

        public static OrderNode Order(int order)
        {
            if (order != 0)
                return new OrderNode() { Order = order };
            return null;
        }

        public static GetOrderNode GetOrder(Func<RectTransform, int> getOrder)
        {
            return new GetOrderNode() { GetOrder = getOrder };
        }

        public static RefNode BindRef(Ref @ref)
        {
            return new RefNode() { Ref = @ref };
        }
    }
}