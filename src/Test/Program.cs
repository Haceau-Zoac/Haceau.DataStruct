using System;

namespace Haceau.DataStruct.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Stack<int> stack = new Stack<int>();
            stack.Push(100);
            Console.WriteLine($"length: {stack.Count}");
            Console.WriteLine($"pop: {stack.Pop()}");
            Console.WriteLine($"length: {stack.Count}");
            stack.Count = 10;
            stack.Push(10);
            Console.WriteLine($"length: {stack.Count}");
            foreach (var item in stack)
                Console.WriteLine(item);
        }
    }
}