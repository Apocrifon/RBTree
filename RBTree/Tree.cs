using System;
using System.Collections.Generic;

namespace RBTree
{
    public enum Colors
    {
        Red,
        Black
    }

    public class Tree
    {
        private Node root = null;
        public Node Root { get { return root; } }
        private int deep;
        public int Deep { get { return deep; } }

        public void Insert(int value)
        {
            var newNode = new Node(value);
            if (root == null)
            {
                root = new Node(value);
                root.Level = 1;
                return;
            }
            var curNode = root;
            var prevNode = curNode;
            while (curNode != null)
            {
                prevNode = curNode;
                curNode = curNode.Key < value ? curNode.Right : curNode.Left;
            }
            if (prevNode.Key > value)
                prevNode.Left = newNode;
            else
                prevNode.Right = newNode;

            newNode.Parent = prevNode;
            newNode.Level = newNode.Parent.Level + 1;
            deep=newNode.Level;
            Balanced(newNode);
        }
        // TODO
        public void Delete(int value)
        {

        }
        // TODO
        private void Balanced(Node node)
        {

        }

        public void Print()
        {
            if (root == null)
                return;
            var queue = new Queue<Node>();
            var level = root.Level;
            queue.Enqueue(root);
            Console.WriteLine(new string(' ', deep+deep-1)); // fix intervals
            while (queue.Count != 0)
            {
                var node = queue.Dequeue();
                if (node.Level > level)
                {
                    Console.WriteLine();
                    level++;
                }
                Console.Write(node.Key);
                Console.Write("  ");
                if (node.Left != null)
                    queue.Enqueue(node.Left);
                if (node.Right != null)
                    queue.Enqueue(node.Right);
            }
            Console.WriteLine();
        }
    }

    public class Node
    {
        public int Key { get; set; }
        public Colors Color { get; set; }
        public Node Parent { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
        public int Level { get; set; }  // Field create for print funk

        public Node(int key)
        {
            Key = key;
            Color = Colors.Red;
            Parent = null;
            Left = null;
            Right = null;
        }
    }
}
