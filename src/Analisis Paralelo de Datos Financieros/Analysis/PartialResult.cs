using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analisis_Paralelo_de_Datos_Financieros.Analysis
{
    public class PartialResult
    {
        // número de precios en el lote
        public int Count { get; set; }
        // suma de precios
        public double Sum { get; set; }
        // suma de precios^2
        public double SumSq { get; set; }
        public double Min { get; set; }
        public double Max { get; set; }

        // Para volatilidad (retornos)
        public int ReturnsCount { get; set; }
        public double SumReturns { get; set; }
        public double SumReturnsSq { get; set; }
    }
}
