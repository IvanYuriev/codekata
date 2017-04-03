using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateChop
{
    public class BinaryTreeChopper : IKarateChop
    {
        public int Chop(int lookingFor, IList<int> list)
        {
            var tree = new Tree(list);
            //Console.WriteLine(tree);

            var current = tree.Root;
            while(current != null)
            {
                if (current.Value == lookingFor)
                    return current.Index;
                else if (current.Value > lookingFor)
                    current = current.Left;
                else if (current.Value < lookingFor)
                    current = current.Right;
            }
            return -1;
        }

        private class TreeNode
        {
            /// <summary>
            /// Index of the value in initial array
            /// </summary>
            public int Index { get; set; } = 0;
            /// <summary>
            /// true if the value is in the left branch of the tree
            /// every node/leaf of the left branch has this value set to true, including the root element
            /// </summary>
            public bool TheMostLeft { get; set; } = false;
            /// <summary>
            /// The value that the node contains
            /// </summary>
            public int Value { get; set; } = -1;
            /// <summary>
            /// Left child node, null if no nodes.
            /// </summary>
            public TreeNode Left { get; set; } = null;
            /// <summary>
            /// Right child node, bull if no nodes.
            /// </summary>
            public TreeNode Right { get; set; } = null;
        }

        /// <summary>
        /// Optimized binary tree, but not fully functional Black-Red-Tree
        /// As we know that the input array is in sorted order, we can construct
        /// the kind of balanced binary tree. So the main task is to initialize/build
        /// the balanced binary tree from sorted list of values.
        /// </summary>
        private class Tree
        {
            private TreeNode _root;

            public Tree(IList<int> sortedList)
            {
                var middle = sortedList.Count / 2;
                if (middle >= sortedList.Count) return;

                _root = new TreeNode() { Value = sortedList[middle], Index = middle, TheMostLeft = true };
                InsertLeft(Root, sortedList.Take(middle).ToList());
                InsertRight(Root, sortedList.Skip(middle + 1).ToList());
            }

            private void InsertLeft(TreeNode current, IList<int> sortedArray)
            {
                var middle = sortedArray.Count / 2;
                if (middle >= sortedArray.Count) return;
                var val = sortedArray[middle];
                var preciseMiddle = (int)Math.Round(sortedArray.Count / 2.0f, 0, MidpointRounding.AwayFromZero);
                current.Left = new TreeNode()
                {
                    Value = val,
                    Index = current.Index - preciseMiddle,
                    TheMostLeft = current.TheMostLeft
                };
                current = current.Left;

                InsertLeft(current, sortedArray.Take(middle).ToList());
                InsertRight(current, sortedArray.Skip(middle + 1).ToList());
            }

            private void InsertRight(TreeNode current, IList<int> sortedArray)
            {
                var middle = sortedArray.Count / 2;
                if (middle >= sortedArray.Count) return;
                var val = sortedArray[middle];
                current.Right = new TreeNode()
                {
                    Value = val,
                    Index = current.Index + middle + 1,
                    TheMostLeft = false
                };
                current = current.Right;

                InsertLeft(current, sortedArray.Take(middle).ToList());
                InsertRight(current, sortedArray.Skip(middle + 1).ToList());
            }

            public TreeNode Root { get { return _root; } }

            public override string ToString()
            {
                var result = new StringBuilder();
                var queue = new Queue<TreeNode>();
                queue.Enqueue(Root);
                while(queue.Count > 0)
                {
                    var node = queue.Dequeue();
                    if (node == null) continue;
                    if (node.TheMostLeft) result.AppendLine();
                    result.AppendFormat(" [{0}:{1}] ", node.Index, node.Value);
                    if (node.Left != null) queue.Enqueue(node.Left);
                    if (node.Right != null) queue.Enqueue(node.Right);
                }

                return result.ToString();
            }
        }
    }
}
