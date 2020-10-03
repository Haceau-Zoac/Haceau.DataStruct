using System;

namespace Haceau.DataStruct.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Queue<int> qwq = new Queue<int>();
            qwq.Enqueue(12);
            qwq.Enqueue(123);
            qwq.Enqueue(7474);
            foreach (var item in qwq)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("---");

            Console.WriteLine(qwq.Dequeue());
            Console.WriteLine(qwq.Dequeue());
            Console.WriteLine(qwq.Dequeue());
            try
            {
                Console.WriteLine(qwq.Dequeue());
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("message: {0}", e.Message);
            }
            qwq.Enqueue(253);
            qwq.Enqueue(999);
            Console.WriteLine(qwq.Peek());
            Console.WriteLine(qwq.Count);
            qwq.Remove(253);
            Console.WriteLine(qwq.Count);
            if (qwq.TryDequeue(out int result))
                Console.WriteLine(result);
            else
                Console.WriteLine("ERROR.");
            if (qwq.TryPeek(out result))
                Console.WriteLine(result);
            else
                Console.WriteLine("ERROR.");
        }
    }
}