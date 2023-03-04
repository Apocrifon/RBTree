namespace RBTree
{
    public enum Colors
    {
        Red,
        Black
    }

    public class Node
    {
        public int Key { get; set; }
        public Colors Color { get; set; }
        public Node Parent { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
        public int Level { get; set; }  // Field create for print funk
        // поле дед и дядя мб нужно сделать методами, потому что какой-то доступ к ним сложный,
        // с точки зрения проектирования мне не нравится
        public Node GrandFather   

        {
            get
            {
                if (Parent != null)
                    return Parent.Parent;
                else
                    return null;
            }
        }
        public Node Uncle
        {
            get 
            {
                if (GrandFather != null)
                {
                    if (GrandFather.Left == this.Parent)
                        return GrandFather.Right;
                    else
                        return GrandFather.Left;
                }
                else
                    return null;
            }
        }

        public bool LeftConnected()
        {
            return Parent.Left == this;
        }

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
