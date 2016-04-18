//Isadora Persson, AC7479, 19/10 -14
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DsoA_3
{
    class BinaryTree
    {
        protected TreeNode myRoot; 
        protected int mySize = 0;

        public BinaryTree()
        {
            myRoot = null;
            mySize = 0;
        }

        public TreeNode Root
        {
            get { return myRoot; }
        }

        public virtual TreeNode Insert(int aValue)
        {
            TreeNode parent = null;
            if (FindParent(aValue, ref parent) != null) return null;

            TreeNode nodeToInsert = new TreeNode(aValue);

            if (myRoot == null)
                myRoot = nodeToInsert;
            else             
            {
                //Add(node, ref myRoot); 
                if (parent.Value > aValue)
                    parent.Left = nodeToInsert;
                else
                    parent.Right = nodeToInsert;
            }

            mySize++;
            return nodeToInsert;
        }

        //private void Add(TreeNode aNode, ref TreeNode aTree)
        //{
        //    if (aTree != aNode)
        //    {
        //        TreeNode next;

        //        if (aNode.Value < aTree.Value)
        //        {
        //            if (aTree.Left == null)
        //                aTree.Left = aNode;
        //            next = aTree.Left;
        //        }
        //        else
        //        {
        //            if (aTree.Right == null)
        //                aTree.Right = aNode;
        //            next = aTree.Right;
        //        }

        //        Add(aNode, ref next);
        //    }
        //}

        protected TreeNode FindValue(int aValue)
        {
            TreeNode currentNode = myRoot;
            while (currentNode != null)
            {                
                if (aValue < currentNode.Value)
                    currentNode = currentNode.Left;
                else if (aValue > currentNode.Value)
                    currentNode = currentNode.Right;
                else
                    return currentNode;
            }
            return null;
        }

        protected TreeNode FindParent(int aValue, ref TreeNode aParent)
        {
            TreeNode currentNode = myRoot;
            aParent = null;

            while (currentNode != null)
            {
                if (aValue < currentNode.Value)
                {
                    aParent = currentNode;
                    currentNode = currentNode.Left;
                }
                else if (aValue > currentNode.Value)
                {
                    aParent = currentNode;
                    currentNode = currentNode.Right;
                }
                else
                    return currentNode;
            }
            return null;
        }

        private TreeNode LeftMostNodeOnRight(TreeNode currentNode, ref TreeNode aParent)
        {
            aParent = currentNode;
            currentNode = currentNode.Right;
            while (currentNode.Left != null)
            {
                aParent = currentNode;
                currentNode = currentNode.Left;
            }
            return currentNode;
        }

        public virtual TreeNode Delete(int aValue)
        {
            TreeNode parent = null;
            TreeNode nodeToDelete = FindParent(aValue, ref parent);

            if (nodeToDelete == null) return null; //if node is null - value was not found - cancel delete

            switch (NrOfChildren(nodeToDelete))
            {
                case 0: //no children
                    if (nodeToDelete == myRoot)
                        myRoot = null;
                    else if (parent.Left == nodeToDelete)
                        parent.Left = null;
                    else
                        parent.Right = null;
                    break;

                case 1: //1 child
                    if (nodeToDelete == myRoot) //node is root
                        myRoot = ((nodeToDelete.Left != null) ? nodeToDelete.Left : nodeToDelete.Right); //check which child is not null and replace root with it

                    else if (parent.Left == nodeToDelete) //Node is left of parent 
                        parent.Left = ((nodeToDelete.Left != null) ? nodeToDelete.Left : nodeToDelete.Right);

                    else //Node is right of parent 
                        parent.Right = ((nodeToDelete.Left != null) ? nodeToDelete.Left : nodeToDelete.Right);

                    break;

                case 2: //2 children
                    TreeNode successor = LeftMostNodeOnRight(nodeToDelete, ref parent);
                    TreeNode temp = new TreeNode(successor.Value);

                    if (parent.Left == successor)
                        parent.Left = successor.Right;
                    else
                        parent.Right = successor.Right;

                    nodeToDelete.Value = temp.Value;
                    break;
            }
            mySize--;
            return nodeToDelete;
        }

        protected int NrOfChildren(TreeNode aNode)
        {
            if (aNode.Left == null && aNode.Right == null) return 0; //both children is null - 0 children
            else if (aNode.Left != null && aNode.Right != null) return 2; //no children are null - 2 children
            else return 1; //one child is null and one is not - 1 child
        }

        public void PreorderTraversal(TreeNode aNode, ref List<TreeNode> preOrderList)
        {
            if (aNode != null)
            {
                preOrderList.Add(aNode);
                PreorderTraversal(aNode.Left, ref preOrderList);
                PreorderTraversal(aNode.Right, ref preOrderList);
            }
        }

        public void InorderTraversal(TreeNode aNode, ref List<TreeNode> inOrderList)
        {
            if (aNode != null)
            {
                InorderTraversal(aNode.Left, ref inOrderList);
                inOrderList.Add(aNode);
                InorderTraversal(aNode.Right, ref inOrderList);
            }
        }

        public void PostorderTraversal(TreeNode aNode, ref List<TreeNode> postOrderList)
        {
            if (aNode != null)
            {
                PostorderTraversal(aNode.Left, ref postOrderList);
                PostorderTraversal(aNode.Right, ref postOrderList);
                postOrderList.Add(aNode);
            }
        }
    }
}
