using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcesadorConsole
{
    public class DetalleEstructura
    {
        public DetalleEstructura(string campo, int posicionInicial, int posicionFinal)
        {
            Campo = campo;
            PosicionInicial = posicionInicial;
            PosicionFinal = posicionFinal;
        }

        public string Campo { get; set; }

        public int PosicionInicial { get; set; }

        public int PosicionFinal { get; set; }
    }
}
