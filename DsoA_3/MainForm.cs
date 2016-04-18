//Isadora Persson, AC7479, 19/10 -14
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DsoA_3
{
    public partial class MainForm : Form
    {
        BinaryTree myTree;

        public MainForm()
        {
            InitializeComponent();
            myTree = new AVLTree();
            cmbTreeChoice.SelectedIndex = 1;

            lblRoot.Text = "Empty tree";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int value;
            if (int.TryParse(tbxValueToAdd.Text, out value))
            {
                if (myTree.Insert(value) != null)
                {
                    ShowOrder();
                    lblRoot.Text = "Root: " + myTree.Root.Value.ToString();
                }
                else
                    MessageBox.Show("Value already exists");
            }
            else
                MessageBox.Show("Only integers plz");
        }

        private void cmbOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowOrder();
        }

        private void ShowOrder()
        {
            lblResult.Text = "";
            List<TreeNode> temp = new List<TreeNode>();
            switch (cmbOrder.SelectedIndex)
            {
                case 0: myTree.PreorderTraversal(myTree.Root, ref temp); 
                    break;
                case 1: myTree.InorderTraversal(myTree.Root, ref temp); 
                    break;
                case 2: myTree.PostorderTraversal(myTree.Root, ref temp);
                    break;
            }

            foreach (TreeNode i in temp)
            {
                lblResult.Text += i.Value.ToString() + " ";
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
           int value;
           if (int.TryParse(tbxValueToDelete.Text, out value))
            {
                if (myTree.Delete(value) != null)
                {               
                    ShowOrder();
                    if(myTree.Root != null)lblRoot.Text = "Root: " + myTree.Root.Value.ToString();
                    else lblRoot.Text = "Empty tree";
                }
                else
                    MessageBox.Show("Value could not be found");
            }
            else
                MessageBox.Show("Only integers plz");
        }

        private void cmbTreeChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbTreeChoice.SelectedIndex)
            {
                case 0:
                    myTree = new BinaryTree();                    
                    break;
                case 1:
                    myTree = new AVLTree();
                    break;
            }
            lblResult.Text = "";
        }
    }
}
