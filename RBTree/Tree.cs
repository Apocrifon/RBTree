using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

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
            var p = FindNode(value);
            var deleteNode = p;
            Colors originalColor=p.Color;
            if (p.Left == null)
            {
                deleteNode = p.Right;
                Transplant(p, deleteNode);
            }
            else
            {
                if (p.Right == null)
                {
                    deleteNode = p.Left;
                    Transplant(p, deleteNode);
                }
                else
                {
                    deleteNode = MinNode(p.Right);
                    originalColor = deleteNode.Color;
                    if (deleteNode.Right!=null)
                        deleteNode.Right.Parent = deleteNode.Parent;
                    if (deleteNode == root)
                        root = deleteNode.Right;
                    else
                    {
                        if (deleteNode.LeftConnected())
                            deleteNode.Parent.Left = deleteNode.Right;
                        else
                            deleteNode.Parent.Right = deleteNode.Right;
                    }
                    if (deleteNode != p)
                        p.Key = deleteNode.Key;
                }
            }
            if (originalColor == Colors.Black)
                DeleteBalanced(deleteNode);

        }

        private void DeleteBalanced(Node node)
        {
            while (node !=root &&  node.Color==Colors.Black)
            {
                Node brother = null;
                if (node.Key < node.Parent.Key) // не будет работать 
                {
                    brother = node.Parent.Right;
                    if (brother.Color == Colors.Red)
                    {
                        brother.Color = Colors.Black;
                        node.Parent.Color = Colors.Red;
                        LeftRotate(node.Parent);
                        brother = node.Parent.Right;
                    }
                    if ((brother.Left==null || brother.Left.Color==Colors.Black) && (brother.Right==null || brother.Right.Color==Colors.Black))
                    {
                        brother.Color = Colors.Red;
                        node = node.Parent;
                    }
                    else
                    {
                        if (brother.Right.Color== Colors.Black)
                        {
                            brother.Left.Color = Colors.Black; //
                            brother.Color = Colors.Red;
                            RightRotate(brother);
                            brother = node.Parent.Right;
                        }
                        brother.Color=node.Parent.Color;
                        node.Parent.Color = Colors.Black;
                        brother.Right.Color = Colors.Black;
                        LeftRotate(node.Parent);
                        node = root;
                    }
                }
                else
                {
                    brother = node.Parent.Left;
                    if (brother.Color == Colors.Red)
                    {
                        brother.Color = Colors.Black;
                        node.Parent.Color = Colors.Red;
                        RightRotate(node.Parent);
                        brother = node.Parent.Left;
                    }
                    if ((brother.Left == null || brother.Left.Color == Colors.Black) && (brother.Right == null || brother.Right.Color == Colors.Black))
                    {
                        brother.Color = Colors.Red;
                        node = node.Parent;
                    }
                    else
                    {
                        if (brother.Left.Color == Colors.Black)
                        {
                            brother.Right.Color = Colors.Black;
                            brother.Color = Colors.Red;
                            LeftRotate(brother);
                            brother = node.Parent.Left;
                        }
                        brother.Color = node.Parent.Color;
                        node.Parent.Color = Colors.Black;
                        brother.Left.Color = Colors.Black;
                        RightRotate(node.Parent);
                        node = root;
                    }
                }             
            }
            node.Color = Colors.Black;
        }

        private void Transplant(Node prevNode, Node newNode)
        {
            newNode.Color = prevNode.Color;
            if (prevNode == null)
                root = newNode;
            else
            {
                if (prevNode.LeftConnected())
                    prevNode.Parent.Left = newNode;
                else
                    prevNode.Parent.Right = newNode;
            }
            if (newNode!=null)
                newNode.Parent = prevNode.Parent;
        }

        private Node MinNode(Node node)
        {
            var p = node;
            while (p.Left!=null)
                p = p.Left;
            return p;
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

        private void LeftRotate(Node node)
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

        private void RightRotate(Node node)
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

        private Node GetNode(int value)
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

        private void FixNodesLeves()
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
