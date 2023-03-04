using System;
using System.Collections.Generic;

namespace RBTree
{


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
        
        public Node GetNode(int value)
        {
            var node = root;
            while (node!=null)
            {
                if (node.Key == value)
                    return node;
                node = node.Key < value ? node.Right : node.Left;
            }
            return null;
        }

        public void LeftRotate(Node node)
        {
            var father = node.Parent;
            var grandFather = node.GrandFather;
            if (grandFather==root)
            {
                var temp = grandFather;
                grandFather.Level++;
                root = father;
                root.Level--;
                node.Level--;
                root.Parent = null;
                root.Left=temp;
                root.Left.Right = null;
                root.Right=node;
                node.Parent = root;                
            }
        }

        public void Print()
        {
            if (root == null)
                return;
            var queue = new Queue<Node>();
            var level = root.Level;
            queue.Enqueue(root);
            //Console.WriteLine(new string(' ', deep+deep-1)); // fix intervals
            while (queue.Count != 0)
            {
                var node = queue.Dequeue();
                if (node.Level > level)
                {
                    Console.WriteLine();
                    level++;
                }
                if (node.Color==Colors.Red)
                {
                    Console.ForegroundColor= ConsoleColor.Red;
                }
                Console.Write(node.Key);
                Console.ResetColor();
                Console.Write("  ");
                if (node.Left != null)
                    queue.Enqueue(node.Left);
                if (node.Right != null)
                    queue.Enqueue(node.Right);
            }
            Console.WriteLine();
        }
    }

}
