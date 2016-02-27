using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace AddWithThreads {
    class AddParams {
        public int a, b;
        public AddParams(int numb1, int numb2) {
            a = numb1;
            b = numb2;
        }
    }
    class Program {
        private static AutoResetEvent waitHndle = new AutoResetEvent(false);
        static void Add(object data) {
            if (data  is AddParams) {
                Console.WriteLine("ID of threads in Add(): {0}", Thread.CurrentThread.ManagedThreadId);
                AddParams ap = (AddParams)data;
                Console.WriteLine("{0} + {1} is {2}", ap.a, ap.b, ap.a + ap.b);
                Thread.Sleep(2000);
                waitHndle.Set();
            }
        }
        static void Main(string[] args) {
            Console.WriteLine("****ADding with Threads objects****\n");
            Console.WriteLine("ID of thread in Main(): {0}", Thread.CurrentThread.ManagedThreadId);

            AddParams addPar = new AddParams(10, 23);
            Thread thread = new Thread(new ParameterizedThreadStart(Add));
            thread.Start(addPar);
            Console.WriteLine("Waiting second thread!");
            //нужно подождать
            waitHndle.WaitOne();

            Console.WriteLine("Wait is done!");

            Thread.Sleep(5000);
            Console.WriteLine("To be continued....");
            Console.ReadLine();
        }
    }
}
