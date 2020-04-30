using System;
using System.Collections.Generic;

namespace Neat
{ 
    public abstract class Node
    {
        public abstract Type PropsType { get; }

        public abstract TreeNode CreateTreeNode();
        public abstract void UpdateProps(TreeNode treeNode);

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
        public override Type PropsType {
            get { return typeof(Props); }
        }

        public override TreeNode CreateTreeNode()
        {
            var widgetReg = Registry.registry[typeof(Props)];
            switch (widgetReg.RegType)
            {
                case RegType.Widget:
                    var widget = (Widget<Props>)Activator.CreateInstance(widgetReg.WidgetType);
                    return new WidgetTreeNode<Props>
                    {
                        currentProps = props,
                        widget = widget
                    };
                case RegType.Component:
                    var component = (Component<Props>)Activator.CreateInstance(widgetReg.WidgetType);
                    return new ComponentTreeNode<Props>
                    {
                        currentProps = props,
                        component = component
                    };
                case RegType.Fragment:
                    return new FragTreeNode
                    {
                        currentProps = (Frag)(object)props,
                    };
                case RegType.Element:
                    var elementComp = new ElementWidget();
                    return new ElementTreeNode
                    {
                        currentProps = (Element)(object)props,
                        component = elementComp
                    };
                default:
                    throw new Exception();
            }
        }

        public override void UpdateProps(TreeNode treeNode)
        {
            ((TreeNode<Props>)treeNode).currentProps = props;
        }
    }
}
