
namespace RBTree
{
    
    public class Program
    {
        static void Main()
        {
            var test1 = new Tree();
            test1.Insert(2);
            test1.Insert(1);
            test1.Insert(3);
            test1.Insert(4);
            test1.Insert(5);
            test1.Insert(6);
            test1.Print();
            Console.WriteLine(test1.Deep);
            Console.WriteLine(test1.GetNode(4).GrandFather.Key); 
        }
    }
}
