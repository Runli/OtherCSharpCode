using System;
using System.Linq;
using System.Diagnostics;

namespace ConsoleApplication1 {
    class Program {
        static void Main() {
            Console.WriteLine("*** ПРОЦЕССЫ ***\n");
            string comm = Command();
            UseMyCommand(comm);
            // Используем рекурсию для вызова новых команд
            if (comm != "X")
                Main();
        }

        static string Command() {
            Console.WriteLine("Какую информацию нужно получить? \n" +
                " 1 - Список всех процессов\n 2 - Выбрать процесс по PID\n" +
                " 3 - Информация о потоках\n 4 - Информация о подключаемых модулях\n" +
                " 5 - Запуск процесса\n 6 - Останов процесса\n X - выход\n");
            Console.Write("Введите команду: ");
            string comm = Console.ReadLine();
            return comm;
        }

        static void UseMyCommand(string str) {
            switch (str) {
                case "X":
                    break;
                case "1":
                    AllInfoProcess();
                    break;
                case "2":
                    ProcInMyPid();
                    break;
                case "3":
                    Threads();
                    break;
                case "4":
                    InfoByModuleProc();
                    break;
                case "5":
                    StartProcess();
                    break;
                case "6":
                    StopProcess();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Команда не распознана!");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
            }
            Console.WriteLine();
        }

        static void AllInfoProcess() {
            var myProcess = from proc in Process.GetProcesses(".")
                            orderby proc.Id
                            select proc;
            Console.WriteLine("\n*** Текущие процессы ***\n");
            foreach (var p in myProcess)
                Console.WriteLine("-> PID: {0}\tName: {1}", p.Id, p.ProcessName);
        }

        static void ProcInMyPid() {
            Console.Write("Введите PID-идентификатор: ");
            string pid = Console.ReadLine();
            Process myProc = null;
            try {
                int i = int.Parse(pid);
                myProc = Process.GetProcessById(i);
                Console.WriteLine("\n-> PID: {0}\tName: {1}\n", myProc.Id, myProc.ProcessName);
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        static void Threads() {
            Console.Write("Введите PID-идентификатор: ");
            string pid = Console.ReadLine();
            Process myProc = null;
            try {
                int i = int.Parse(pid);
                myProc = Process.GetProcessById(i);
                // Получаем коллекцию потоков процесса
                ProcessThreadCollection threads = myProc.Threads;
                Console.WriteLine("Потоки процесса {0}:\n", myProc.ProcessName);
                foreach (ProcessThread pt in threads)
                    Console.WriteLine("-> Thread ID: {0}\tВремя: {1}\tПриоритет: {2}"
                        , pt.Id, pt.StartTime.ToShortTimeString(), pt.PriorityLevel);
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            } finally {
                Console.WriteLine();
            }
        }

        static void InfoByModuleProc() {
            Console.Write("Введите PID-идентификатор: ");
            string pid = Console.ReadLine();
            Process myProc = null;
            try {
                int i = int.Parse(pid);
                myProc = Process.GetProcessById(i);
                Console.WriteLine("Подключаемые модули процесса {0}:\n", myProc.ProcessName);
                // Получаем коллекцию модулей
                ProcessModuleCollection mods = myProc.Modules;
                foreach (ProcessModule pm in mods)
                    Console.WriteLine("-> Имя модуля: " + pm.ModuleName);
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            } finally {
                Console.WriteLine();
            }
        }

        static void StartProcess() {
            Process myProc = null;
            // Запустить сайт professorweb.ru на машине пользователя
            try {
                // Будет работать только для пользавателей у которых установлен браузер Google Chrome
                // Чтобы запустить IE, нужно запустить процесс IExplore.exe
                myProc = Process.Start("chrome.exe", "www.professorweb.ru");
            } catch (Exception ex) {
                Console.WriteLine(ex);
            }
        }

        static void StopProcess() {
            Console.Write("Введите PID-идентификатор процесса, который нужно остановить: ");
            string pid = Console.ReadLine();
            Process myProc = null;
            try {
                int i = int.Parse(pid);
                myProc = Process.GetProcessById(i);
                myProc.Kill();
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
