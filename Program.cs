using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcesadorConsole
{
    public class Program
    { 
        public static void Main()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            Ejecutor oEjecutor = new Ejecutor();
            oEjecutor.ProcesarArchivo();
            stopWatch.Stop();
            TimeSpan tiempoTranscurrido = stopWatch.Elapsed;
            Console.WriteLine($"Segundos {tiempoTranscurrido.Seconds} - Milisegundos {tiempoTranscurrido.Milliseconds}");

            Console.ReadKey();
        }
    }
}
