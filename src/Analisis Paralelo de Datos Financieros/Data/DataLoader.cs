using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Analisis_Paralelo_de_Datos_Financieros.Data
{
    public class DataLoader
    {
        // Lee un archivo CSV separado por tabs y extrae la columna de precios
        public List<double> CargarPrecios(string rutaArchivo)
        {
            List<double> precios = new List<double>();

            if (!File.Exists(rutaArchivo))
                return precios;

            string[] lineas = File.ReadAllLines(rutaArchivo);

            // Inicia desde 1 para saltar la fila de encabezados
            for (int i = 1; i < lineas.Length; i++)
            {
                string linea = lineas[i].Trim();

                if (string.IsNullOrWhiteSpace(linea))
                    continue;

                // El CSV viene separado por TAB
                string[] columnas = linea.Split('\t');

                // La segunda columna (indice 1) es: "Precio de Bolsa US$/T.M."
                if (columnas.Length < 2)
                    continue;

                string precioTexto = columnas[1];

                // Evita filas con ceros (datos basura en el dataset)
                if (precioTexto == "0")
                    continue;

                if (double.TryParse(precioTexto, NumberStyles.Any, CultureInfo.InvariantCulture, out double precio))
                {
                    precios.Add(precio);
                }
            }

            return precios;
        }
    }
}