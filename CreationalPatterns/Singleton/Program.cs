using System;

namespace Singleton
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger l1 = Logger.CreateLogger();
            Logger l2 = Logger.CreateLogger();
            Console.WriteLine(l1.TestValue);
            Console.WriteLine(l2.TestValue);

            if(l1==l2)
                Console.WriteLine("Same object");
        }
    }
}
