using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp45
{
    class Program
    {
        static void Main(string[] args)
        {
            CashManager cm = new CashManager(5, 5);
            int num = 0;
            string obj = "sent";
            do
            {
                obj = obj + num;
                num++;


            } while (cm.Add(num, obj));

            num = 0;
            object obj2 = new object();
            do
            {
                GC.Collect();
                num++;
                if (obj2 != null) Console.WriteLine(obj2.ToString());


            } while (cm.GetAndRemove(num, out obj2));

        }
    }
}
