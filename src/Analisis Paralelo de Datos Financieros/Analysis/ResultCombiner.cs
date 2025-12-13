using System;
using System.Collections.Generic;

namespace Analisis_Paralelo_de_Datos_Financieros.Analysis
{
    public class ResultCombiner
    {
        public CombinedResult CombinarResultados(List<PartialResult> parcial)
        {
            if (parcial == null || parcial.Count == 0)
                return new CombinedResult();

            long totalCount = 0;
            long totalReturnsCount = 0;

            double sum = 0, sumSq = 0;
            double sumR = 0, sumRSq = 0;

            double min = double.MaxValue;
            double max = double.MinValue;

            foreach (var p in parcial)
            {
                totalCount += p.Count;
                totalReturnsCount += p.ReturnsCount;

                sum += p.Sum;
                sumSq += p.SumSq;

                sumR += p.SumReturns;
                sumRSq += p.SumReturnsSq;

                if (p.Count > 0)
                {
                    if (p.Min < min) min = p.Min;
                    if (p.Max > max) max = p.Max;
                }
            }

            var resultado = new CombinedResult
            {
                TotalCount = (int)totalCount,
                TotalReturnsCount = (int)totalReturnsCount,
                Promedio = totalCount > 0 ? sum / totalCount : 0,
                Minimo = min == double.MaxValue ? 0 : min,
                Maximo = max == double.MinValue ? 0 : max
            };

            // Calcular varianza y desviación estándar
            if (totalCount > 1)
            {
                double varianza = (sumSq - (sum * sum) / totalCount) / (totalCount - 1);
                resultado.Varianza = varianza;
                resultado.DesviacionEstandar = Math.Sqrt(Math.Max(0, varianza));
            }

            // Calcular volatilidad
            if (totalReturnsCount > 1)
            {
                double varR = (sumRSq - (sumR * sumR) / totalReturnsCount) / (totalReturnsCount - 1);
                resultado.Volatilidad = Math.Sqrt(Math.Max(0, varR));
            }

            return resultado;
        }
    }

    public class CombinedResult
    {
        public double Promedio { get; set; }
        public double Varianza { get; set; }
        public double DesviacionEstandar { get; set; }
        public double Minimo { get; set; }
        public double Maximo { get; set; }
        public double Volatilidad { get; set; }
        public int TotalCount { get; set; }
        public int TotalReturnsCount { get; set; }
    }
}