using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analisis_Paralelo_de_Datos_Financieros.Analysis
{
    public class ResultCombiner
    {
        // Combina los resultados parciales y genera un resumen estadístico
        public CombinedResult CombinarResultados(List<double> resultadosLotes)
        {
            if (resultadosLotes == null || resultadosLotes.Count == 0)
                return new CombinedResult();

            var result = new CombinedResult
            {
                Promedio = Statistics.Mean(resultadosLotes),
                Varianza = Statistics.Variance(resultadosLotes),
                DesviacionEstandar = Statistics.StdDev(resultadosLotes),
                Minimo = Statistics.Min(resultadosLotes),
                Maximo = Statistics.Max(resultadosLotes),
                Volatilidad = Statistics.Volatility(resultadosLotes)
            };

            return result;
        }
    }

    // Clase para guardar el resultado final combinado
    public class CombinedResult
    {
        public double Promedio { get; set; }
        public double Varianza { get; set; }
        public double DesviacionEstandar { get; set; }
        public double Minimo { get; set; }
        public double Maximo { get; set; }
        public double Volatilidad { get; set; }
    }
}
