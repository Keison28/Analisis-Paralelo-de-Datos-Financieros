using System;
using System.Threading.Tasks;

namespace Analisis_Paralelo_de_Datos_Financieros
{
    public class Config
    {
        // Información del hardware
        public int NucleosDisponibles { get; private set; }
        public int NucleosAUsar { get; private set; }

        // Tamaño de los lotes
        public int TamanoLote { get; private set; }

        // Opciones de paralelismo
        public ParallelOptions Options { get; private set; }

        // Ruta del archivo CSV
        public string RutaArchivoCSV { get; private set; }

        public Config()
        {
            NucleosDisponibles = Environment.ProcessorCount;
            NucleosAUsar = Math.Min(8, NucleosDisponibles); // Máximo 8 para evitar sobrecarga
            TamanoLote = 10000; // Tamaño fijo para simplicidad
            RutaArchivoCSV = "datos_financieros.csv"; // Nombre por defecto

            Options = new ParallelOptions
            {
                MaxDegreeOfParallelism = NucleosAUsar
            };
        }

        public void SetNucleos(int n)
        {
            if (n < 1) n = 1;
            if (n > NucleosDisponibles) n = NucleosDisponibles;

            NucleosAUsar = n;
            Options.MaxDegreeOfParallelism = n;
        }

        public void SetRutaArchivo(string ruta)
        {
            RutaArchivoCSV = ruta;
        }

        public void SetTamanoLote(int tamano)
        {
            TamanoLote = Math.Max(100, tamano);
        }
    }
}