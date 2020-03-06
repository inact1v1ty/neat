using System;
using System.Collections.Generic;

namespace Neat
{ 
    public abstract class Node
    {
        public abstract Type PType { get; }
        public abstract TreeNode CreateTreeNode();
        public abstract void UpdateProps(TreeNode treeNode);

        public Node child;

        public static implicit operator Node[](Node node)
        {
            return new Node[1]
            {
                node
            };
        }
    }

    public class Node<Props> : Node
    {
        public Props props;
        public override Type PType {
            get { return typeof(Props); }
        }

        public override TreeNode CreateTreeNode()
        {
            var widgetReg = Registry.widgets[typeof(Props)];
            if (widgetReg.Native)
            {
                var widget = (NativeWidget<Props>)Activator.CreateInstance(widgetReg.WidgetType);
                return new NativeTreeNode<Props>
                {
                    currentProps = props,
                    widget = widget
                };
            }
            else
            {
                var widget = (Widget<Props>) Activator.CreateInstance(widgetReg.WidgetType);
                return new WidgetTreeNode<Props>
                {
                    currentProps = props,
                    widget = widget
                };
            }
        }

        public override void UpdateProps(TreeNode treeNode)
        {
            ((TreeNode<Props>)treeNode).currentProps = props;
        }
    }

    public class FragNode : Node<Frag>
    {
        public Node[] children;

        public override TreeNode CreateTreeNode()
        {
            var widget = new FragWidget();
            return new FragTreeNode()
            {
                currentProps = props,
                widget = widget
            };
        }
    }
}
