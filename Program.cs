using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProcesadorConsole
{
    public class Program
    { 
        public static void Main()
        {
            Ejecutor oEjecutor = new Ejecutor();

            Thread hilo1 = new Thread(oEjecutor.ProcesarArchivo1);
            hilo1.Start();
            Thread hilo2 = new Thread(oEjecutor.ProcesarArchivo2);
            hilo2.Start();

            Console.ReadKey();
        }
    }
}
