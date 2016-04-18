//Isadora Persson, AC7479, 19/10 -14
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DsoA_3
{
    class AVLTree : BinaryTree
    {
        private TreeNode myX, myY, myZ, myW;

        public override TreeNode Insert(int aValue)
        {
            TreeNode nodeToInsert = base.Insert(aValue);

            if (!IsBalanced(myRoot))
            {
                myW = nodeToInsert;
                myZ = FindUnbalancedNode(myW);
                myY = FindYForInsert(myZ);
                myX = FindXForInsert(myY, myW);
                TriNodeRestruct(myZ);
            }
            return nodeToInsert;   
        }

        private TreeNode FindYForInsert(TreeNode aNodeZ)
        {
            return (GetHeight(aNodeZ.Left) > GetHeight(aNodeZ.Right) ? aNodeZ.Left : aNodeZ.Right);
        }

        private TreeNode FindXForInsert(TreeNode aNodeY, TreeNode aNodeW)
        {
            if (GetHeight(aNodeY.Left) > GetHeight(aNodeY.Right))
                return aNodeY.Left;
            else if (GetHeight(aNodeY.Left) < GetHeight(aNodeY.Right))
                return aNodeY.Right;
            else
            {
                List<TreeNode> temp = new List<TreeNode>();
                PreorderTraversal(myRoot, ref temp);
                foreach (TreeNode node in temp)
                {
                    if (node.Value == aNodeW.Value)
                        return aNodeW;
                    else if (node.Value == aNodeY.Left.Value)
                        return aNodeY.Left;
                    else if (node.Value == aNodeY.Right.Value)
                        return aNodeY.Right;
                }
            }
            return null;
        }

        public override TreeNode Delete(int aValue)
        {
            TreeNode myW = base.Delete(aValue);

            if (myW == null) return null; //value could not be found
            
            while ((myZ = FindUnbalancedNode(myW)) != myRoot) //while there is a unbalanced node up in tree that is not root
            {
                myY = FindYForDelete(myZ, myW);
                myX = FindXForDelete(myY);

                TriNodeRestruct(myZ);

                myW = myZ; //check from z and up to root next time
            }

            if (!IsBalanced(myRoot)) //finally check if root is balanced 
            {
                myZ = myRoot;
                myY = FindYForDelete(myZ, myW);
                myX = FindXForDelete(myY);

                TriNodeRestruct(myZ);
            }

            return myW;
        }

        private TreeNode FindYForDelete(TreeNode aNodeZ, TreeNode aNodeW)
        {
            TreeNode parent = null;
            int value = aNodeW.Value;

            FindParent(value, ref parent);

            if (GetHeight(aNodeZ.Left) > GetHeight(aNodeZ.Right))
            {
                while (parent != null)
                {
                    if (parent == aNodeZ.Left)
                        return aNodeZ.Right;
                    value = parent.Value;
                    FindParent(value, ref parent);
                }
                return aNodeZ.Left;
            }
            else
            {
                while (parent != null)
                {
                    if (parent == aNodeZ.Right)
                        return aNodeZ.Left;
                    value = parent.Value;
                    FindParent(value, ref parent);
                }
                return aNodeZ.Right;
            }
        }

        private TreeNode FindXForDelete(TreeNode aNodeY)
        {
            if (GetHeight(aNodeY.Left) > GetHeight(aNodeY.Right))
                return aNodeY.Left;
            else
                return aNodeY.Right;
        }

        private TreeNode FindUnbalancedNode(TreeNode aNode)
        {
            if (aNode == myRoot) return aNode;

            TreeNode z = null;
            int value = aNode.Value;
            FindParent(value, ref z);

            if (IsBalanced(z))
                return FindUnbalancedNode(z);
            else
                return z;
        }

        private void TriNodeRestruct(TreeNode aTree)
        {
            List<TreeNode> inorder = new List<TreeNode>();
            InorderOfUnbalancedTree(aTree, ref inorder); //list of nodes inorder

            TreeNode t0 = inorder[0];
            TreeNode a = inorder[1];
            TreeNode t1 = inorder[2];
            TreeNode b = inorder[3];
            TreeNode t2 = inorder[4];
            TreeNode c = inorder[5];
            TreeNode t3 = inorder[6];

            TreeNode newTree = b;
            newTree.Left = a;
            a.Left = t0;
            a.Right = t1;
            b.Right = c;
            c.Left = t2;
            c.Right = t3;

            if (aTree == myRoot) 
                myRoot = b; 
            else
            {
                TreeNode parent = null;
                FindParent(aTree.Value, ref parent);

                if (parent.Left == aTree)
                    parent.Left = b;
                else
                    parent.Right = b;
            }
        }

        private void InorderOfUnbalancedTree(TreeNode aNode, ref List<TreeNode> anInOrderList)
        {
            if (aNode == myX || aNode == myY || aNode == myZ) //if the node is any of x, y, z
            {
                if (aNode.Left != myY && aNode.Left != myX) //if left child is not x or y 
                    anInOrderList.Add(aNode.Left); //add left node that is not x or y
                else
                    InorderOfUnbalancedTree(aNode.Left, ref anInOrderList); //else, check next left

                anInOrderList.Add(aNode); //add node that is x, y or z

                if (aNode.Right != myY && aNode.Right != myX) //if right child is not x or y 
                    anInOrderList.Add(aNode.Right); //add right child that is not x or y
                else
                    InorderOfUnbalancedTree(aNode.Right, ref anInOrderList); //else, check next right
            }
        }

        private bool IsBalanced(TreeNode aNode)
        {
            if (aNode == null) return true;
            int balance = (GetHeight(aNode.Left) - GetHeight(aNode.Right));
            return (balance <= 1 && balance >= -1);
        }

        private int GetHeight(TreeNode aStartNode)
        {
            if (aStartNode == null)
                return 0;
            else
                return Math.Max(GetHeight(aStartNode.Left), GetHeight(aStartNode.Right)) + 1;
        }
    }
}
