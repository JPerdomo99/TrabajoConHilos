using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcesadorConsole
{
    public class Ejecutor
    {
        // COD_CLIENTE PI: 1, PF: 2
        // COD_PROD PI: 17, PF: 18
        // NUM_CUENTA: PI: 31, PF: 39
        // NUM_CHEQUE: PI: 46, PF: 51
        // COD_SEG: PI: 61 PF: 66
        public void ProcesarArchivo()
        {
            DetalleEstructura detalle1 = new DetalleEstructura("COD_CLIENTE", 1, 2);
            DetalleEstructura detalle2 = new DetalleEstructura("COD_PROD", 11, 13);
            DetalleEstructura detalle3 = new DetalleEstructura("NUM_CUENTA", 20, 28);
            DetalleEstructura detalle4 = new DetalleEstructura("NUM_CHEQUE", 32, 37);
            DetalleEstructura detalle5 = new DetalleEstructura("COD_SEG", 41, 46);

            string path = @"C:\Users\ateho\Desktop\ArchivoPruebaCargueBase.txt";
            #region PrimerFichero
            string pathRE = $@"C:\Users\ateho\Desktop\RE_{ConstruirFechaHora()}.txt";
            string pathPR = $@"C:\Users\ateho\Desktop\PR_{ConstruirFechaHora()}.txt";
            string[] lineasArchivo = File.ReadAllLines(path);
            FileStream respuesta = File.Create(pathRE);
            FileStream produccion = File.Create(pathPR);

            UTF8Encoding utf8 = new UTF8Encoding();

            foreach (var linea in lineasArchivo)
            {
                char[] caracteres = linea.ToCharArray();
                string codigoCliente = ConstruirString(caracteres, detalle1);
                string codigoProducto = ConstruirString(caracteres, detalle2);
                string numeroCuenta = ConstruirString(caracteres, detalle3);
                string numeroCheque = ConstruirString(caracteres, detalle4);
                string codSeguridad = ConstruirString(caracteres, detalle5);
                string codigoSeguridadCalculado = ConstruirCodigoSeguridadCalculado();

                Console.WriteLine($"{codigoCliente};{numeroCuenta};{numeroCheque};{codSeguridad};{codigoSeguridadCalculado}");
                string informacionLinea = $"{codigoCliente};{numeroCuenta};{numeroCheque};{codSeguridad};{codigoSeguridadCalculado}\r\n";
                byte[] informacion = utf8.GetBytes(informacionLinea);
                try
                {
                    respuesta.Write(informacion, 0, informacion.Length);
                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine($"El contenido de la información a escribir es nula {e}");
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine($"Linea fuera de rango {e}");
                }
                catch (IOException e)
                {
                    Console.WriteLine($"Error de entrada y salida {e}");
                }
                catch (NotSupportedException e)
                {
                    Console.WriteLine($"Escritura no permitida {e}");
                }           
            }
            #endregion

            Console.WriteLine("segundo fichero---------------------------------------------------------------------");

            #region SegundoFichero
            string nombreCliente = "Carmen";
            string direccionCliente = "Calle 27 # 7 - 68";
            string descripcionProducto = "Alcohol 70º 100ml";
            string codigoProductoDefinido = "510";
            string[] productosEstructura = { "510", "610" };
            string FechaGeneracion = DateTime.Now.ToShortDateString();
            foreach (var linea in lineasArchivo)
            {
                char[] caracteres = linea.ToCharArray();
                string codigoProducto = ConstruirString(caracteres, detalle2);

                bool existe = false;
                foreach (var producto in productosEstructura)
                {
                    if (existe == false)
                        if (codigoProducto == producto)
                            existe = true;            
                }

                if (!existe)
                {
                    Console.WriteLine("Codigo de producto no coincide con la configuración definida");
                }
                else
                {
                    string codigoCliente = ConstruirString(caracteres, detalle1);
                    string numeroCuenta = ConstruirString(caracteres, detalle3);
                    string numeroCheque = ConstruirString(caracteres, detalle4);
                    string codSeguridad = ConstruirString(caracteres, detalle5);
                    string codigoSeguridadCalculado = ConstruirCodigoSeguridadCalculado();

                    Console.WriteLine($"{codigoCliente};{nombreCliente};{direccionCliente};{codigoProducto};{descripcionProducto};{numeroCuenta};{numeroCheque};{codSeguridad};{codigoSeguridadCalculado};{FechaGeneracion}");
                    string informacionLinea = $"{codigoCliente};{nombreCliente};{direccionCliente};{codigoProducto};{descripcionProducto};{numeroCuenta};{numeroCheque};{codSeguridad};{codigoSeguridadCalculado};{FechaGeneracion}\r\n";
                    byte[] informacion = utf8.GetBytes(informacionLinea);
                    try
                    {
                        produccion.Write(informacion, 0, informacion.Length);
                    }
                    catch (ArgumentNullException e)
                    {
                        Console.WriteLine($"El contenido de la información a escribir es nula {e}");
                    }
                    catch (ArgumentException e)
                    {
                        Console.WriteLine($"Linea fuera de rango {e}");
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine($"Error de entrada y salida {e}");
                    }
                    catch (NotSupportedException e)
                    {
                        Console.WriteLine($"Escritura no permitida {e}");
                    }
                }
            }
            #endregion
        }

        private string ConstruirString(char [] caracteres, DetalleEstructura detalleEstructura)
        {
            string detalle = string.Empty;
            for (int i = detalleEstructura.PosicionInicial-1; i <= detalleEstructura.PosicionFinal-1; i++)
            {
                detalle += caracteres[i];
            }               

            return detalle;
        }

        private string ConstruirCodigoSeguridadCalculado()
        {
            Random rand = new Random();
            int aleatorio = rand.Next(111111, 999999);
            string codigoSeguridadCalculado = $"{aleatorio}{ConstruirFechaHora()}";
            
            return codigoSeguridadCalculado;
        }

        private string ConstruirFechaHora()
        {
            DateTime fecha = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0);
            string fechaFormato = $"{fecha:ddMMyyHHmm}";

            return fechaFormato;
        }
    }
}

// COD_CLIENT     COD_PROD       NUM_CUENT      NUM_CHEQ       COD_SEG
// 01             10             010100001      000001         535231
// 01             10             010100001      000002         732295    
// Codigo cliente; numero cuenta; numero cheque; cod seg; codsegcalculado 

// Codigo cliente;Nombre Cliente;Direccion cliente;Codigo Producto;Descripcion Producto;Numero cuenta;Numero Cheque;Cod Seg;Cod SegCalc;Fecha generacion
