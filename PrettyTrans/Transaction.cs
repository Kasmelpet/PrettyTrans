using System;

namespace PrettyTrans
{
    class Transaction
    {
        // Transaction's field lenghts.
        readonly int[] trans01Array = new int[] { 4, 2, 13, 4, 35, 2, 9, 14 };
        readonly int[] trans10Array = new int[] { 4, 2, 13, 4, 3, 3, 4, 10, 2, 10, 1, 8, 3, 1, 10, 1, 8, 10, 10, 10, 1, 8, 8, 9, 9, 9, 36, 36 };
        readonly int[] trans14Array = new int[] { 4, 2, 13, 4, 3, 3, 4, 10, 2, 10, 10, 2, 1 };
        readonly int[] trans24Array = new int[] { 4, 2, 13, 4, 3, 3, 4, 10, 2, 2, 3, 11, 1, 11, 1, 8, 8, 8, 8, 3, 3, 1, 35, 8, 8, 8, 4, 100, 35, 17, 35, 8, 35, 35, 10, 14, 2, 5, 10, 3, 12 };
        readonly int[] trans26Array = new int[] { 4, 2, 13, 4, 3, 3, 4, 10, 2, 2, 3, 3, 60 };
        readonly int[] trans51Array = new int[] { 4, 2, 13, 4, 3, 3, 4, 10, 2, 10, 1, 10 };
        readonly int[] trans52Array = new int[] { 4, 2, 13, 4, 3, 3, 4, 10, 2, 13, 1 };
        readonly int[] trans61Array = new int[] { 4, 2, 13, 4, 3, 3, 4, 10, 2, 10, 1, 1, 32, 32, 32, 4, 32 };
        readonly int[] trans71Array = new int[] { 4, 2, 13, 4, 3, 3, 4, 10, 2, 1, 2, 70 };

        // Temp files with transactions field names.
        string transxx = "transxx.tmp";
        private string transline;
        private int cursor;
        private string transtype;

        public Transaction(string transline)
        {
            this.transline = transline;
            cursor = -1;
        }

        public int GetCursor()
        {
            if (transline.Contains("BYGS"))
                cursor = transline.IndexOf("BYGS");
            if (transline.Contains("MADS"))
                cursor = transline.IndexOf("MADS");
            
            return cursor;
        }

        public string GetTransType()
        {
            // Try/catch if the transline is too short or something.
            try
            {
                transtype = transline.Substring(cursor + 4, 2);
                return transtype;
            }
            catch
            {
                return "00";
            }

        }
        internal void WriteTransaction()
        {
            int[] useArray;
            switch(transtype)
            {
                case "01":
                    useArray = trans01Array;
                    break;
                case "10":
                    useArray = trans10Array;
                    break;
                case "14":
                    useArray = trans14Array;
                    break;
                case "24":
                    useArray = trans24Array;
                    break;
                case "26":
                    useArray = trans26Array;
                    break;
                case "51":
                    useArray = trans51Array;
                    break;
                case "52":
                    useArray = trans52Array;
                    break;
                case "61":
                    useArray = trans61Array;
                    break;
                case "71":
                    useArray = trans71Array;
                    break;
                default:
                    Console.WriteLine($"Unknown transaction type.");
                    return;
            }

            transxx = transxx.Replace("xx", transtype);
            string[] tmpFile = System.IO.File.ReadAllLines($"Docs/{transxx}");

            int cntArray = 0;
            foreach (string line in tmpFile)
            {
                try
                {
                    Console.WriteLine($"{line} {transline.Substring(cursor, useArray[cntArray])}");
                }
                catch
                {
                    Console.WriteLine($"{line} {transline.Substring(cursor)}");
                    Console.WriteLine("Could not complete the parsing...");
                    return;
                }
                cursor = cursor + useArray[cntArray];
                cntArray++;
            }
        }
    }
}
