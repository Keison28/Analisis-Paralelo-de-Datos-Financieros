using System;
using System.Collections.Generic;

namespace Analisis_Paralelo_de_Datos_Financieros.Analysis
{
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
    }

    public class ResultadoAnalisis
    {
        public double Promedio { get; set; }
        public double Volatilidad { get; set; }
        public double Maximo { get; set; }
        public double Minimo { get; set; }
        public double RetornoPromedio { get; set; }
        public double DesviacionEstandar { get; set; }
    }
}