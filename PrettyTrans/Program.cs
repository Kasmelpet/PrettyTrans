using System;

namespace PrettyTrans
{
    class Program
    {
        
        static void Main(string[] args)
        {
            string transline;
            Console.WriteLine("Gimme ya transline:");
            transline = Console.ReadLine();

            Transaction transaction = new Transaction(transline);

            int cursor = transaction.GetCursor();
            if (cursor == -1)
            {
                Console.WriteLine($"Not a BYGS/MADS transaction...");
                return;
            }

            string transtype;
            transtype = transaction.GetTransType();
            Console.WriteLine($"Transaction type: {transtype}");

            transaction.WriteTransaction();

            Console.ReadKey();
        }

    }
}
