using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab4OS
{
    class Program
    {
        static int n, m, k;
        static int[,] C;
        static Random random = new Random();
        static void Main(string[] args)
        {
            Lab4_23.Main1();

            Console.WriteLine("Input n m k");
            n = int.Parse(Console.ReadLine());
            m = int.Parse(Console.ReadLine());
            k = int.Parse(Console.ReadLine());

            int[,] A = GenerateMatrix(n, m);
            int[,] B = GenerateMatrix(m, k);
            C = new int[n, k];
            OutputMatrix(n, m, A);
            OutputMatrix(m, k, B);

            List<Task> tasks = new List<Task>();
            for(int i = 0; i < n; i++)
            {
                int[] a = new int[m];
                for (int y = 0; y < m; y++)
                    a[y] = A[i,y];

                for (int j = 0; j < k; j++)
                {
                    int[] b = new int[m];
                    for (int y = 0; y < m; y++)
                        b[y] = B[y, j];

                    int[] a1 = new int[m];
                    int[] b1 = new int[m];
                    a.CopyTo(a1, 0);
                    b.CopyTo(b1, 0);
                    int i1 = i;
                    int j1 = j;

                    tasks.Add(new Task(() => VectorMultiplication(a1, b1, i1, j1)));
                }
            }

            foreach (Task task in tasks)
                task.Start();

            Task.WaitAll(tasks.ToArray());

            Console.WriteLine();
            OutputMatrix(n, k, C);
            Console.ReadKey();
        }

        static int[,] GenerateMatrix(int n, int m)
        {
            int[,] A = new int[n, m];

            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                {
                    A[i, j] = random.Next(20) - 10;
                }

            return A;
        }

        static void OutputMatrix(int n1, int m1, int[,] A)
        {
            for (int i = 0; i < n1; i++)
            {
                for (int j = 0; j < m1; ++j)
                    Console.Write(A[i, j] + " ");
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static void VectorMultiplication(int[] a, int[] b, int i, int j)
        {
            int res = 0;
            for (int y = 0; y < a.Length; ++y)
                res += a[y] * b[y];

            C[i, j] = res;
            Console.WriteLine($"C[{i}, {j}] = " + C[i, j]);
        }
    }

    static class Lab4_23
    {
        public static int[] array = new int[3];

        private static void Increase1()
        {
            for (int i = 0; i < 500000; ++i)
                lock (array)
                {
                    array[1] = array[1] + 1;
                    array[1] = array[1] + 1;
                }

            lock (array)
                Console.WriteLine(array[1]);
        }

        private static void Increase2()
        {
            for (int i = 0; i < 500000; ++i)
                lock (array)
                { 
                    array[1] = array[1] + 1;
                    array[1] = array[1] + 1;
                }

            lock (array)
                Console.WriteLine(array[1]);
        }

        public static void Main1()
        {
            Task task1 = new Task(Increase1);
            Task task2 = new Task(Increase2);

            task1.Start();
            task2.Start();

            task1.Wait();
            task2.Wait();
            Console.WriteLine(array[1]);
        }
    }
}
