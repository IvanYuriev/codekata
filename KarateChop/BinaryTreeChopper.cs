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
            Console.WriteLine(tree);

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
            public int Index { get; set; } = 0;
            public bool TheMostLeft { get; set; } = false;
            public int Value { get; set; } = -1;
            public TreeNode Left { get; set; } = null;
            public TreeNode Right { get; set; } = null;
        }

        private class Tree
        {
            private TreeNode _root;

            public Tree(IList<int> sortedList)
            {
                var middle = sortedList.Count / 2;
                if (middle >= sortedList.Count) return;

                _root = new TreeNode() { Value = sortedList[middle], Index = middle, TheMostLeft = true };
                Insert(Root, sortedList.Take(middle).ToList());
                Insert(Root, sortedList.Skip(middle + 1).ToList());
            }

            private void Insert(TreeNode current, IList<int> sortedArray)
            {
                var middle = sortedArray.Count / 2;
                if (middle >= sortedArray.Count) return;
                var val = sortedArray[middle];
                if (val > current.Value)
                {
                    current.Right = new TreeNode() {
                        Value = val,
                        Index = current.Index + middle + 1,
                        TheMostLeft = false };
                    current = current.Right;
                }
                else
                {
                    current.Left = new TreeNode() {
                        Value = val,
                        Index = current.Index - (int)Math.Round(sortedArray.Count / 2.0f, 0, MidpointRounding.AwayFromZero),
                        TheMostLeft = current.TheMostLeft};
                    current = current.Left;
                }
                Insert(current, sortedArray.Take(middle).ToList());
                Insert(current, sortedArray.Skip(middle + 1).ToList());
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
