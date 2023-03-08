﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
            InsertBalanced(newNode);
        }

        private void InsertBalanced(Node node)
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
                    {
                        ColorSwap(node);
                        node = node.GrandFather;
                    }
                    else
                    {
                        if (!node.LeftConnected())
                        {
                            node = node.Parent;
                            LeftRotate(node);
                        }
                        node.Parent.Color = Colors.Black;
                        if (node.GrandFather != null)
                            node.GrandFather.Color = Colors.Red;
                        RightRotate(node.GrandFather);
                    }
                }
                else
                {
                    if (node.Uncle != null && node.Uncle.Color == Colors.Red)
                    {
                        ColorSwap(node);
                        node = node.GrandFather;
                    }
                    else
                    {
                        if (node.LeftConnected())
                        {
                            node = node.Parent;
                            RightRotate(node);
                        }
                        node.Parent.Color = Colors.Black;
                        if (node.GrandFather != null)
                            node.GrandFather.Color = Colors.Red;
                        LeftRotate(node.GrandFather);
                    }
                }
                root.Color= Colors.Black;
            }
        }

        public void Delete(int value)
        {

        }

        private void DeleteBalanced(Node node)
        {

        }

        private Node FindNode(int key)
        {
            var p = root;
            while (p.Key != key)
                p = p.Key > key ? p.Left : p.Right;
            return p;
        }

        public Node NextNode(Node node)
        {
            if (node.Left != null)
            {
                node = node.Left;
                while (node.Right!=null)
                    node = node.Right;
            }
            else
            {
                node = node.Right;
                while (node.Left != null)
                    node = node.Left;
            }
            return node;
        }



        public void LeftRotate(Node node)
        {
            var rightSon = node.Right;
            if (node == root)
            {
                root = rightSon;
                rightSon.Parent = null;
            }
            else
            {
                rightSon.Parent = node.Parent;
                if (node.LeftConnected())
                    node.Parent.Left = rightSon;
                else
                    node.Parent.Right = rightSon;
            }
            node.Right = rightSon.Left;
            if (rightSon.Left != null)
                rightSon.Left.Parent = node;
            rightSon.Left = node;
            node.Parent = rightSon;
        }

        public void RightRotate(Node node)
        {
            var leftSon = node.Left;
            if (node == root)
            {
                root = leftSon;
                leftSon.Parent = null;
            }
            else
            {
                leftSon.Parent = node.Parent;
                if (node.LeftConnected())
                    node.Parent.Left = leftSon;
                else
                    node.Parent.Right = leftSon;
            }
            leftSon.Right = node;
            node.Parent = leftSon;
            node.Left = null;
            node.Right = null;
        }

        private void ColorSwap(Node node)
        {
            node.Parent.Color = Colors.Black;
            node.Uncle.Color = Colors.Black;
            node.GrandFather.Color = Colors.Red;
        }

        public Node GetNode(int value)
        {
            var node = root;
            while (node != null)
            {
                if (node.Key == value)
                    return node;
                node = node.Key < value ? node.Right : node.Left;
            }
            return null;
        }

        public void FixNodesLeves()
        {
            if (root == null)
                return;
            root.Level = 1;
            var queue = new Queue<Node>();
            queue.Enqueue(root);
            while (queue.Count !=0)
            {
                var node = queue.Dequeue();
                if (node!=root)
                    node.Level = node.Parent.Level + 1;
                if (node.Left != null)
                    queue.Enqueue(node.Left);
                if (node.Right != null)
                    queue.Enqueue(node.Right);
            }
        }

        public void Print()
        {
            FixNodesLeves();
            if (root == null)
                return;
            var queue = new Queue<Node>();
            var level = root.Level;
            queue.Enqueue(root);
            while (queue.Count != 0)
            {
                var node = queue.Dequeue();
                if (node.Level > level)
                {
                    Console.WriteLine();
                    level++;
                }
                if (node.Color==Colors.Red)
                    Console.ForegroundColor= ConsoleColor.Red;
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
