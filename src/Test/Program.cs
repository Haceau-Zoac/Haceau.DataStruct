using System;

namespace Haceau.DataStruct.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            LinkedList<int> linkedList = new LinkedList<int>();
            linkedList.Insert(0, 123);
            Console.WriteLine(linkedList[1]);
            Console.WriteLine("Length: {0}", linkedList.Length);
            linkedList.RemoveEnd();
            linkedList[0] = 233;
            Console.WriteLine(linkedList[0]);
            Console.WriteLine("Length: {0}", linkedList.Length);
            linkedList.Append(999);
            linkedList.RemoveStart();
            if (linkedList.Search(999, out ulong index))
                Console.WriteLine(linkedList[index]);
            Console.WriteLine("Length: {0}", linkedList.Length);
        }
    }
}