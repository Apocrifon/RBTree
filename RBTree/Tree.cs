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
                root.Color = Colors.Black;
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
            if (node == root)
            {
                node.Color = Colors.Black;
                return;
            }
            while (node.Parent != null && node.Parent.Color == Colors.Red)
            {
                if (node.Parent.LeftConnected())
                {
                    if (node.Uncle != null && node.Uncle.Color == Colors.Red)
                        ColorSwap(node);
                    else
                    {
                        if (!node.LeftConnected())
                        {
                            node = node.Parent;
                            LeftRotate(node);
                        }
                        node.Parent.Color = Colors.Black;
                        node.GrandFather.Color = Colors.Red;
                        RightRotate(node.GrandFather);
                    }
                }
                else
                {
                    if (node.Uncle != null && node.Uncle.Color == Colors.Red)
                        ColorSwap(node);
                    else
                    {
                        if (node.LeftConnected())
                        {
                            node = node.Parent;
                            RightRotate(node);
                        }
                        node.Parent.Color = Colors.Black;
                        node.GrandFather.Color = Colors.Red;
                        LeftRotate(node.GrandFather);
                    }
                }
                root.Color= Colors.Black;
            }
        }

        private void ColorSwap(Node node)
        {
            node.Parent.Color = Colors.Black;
            node.Uncle.Color = Colors.Black;
            node.GrandFather.Color = Colors.Red;
            node = node.GrandFather;
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
            if (node != root)
            {
                if (node.LeftConnected())
                    node.Parent.Left = node.Right;
                else
                    node.Parent.Right = node.Right;
            }
            else
                root = node.Right;
            node.Right.Left = node;
            node.Right.Level--;
            node.Right.Right.Level--;
            node.Level++;
            node.Left = null;
            node.Right = null;
        }

        public void RightRotate(Node node)
        {
            if (node != root)
            {
                if (node.LeftConnected())
                    node.Parent.Left = node.Left;
                else
                    node.Parent.Right = node.Left;
            }
            else
                root = node.Left;
            node.Left.Right = node;
            node.Left.Level--;
            node.Left.Left.Level--;
            node.Level++;
            node.Left = null;
            node.Right = null;
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
