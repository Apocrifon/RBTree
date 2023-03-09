
namespace RBTree
{
    
    public class Program
    {
        static void Main()
        {
            var test1 = new Tree();
            for (int i = 5; i <= 60; i+=5)
            {
                test1.Insert(i);
            }
            test1.Print();
            Console.WriteLine();
            test1.Delete(30);
            test1.Delete(20);
            test1.Insert(13);
            test1.Insert(27);
            test1.Insert(70);
            test1.Print();
        }
    }
}
