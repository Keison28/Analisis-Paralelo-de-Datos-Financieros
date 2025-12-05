using System;
using System.Threading.Tasks;

namespace Analisis_Paralelo_de_Datos_Financieros
{
    public class Config
    {
        // Información del hardware
        public int NucleosDisponibles { get; private set; }
        public int NucleosAUsar { get; private set; }

        // Tamaño de los lotes del dataset financiero
        public int TamanoLote { get; private set; }

        // Opciones generales de paralelismo
        public ParallelOptions Options { get; private set; }

        public Config()
        {
            NucleosDisponibles = Environment.ProcessorCount;
            NucleosAUsar = NucleosDisponibles;
            TamanoLote = 1000;

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

        public void SetTamanoLote(int tam)
        {
            TamanoLote = Math.Max(1, tam);
        }
    }
}