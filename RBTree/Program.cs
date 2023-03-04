
namespace RBTree
{
    
    public class Program
    {
        static void Main()
        {
            var test1 = new Tree();
            //test1.Insert(2);
            //test1.Insert(1);
            //test1.Insert(3);
            test1.Insert(20);
            test1.Insert(25);
            test1.Insert(30);
            test1.Insert(35);
            test1.Insert(40);
            test1.Insert(6);
            test1.Insert(10);
            //test1.Insert(52);
            test1.Print();
            Console.WriteLine(test1.Deep);
            //Console.WriteLine(test1.GetNode(4).GrandFather.Key); 
        }
    }
}
