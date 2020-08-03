using System;

namespace Haceau.DataStruct.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            DynamicArray<int> arr = new DynamicArray<int>();
            arr.Add(10);
            arr.Add(20);
            arr.Add(30);
            arr.Count = 1;
            foreach (var item in arr)
                Console.WriteLine(item);
        }
    }
}