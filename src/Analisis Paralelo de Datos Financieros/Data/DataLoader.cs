using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Analisis_Paralelo_de_Datos_Financieros.Data
{
    public class DataLoader
    {
        /// <summary>
        /// Carga precios desde un archivo CSV con formato: Fecha[TAB]Precio
        /// </summary>
        public List<double> CargarPrecios(string rutaArchivo)
        {
            List<double> precios = new List<double>();

            if (!File.Exists(rutaArchivo))
            {
                Console.WriteLine("ERROR: No se encontro el archivo: " + rutaArchivo);
                return precios;
            }

            try
            {
                Console.WriteLine("Cargando datos desde: " + rutaArchivo);
                string[] lineas = File.ReadAllLines(rutaArchivo);
                int datosCargados = 0;
                int datosOmitidos = 0;

                for (int i = 1; i < lineas.Length; i++) // Saltar encabezado
                {
                    string linea = lineas[i].Trim();

                    if (string.IsNullOrEmpty(linea))
                        continue;

                    // Separar por TAB
                    string[] columnas = linea.Split('\t');

                    if (columnas.Length < 2)
                    {
                        datosOmitidos++;
                        continue;
                    }

                    string precioTexto = columnas[1].Trim();

                    // Limpiar el precio
                    precioTexto = precioTexto.Replace("$", "")
                                             .Replace(",", "")
                                             .Replace("\"", "")
                                             .Replace(" ", "");

                    if (string.IsNullOrEmpty(precioTexto) ||
                        precioTexto == "0" ||
                        precioTexto == "-" ||
                        precioTexto.ToLower() == "n/a")
                    {
                        datosOmitidos++;
                        continue;
                    }

                    if (double.TryParse(precioTexto,
                        NumberStyles.Any,
                        CultureInfo.InvariantCulture,
                        out double precio))
                    {
                        precios.Add(precio);
                        datosCargados++;
                    }
                    else
                    {
                        datosOmitidos++;
                    }
                }

                Console.WriteLine("  Datos cargados: " + datosCargados);
                Console.WriteLine("  Datos omitidos: " + datosOmitidos);
                return precios;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR al leer el archivo: " + ex.Message);
                return precios;
            }
        }

        // Genera datos de prueba si no hay archivo CSV
        public List<double> GenerarPrecios(int cantidad)
        {
            Console.WriteLine("Generando " + cantidad + " datos de prueba...");

            var precios = new List<double>(cantidad);
            var random = new Random();
            double precioActual = 1000.0;
            double volatilidad = 0.02;
            double tendencia = 0.0005;
            double maximo = 10000.0;

            for (int i = 0; i < cantidad; i++)
            {
                double cambio = tendencia + volatilidad * (random.NextDouble() * 2 - 1);
                precioActual *= Math.Exp(cambio);

                if (precioActual > maximo)
                    precioActual = maximo;

                precios.Add(Math.Round(precioActual, 2));
            }

            Console.WriteLine("Datos generados: " + precios.Count);
            return precios;
        }
    }
}