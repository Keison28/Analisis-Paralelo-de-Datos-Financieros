using System;
using System.Collections.Generic;
using System.Linq;

namespace Analisis_Paralelo_de_Datos_Financieros.Analysis
{
<<<<<<< Updated upstream
    internal class SequentialAnalysis
    {
        public class AnalysisResult
        {
            public double Mean { get; set; }
            public double StandardDeviation { get; set; }
            public double Min { get; set; }
            public double Max { get; set; }
            public double Volatility { get; set; }
            public List<double> Returns { get; set; }
            public double AverageReturn { get; set; }
        }

      
        public AnalysisResult AnalyzeSequential(List<double> prices)
        {
            if (prices == null || prices.Count < 2)
                throw new ArgumentException("La lista de precios debe tener al menos 2 valores.");

            // metricas basicas
            double mean = prices.Average();
            double min = prices.Min();
            double max = prices.Max();

            // desviacion
            double variance = 0;
            foreach (var p in prices)
            {
                variance += Math.Pow(p - mean, 2);
            }
            variance /= prices.Count;
            double stdDev = Math.Sqrt(variance);

            // retornos
            List<double> returns = new List<double>();
            for (int i = 1; i < prices.Count; i++)
            {
                double ret = (prices[i] - prices[i - 1]) / prices[i - 1];
                returns.Add(ret);
            }

            double avgReturn = returns.Average();

            // volatilidad
            double volVariance = 0;
            foreach (var r in returns)
            {
                volVariance += Math.Pow(r - avgReturn, 2);
            }
            volVariance /= returns.Count;
            double volatility = Math.Sqrt(volVariance);

            // retorno del resultado
            return new AnalysisResult
            {
                Mean = mean,
                StandardDeviation = stdDev,
                Min = min,
                Max = max,
                Volatility = volatility,
                Returns = returns,
                AverageReturn = avgReturn
            };

=======
    public class SequentialAnalysis
    {
        public ResultadoAnalisis Analizar(List<double> data)
        {
            double promedio = Statistics.Mean(data);
            double volatilidad = Statistics.Volatility(data);
            double max = Statistics.Max(data);
            double min = Statistics.Min(data);
            double retornoProm = Statistics.AverageReturn(data);
            double varianza = Statistics.Variance(data);
            double desviacionEstandar = Statistics.StdDev(data);

            return new ResultadoAnalisis
            {
                Promedio = promedio,
                Volatilidad = volatilidad,
                Maximo = max,
                Minimo = min,
                RetornoPromedio = retornoProm,
                DesviacionEstandar = desviacionEstandar
            };
        }

        public class ResultadoAnalisis
        {
            public double Promedio { get; set; }
            public double Volatilidad { get; set; }
            public double Maximo { get; set; }
            public double Minimo { get; set; }
            public double RetornoPromedio { get; set; }
            public double DesviacionEstandar { get; set; }
>>>>>>> Stashed changes
        }
    }
}
