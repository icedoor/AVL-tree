using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DsoA_3
{
    class TreeNode
    {
        private int myValue;
        private TreeNode myLeft, myRight;

        public int Value
        {
            get
            {
                return myValue;
            }
            set 
            {
                myValue = value;
            }
        }

        public TreeNode Left
        { 
            get
            {
                return myLeft;
            }
            set
            {
                myLeft = value;
            }
        }

        public TreeNode Right
        {
            get
            {
                return myRight;
            }
            set
            {
                myRight = value;
            }
        }

        public TreeNode(int aValue)
        {
            myValue = aValue; 
            myLeft = null; 
            myRight = null;
        }

        public TreeNode(TreeNode aTreeNode)
        {
            myLeft = aTreeNode.Left;
            myRight = aTreeNode.Right;
            myValue = aTreeNode.Value;
        }
    }
}
