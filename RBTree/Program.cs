using System;

namespace RBTree
{
    public enum Colors
    {
        Red,
        Black
    }

    public class Tree
    {
        public Node root = null;
        public Node Root { get; set; }

        public void Insert(Node node)
        {
            var curNode = root;
            while (curNode!=null)
                curNode = curNode.Key > node.Key ? curNode.Right : curNode.Left;
            curNode = new Node(node.Key);
            Balanced();
        }

        public void Balanced()
        {

        }




        // идея вывода дерева через очередь(Ulearn)
    }

    public class Node   
    {
        public int Key { get; set; }
        public Colors Color { get; set; }
        public Node Parent { get; set;}
        public Node Left { get; set; }
        public Node Right { get; set; }

        public Node(int key)
        {
            Key = key;
            Color = Colors.Red;
            Parent = null;
            Left = null;
            Right = null;
        }    
    } 

    public class Program
    {
        static void Main()
        {
            var test1 = new Tree();
        }
    }
}
