using Microsoft.CodeAnalysis;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FutureValue.Models
{
    public class FutureValueModel
    {

        int? outputleft = 0;
        int? outputRight = 0;
        public FutureValueModel() {

            //Demonstation
            
            var myString = new StringBuilder("Glorious Embrance");
            while(myString.Length > 3)
            {
                myString.Remove(myString.Length - 1, 1);
            }






            //Build tree and return both roots
            var (rootLeft, rootRight) = Createtree();

            //Debug1
            Build();

            string thisnothing = "";


            //Thread mySecondThread = new Thread(() => TraverseIn(rootLeft));
            //Thread myThirdThread = new Thread(() => TraversePost(rootLeft));
            //mySecondThread.Name = "MySecondThead";
            //myThirdThread.Name = "MyThirdThead";
            //mySecondThread.Start();
            //myThirdThread.Start();
            PrintABCs();
        }

        

        [Required(ErrorMessage = "Please enter a monthly investment.")]
        [Range(1, 500, ErrorMessage =
               "Monthly investment amount must be between 1 and 500.")]
        public decimal? MonthlyInvestment { get; set; }

        [Required(ErrorMessage = "Please enter a yearly interest rate.")]
        [Range(0.1, 10.0, ErrorMessage =
               "Yearly interest rate must be between 0.1 and 10.0.")]
        public decimal? YearlyInterestRate { get; set; }

        [Required(ErrorMessage = "Please enter a number of years.")]
        [Range(1, 50, ErrorMessage =
               "Number of years must be between 1 and 50.")]
        public int? Years { get; set; }

        public decimal? CalculateFutureValue()
        {
            int? months = Years * 12;
            decimal? monthlyInterestRate = YearlyInterestRate / 12 / 100;
            decimal? futureValue = 0;
            for (int i = 0; i < months; i++)
            {
                futureValue = (futureValue + MonthlyInvestment) *
                                (1 + monthlyInterestRate);
            }
            return futureValue;
        }

        public  class TreeNode
        {
            private static int _globalId = 0;
            public int ID { get; set; }
            public int value { get; set; }
            public TreeNode? left { get; set; }
            public TreeNode? right { get; set; }
            
            public static int TreeSize = 0;

            public TreeNode(int value, TreeNode? left = null, TreeNode? right = null)
            {
                ID = _globalId++;
                this.value = value;
                this.left = left;
                this.right = right;
            }
        }

        private static readonly object syncLock = new object();
        private static int? sharedValue = null;  // Assuming value is of type int

        //inorder
        public void TraverseIn( TreeNode? root )
        {
            if (root == null)
            {
                return;
            }

            
            TraverseIn(root.left);
            outputleft = root.value;
            TraverseIn(root.right);

            lock (syncLock)
            {
                if (sharedValue.HasValue && sharedValue.Value == root.value)
                {
                    // The value of root in TraversePost is the same as in TraverseIn
                    var caught = $"Value {root.value} is the same in both threads!";
                }
                else
                {
                    sharedValue = root.value;
                }
            }
        }

        //postorder
        public void TraversePost(TreeNode? root)
        {
            if (root == null)
            {
                return;
            }


            TraversePost(root.left);
            TraversePost(root.right);
            outputRight = root.value;

            lock (syncLock)
            {
                if (sharedValue.HasValue && sharedValue.Value == root.value)
                {
                    // The value of root in TraversePost is the same as in TraverseIn
                    var caught = $"Value {root.value} is the same in both threads!";
                }
                else
                {
                    sharedValue = root.value;
                }
            }

        }
        public (TreeNode?, TreeNode?) Createtree()
        {
            Queue<TreeNode?> rootLeftStack = new Queue<TreeNode?>();
            rootLeftStack.Enqueue(new TreeNode(1));

            TreeNode? RootNodeLeft = rootLeftStack.Peek();

           
            int treeSize = 1;

            

            while (treeSize < 50 && rootLeftStack.Count != 0)
            {
                var rootLeft = rootLeftStack.Dequeue();

                rootLeft.left = new TreeNode(treeSize++);
                rootLeft.right = new TreeNode(treeSize++);

                rootLeftStack.Enqueue(rootLeft.left);
                rootLeftStack.Enqueue(rootLeft.right);
                
                if (rootLeft.left == null || rootLeft.right == null)
                {
                    rootLeftStack.Enqueue(rootLeft);
                }
            }
            
            treeSize = 1;
            Queue<TreeNode?> rootRightStack = new Queue<TreeNode?>();
            rootRightStack.Enqueue(new TreeNode(1));

            TreeNode? RootNodeRight = rootRightStack.Peek();

            while (treeSize < 50 && rootRightStack.Count != 0)
            {
                var rootRight = rootLeftStack.Dequeue();

                rootRight.right = new TreeNode(treeSize++);
                rootRight.left = new TreeNode(treeSize++);
                

                rootRightStack.Enqueue(rootRight.left);
                rootRightStack.Enqueue(rootRight.right);

                if (rootRight.left == null || rootRight.right == null)
                {
                    rootRightStack.Enqueue(rootRight);
                }
            }

            return (RootNodeLeft, RootNodeRight) ;

        }


        /// <summary>
        /// Debug Exercises:
        /// 1. Find The ID of nodes with values {65, 68, 90)
        ///    a) using the immediate window, what are is the ID 68 when mult by *34/5%4 equal to?
        /// 2. What are the node values when breakpoint at line 192 gets hit 80 times
        /// 3. without adding any code to the PrintInOrder method, print all values of the BrinaryTree 
        /// to the output window. Do you have the min value of 40, and the max value of 200?
        /// </summary>
        public TreeNode Root { get; private set; }

        private void Build()
        {
            int x = 40, y = 200;
            BinarySearchTree bst = new BinarySearchTree();

            for (int i = x; i <= y; i++)
            {
                bst.Insert(i); // Just insert value; ID is auto-generated
            }

            // For visualization purposes, let's print the tree in-order
            PrintInOrder(bst.Root);
        }


        //Focus on this method***************************************************************
        public List<int> myintlist = new List<int>();
        private void PrintInOrder(TreeNode node)
        {
            if (node != null)
            {
                PrintInOrder(node.left);
                outputRight = node.value;

                PrintInOrder(node.right);
            }
        }
        //************************************************************************************
        public class BinarySearchTree
        {
            public TreeNode Root { get; private set; }

            public void Insert(int value)
            {
                TreeNode newNode = new TreeNode(value); // No need to pass ID; it's auto-generated

                if (Root == null)
                {
                    Root = newNode;
                    return;
                }

                TreeNode current = Root;
                TreeNode parent = null;

                while (true)
                {
                    parent = current;
                    if (value < current.value)
                    {
                        current = current.left;
                        if (current == null)
                        {
                            parent.left = newNode;
                            return;
                        }
                    }
                    else
                    {
                        current = current.right;
                        if (current == null)
                        {
                            parent.right = newNode;
                            return;
                        }
                    }
                }
            }
        }


        ///In the below area of code write The ABCS to the Output window. then use this method in public FutureValueModel(), to activate it.

        public void PrintABCs()
        {
            for (char letter = 'A'; letter <= 'Z'; letter++)
            {
                //Nope!!!   not using Console.WriteLine
                var myLetter = letter;
            }
        }
    }
}