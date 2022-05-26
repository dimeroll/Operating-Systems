using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab2OS
{
    class Program
    {
        private static int X ;
        static void Main(string[] args)
        {
            Console.WriteLine("Input x");
            X = int.Parse(Console.ReadLine());

            Task<bool> taskF = new Task<bool>(() => f(X));
            Task<bool> taskG = new Task<bool>(() => g(X));
            taskF.Start();
            taskG.Start();

            bool flag = true;
            while(flag)
            {
                Thread.Sleep(5000);
                Console.WriteLine("Print 1 (Stop) or 2 (Continue) or 3 (don't ask again) or 4 (change x)");
                int key = int.Parse(Console.ReadLine());
                switch (key)
                {
                    case 1:
                        flag = false;
                        break;
                    case 2:
                        if (taskF.IsCompleted && taskG.IsCompleted)
                        {
                            Console.WriteLine("Result of f && g : " + (taskF.Result && taskG.Result).ToString());
                            flag = false;
                        }
                        break;

                    case 3:
                        Console.WriteLine("Result of f && g : " + (taskF.Result && taskG.Result).ToString());
                        flag = false;
                        break;
                    case 4:
                        Console.WriteLine("Input new value of x");
                        X = int.Parse(Console.ReadLine());
                        goto case 3;
                }    
            }
            Console.WriteLine("End of calculations");
            Console.ReadKey();
        }

        static bool f(int x)
        {
            bool res =  x * (x - 20) > 0;
            Thread.Sleep(10000);
            if (x != X)
                return false;
            return res;
        }

        static bool g(int x)
        {
            bool res =  x * (2*x - 50) <= 0;
            Thread.Sleep(10000);
            if (x != X)
                return false;
            return res;
        }
    }
}
