using System;
using System.Collections.Generic;
using UnityEngine;

namespace Neat
{
    public enum TreeNodeState
    {
        Created,
        Mounted
    }

    public abstract class TreeNode
    {
        public abstract Type PType { get; }
        public abstract void Mount(GameObject root);
        public abstract void Unmount();
        public abstract GameObject GetGO(GameObject root);

        public TreeNode child;
    }

    public abstract class TreeNode<Props> : TreeNode
    {
        public Props currentProps;
    }

    public class WidgetTreeNode<Props> : TreeNode<Props>
    {
        public Widget<Props> widget;
        public override Type PType
        {
            get { return typeof(Props); }
        }

        public Node Render()
        {
            return widget.Render();
        }

        public override void Mount(GameObject root)
        {
            widget.OnComponentDidMount();
        }

        public override void Unmount()
        {
            widget.OnComponentWillUnmount();
        }

        public override GameObject GetGO(GameObject root)
        {
            return root;
        }
    }

    public class ComponentTreeNode<Props> : TreeNode<Props>
    {
        public GameObject go;
        public Component<Props> component;
        public override Type PType
        {
            get { return typeof(Props); }
        }

        public void Update() {
            component.Update();
        }

        public override void Mount(GameObject root)
        {
            go = new GameObject();
            go.transform.SetParent(root.transform);
            component.OnComponentDidMount(go);
        }

        public override void Unmount()
        {
            component.OnComponentWillUnmount();
        }

        public override GameObject GetGO(GameObject root)
        {
            return go;
        }
    }

    public class FragTreeNode : WidgetTreeNode<Frag>
    {
        public TreeNode[] children;
    }

    public class ElementTreeNode : ComponentTreeNode<Element>
    {
        public TreeNode[] children;
    }
}
