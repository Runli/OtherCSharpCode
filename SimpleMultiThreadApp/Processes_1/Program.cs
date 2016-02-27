using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Processes_1 {
    class Program {
        static void Main(string[] args) {
            // Используем LINQ-запрос для получения
            // списка всех используемых процессов
            var allProcess = from pr in Process.GetProcesses()
                             orderby pr.Id
                             select pr;
            int i = 0;
            foreach (var proc in allProcess) {
                i++;
                string infoproc = string.Format(@"-->{0}: 
    PID:    {1}
    Name:   {2}
", i, proc.Id, proc.ProcessName);
                Console.Write(infoproc);
            }
            Console.ReadLine();
        }
    }
}
