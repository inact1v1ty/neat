﻿using System;
using System.Diagnostics;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Debug = UnityEngine.Debug;

using Neat.Events;
using EventHandler = Neat.Events.EventHandler;

namespace Neat
{
    public abstract class UIBehaviour : MonoBehaviour
    {
        private readonly Dictionary<string, Type> handlers = new Dictionary<string, Type>()
        {
            { "Click", typeof(PointerClickHandler) },
            { "PointerClick", typeof(PointerClickHandler) },
            { "PointerDown", typeof(PointerDownHandler) },
            { "PointerUp", typeof(PointerUpHandler) },
            { "PointerEnter", typeof(PointerEnterHandler) },
            { "PointerExit", typeof(PointerExitHandler) },
            { "InitializePotentialDrag", typeof(InitializePotentialDragHandler) },
            { "BeginDrag", typeof(BeginDragHandler) },
            { "Drag", typeof(DragHandler) },
            { "EndDrag", typeof(EndDragHandler) },
            { "Drop", typeof(DropHandler) },
            { "Scroll", typeof(ScrollHandler) },
            { "UpdateSelected", typeof(UpdateSelectedHandler) },
            { "Select", typeof(SelectHandler) },
            { "Deselect", typeof(DeselectHandler) },
            { "Move", typeof(MoveHandler) },
            { "Submit", typeof(SubmitHandler) },
            { "Cancel", typeof(CancelHandler) }
        };

        private readonly Dictionary<string, int> handlerIDs = new Dictionary<string, int>()
        {
            { "Click", 0 },
            { "PointerClick", 0 },
            { "PointerDown", 1 },
            { "PointerUp", 2 },
            { "PointerEnter", 3 },
            { "PointerExit", 4 },
            { "InitializePotentialDrag", 5 },
            { "BeginDrag", 6 },
            { "Drag", 7 },
            { "EndDrag", 8 },
            { "Drop", 9 },
            { "Scroll", 10 },
            { "UpdateSelected", 11 },
            { "Select", 12 },
            { "Deselect", 13 },
            { "Move", 14 },
            { "Submit", 15 },
            { "Cancel", 16 }
        };

        private Dictionary<(int, int), EventHandler> handlerCache = new Dictionary<(int, int), EventHandler>();

        protected abstract UINode Render();
        protected abstract Transform GetRoot();

        protected void ReDraw()
        {
            InternalRender();
        }

        private void InternalRender()
        {
#if NEAT_DEBUG
            Stopwatch sw = Stopwatch.StartNew();
#endif
            var rootNode = Render();
#if NEAT_DEBUG
            sw.Stop();
            var render = sw.Elapsed;
            sw.Restart();
#endif
            var rootTransform = GetRoot();
            var pseudoRoot = new UINode() { Children = new Node[] { rootNode } };
            DSLTraversal(pseudoRoot, rootTransform);
#if NEAT_DEBUG
            sw.Stop();
            UnityEngine.Debug.Log($"Render took {render.Milliseconds} ms, modifications took {sw.Elapsed.Milliseconds} ms");
#endif
        }

        protected virtual void Start()
        {
            InternalRender();
        }

        private void DSLTraversal(UINode root, Transform rootTransform)
        {

            var newCache = new Dictionary<(int, int), EventHandler>();

            int DOMDfs(UINode node, Transform current)
            {
                if (node.Children == null)
                    return 0;

                var children = new Dictionary<string, Transform>();

                int order = 0;

                foreach (Transform child in current)
                {
                    children.Add(child.name, child);
                }

                var orderUp = new List<(int priority, Transform transform)>();
                var orderDown = new List<(int priority, Transform transform)>();

                foreach (var child in node.Children)
                {
                    switch (child)
                    {
                        case UINode uiNode:
                            if (children.TryGetValue(uiNode.Name, out var tr))
                            {
                                tr.gameObject.SetActive(true);
                                children.Remove(uiNode.Name);
                                int childOrder = DOMDfs(uiNode, tr);
                                if (childOrder > 0)
                                {
                                    orderUp.Add((childOrder, tr));
                                }
                                else if (childOrder < 0)
                                {
                                    orderDown.Add((childOrder, tr));
                                }
                            }
                            else
                            {
                                throw new ElementNotFoundException($"No child of {current.name} with name {uiNode.Name}");
                            }

                            break;
                        case EventNode eventNode:
                            if (handlerIDs.TryGetValue(eventNode.Event, out int handlerID))
                            {
                                var key = (current.gameObject.GetInstanceID(), handlerID);
                                if (handlerCache.TryGetValue(key, out EventHandler handler))
                                {
                                    handlerCache.Remove(key);
                                }
                                else
                                {
                                    var handlerType = handlers[eventNode.Event];
                                    handler = current.gameObject.AddComponent(handlerType) as EventHandler;
                                }

                                handler.Setup(_ =>
                                {
                                    try
                                    {
                                        eventNode.Callback(current as RectTransform);
                                    }
                                    catch (Exception ex)
                                    {
                                        Debug.LogException(ex);
                                    }
                                    this.InternalRender();
                                });

                                newCache.Add(key, handler);
                            }
                            else
                            {
                                throw new ElementNotFoundException($"Wrong event type: {eventNode.Event}");
                            }
                            break;
                        case SetNode setNode:
                            try
                            {
                                setNode.Callback(current);
                            }
                            catch (Exception ex)
                            {
                                Debug.LogException(ex);
                            }
                            break;
                        case OrderNode orderNode:
                            order = orderNode.Order;
                            break;
                        case GetOrderNode getOrderNode:
                            try
                            {
                                order = getOrderNode.GetOrder(current as RectTransform);
                            }
                            catch (Exception ex)
                            {
                                Debug.LogException(ex);
                            }
                            break;
                    }
                }

                if (!node.Leaf)
                {
                    foreach (var unDrawn in children)
                    {
                        unDrawn.Value.gameObject.SetActive(false);
                    }
                }

                orderUp.Sort((a, b) => b.priority - a.priority);
                orderDown.Sort((a, b) => a.priority - b.priority);

                for (int i = 0; i < orderUp.Count; i++)
                {
                    orderUp[i].transform.SetSiblingIndex(i);
                }

                for (int i = 0; i < orderDown.Count; i++)
                {
                    orderDown[i].transform.SetSiblingIndex(current.childCount - i - 1);
                }

                return order;
            }

            DOMDfs(root, rootTransform);

            foreach (var p in handlerCache)
            {
                p.Value.TearDown();
            }

            handlerCache.Clear();
            handlerCache = newCache;
        }
    }
}
