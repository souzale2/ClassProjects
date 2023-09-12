using Microsoft.CodeAnalysis;
using System.ComponentModel.DataAnnotations;


namespace FutureValue.Models
{
    public class FutureValueModel
    {

        int outputleft = 0;
        int outputRight = 0;
        public FutureValueModel() {
            var (rootLeft, rootRight) = Createtree();

            print(rootLeft);

            Thread mySecondThread = new Thread(() => TraverseIn(rootLeft));
            Thread myThirdThread = new Thread(() => TraversePost(rootLeft));
            mySecondThread.Start();
            myThirdThread.Start();
        }

        public void print(TreeNode root)
        {
            TraverseIn(root);
            TraversePost(root);
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
            public int value { get; set; }
            public TreeNode? left { get; set; }
            public TreeNode? right { get; set; }
            
            public static int TreeSize = 0;

            public TreeNode(int value, TreeNode? left = null, TreeNode? right = null)
            {
                this.value = value;
                this.left = left;
                this.right = right;
            }
        }

        

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
    }
}