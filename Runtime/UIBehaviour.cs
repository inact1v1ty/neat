using System;
using System.Collections.Generic;
using UnityEngine;

namespace Neat
{
    public abstract class UIBehaviour : MonoBehaviour
    {
        public abstract Node Render();

        private TreeNode rootTreeNode = null;

        public void Test()
        {
            var rootNode = Render();
            rootTreeNode = DFS(rootNode, rootTreeNode, gameObject);
        }

        private TreeNode DFS(Node node, TreeNode treeNode, GameObject go)
        {
            var cTreeNode = treeNode;
            if (treeNode == null || node.PType != treeNode.PType)
            {
                treeNode?.Unmount();
                var newTreeNode = node.CreateTreeNode();
                newTreeNode.Mount(go);
                cTreeNode = newTreeNode;
            } else
            {
                node.UpdateProps(cTreeNode);
            }
            cTreeNode.Update();

            if (node.PType == typeof(Frag))
            {
                var nodeF = node as FragNode;
                var treeNodeF = cTreeNode as FragTreeNode;
                var children = nodeF.props.Children;

                for (
                    int i = (children?.Length).GetValueOrDefault(0);
                    i < (treeNodeF.children?.Length).GetValueOrDefault(0);
                    ++i
                )
                {
                    treeNodeF.children[i].Unmount();
                }

                Array.Resize(ref treeNodeF.children, (children?.Length).GetValueOrDefault(0));

                var currentGo = treeNodeF.GetGO(go);

                for (int i = 0; i < (children?.Length).GetValueOrDefault(0); ++i)
                {
                    treeNodeF.children[i] = DFS(children[i], treeNodeF.children[i], currentGo);
                }
            }
            else
            {
                node.child = cTreeNode.Render();
                if (node.child == null)
                {
                    cTreeNode.child?.Unmount();
                    cTreeNode.child = null;
                }
                else
                {
                    var currentGo = cTreeNode.GetGO(go);
                    if (cTreeNode.child != null)
                    {
                        cTreeNode.child.Unmount();
                    }
                    cTreeNode.child = DFS(node.child, cTreeNode.child, currentGo);
                }
            }

            return cTreeNode;
        }
    }
}
