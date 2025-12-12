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
<<<<<<< Updated upstream
=======

        // Ruta del archivo CSV
        public string RutaArchivoCSV { get; private set; }
>>>>>>> Stashed changes

        public Config()
        {
            NucleosDisponibles = Environment.ProcessorCount;
<<<<<<< Updated upstream
            NucleosAUsar = NucleosDisponibles;
            TamanoLote = 1000;
=======
            NucleosAUsar = Math.Min(8, NucleosDisponibles);
            TamanoLote = 10000;
            RutaArchivoCSV = "datos_cacao_realistas.csv";
>>>>>>> Stashed changes

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
<<<<<<< Updated upstream
=======

        public void SetTamanoLote(int tamano)
        {
            TamanoLote = Math.Max(100, tamano);
        }
>>>>>>> Stashed changes
    }
}