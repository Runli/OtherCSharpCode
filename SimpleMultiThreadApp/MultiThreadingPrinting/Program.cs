using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MultiThreadingPrinting {
    class Printer {
        private object threadLock = new object();
        public void PrintNumbers() {
            lock (threadLock) {
                Console.WriteLine("-> {0} is executing PrintNumbers()", Thread.CurrentThread.Name);
                Console.WriteLine("Your numbers: ");
                for (int i = 0; i < 10; i++) {
                    Random random = new Random();
                    Thread.Sleep(500);
                    Console.Write("{0}, ", i);
                }
                Console.WriteLine();
            }
        }
    }
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("******Synchronizing Threads ****\n");
            Printer p = new Printer();

            Thread[] threads = new Thread[10];
            for (int i = 0; i < 10; i++) {
                threads[i] = new Thread(new ThreadStart(p.PrintNumbers));
                threads[i].Name = string.Format("Worker thread #{0}", i);
            }
            // теперь запусктим все эти 10 потоков
            foreach (Thread th in threads) {
                th.IsBackground = true;
                th.Start();
            }
            Console.ReadLine();
        }
    }
}
