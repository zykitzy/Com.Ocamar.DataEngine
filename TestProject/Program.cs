using Com.Ocamar.DataEngine.Service.Watcher;
using System;

namespace TestProject
{
    class Program
    {

        static void Main(string[] args)
        {
            WatchService service = new WatchService();
            service.StartListen();

            while (true)
            {
                Console.ReadLine();
            }
        }


    }
}
