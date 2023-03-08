
namespace RBTree
{
    
    public class Program
    {
        static void Main()
        {
            var test1 = new Tree();
            test1.Insert(20);
            test1.Insert(25);
            test1.Insert(23);
            test1.Insert(30);
            test1.Insert(35);
            test1.Insert(40);
            test1.Insert(45);
            test1.Insert(33);
            test1.Insert(18);
            test1.Insert(34);
            test1.Insert(17);
            test1.Delete(23);
            test1.Delete(17);
            test1.Delete(30);
            test1.Print();
        }
    }
}
