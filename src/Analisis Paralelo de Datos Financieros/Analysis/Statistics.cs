using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analisis_Paralelo_de_Datos_Financieros.Analysis
{
    public static class Statistics
    {
        // PROMEDIO

        // v = a los datos en el dataset
        public static double Mean(List<double> data)
        {
            if (data == null || data.Count == 0)
                return 0;

            double sum = 0;
            foreach (var v in data)
                sum += v;

            return sum / data.Count;
        }

        // VARIANZA
        public static double Variance(List<double> data)
        {
            if (data == null || data.Count < 2)
                return 0;

            // se calcula el promedio
            double mean = Mean(data);
            double sumSq = 0;

            //se calcula que tan alejados están los datos del promedio
            foreach (var v in data)
            {
                double diff = v - mean;
                sumSq += diff * diff;
            }

            return sumSq / (data.Count - 1); // varianza muestral
        }

        // DESVIACIÓN ESTÁNDAR

        public static double StdDev(List<double> data)
        {
            return Math.Sqrt(Variance(data));
        }

        // MÍNIMO Y MÁXIMO
        
        public static double Min(List<double> data)
        {
            if (data == null || data.Count == 0)
                return 0;

            // se aplica el valor maximo a min, asi cualquier dato será menor, minimo una vez
            double min = double.MaxValue;
            foreach (var v in data)
                if (v < min) min = v;

            return min;
        }
            // se aplica el valor minimo a max, asi cualquier dato será mayor, minimo una vez

        public static double Max(List<double> data)
        {
            if (data == null || data.Count == 0)
                return 0;

            double max = double.MinValue;
            foreach (var v in data)
                if (v > max) max = v;

            return max;
        }
     
        // RETORNOS
        public static double AverageReturn(List<double> data)
        {
            if (data == null || data.Count < 2)
                return 0;

            double sum = 0;
            int count = 0;

            for (int i = 1; i < data.Count; i++)
            {
                //retorno = (PrecioFinal - PrecioInicial) / PrecioInicial
                double r = (data[i] - data[i - 1]) / data[i - 1];
                sum += r;
                count++;
            }

            return sum / count;
        }

     


        //Volatilidad, calcula la inestabilidad de los retornos
        public static double Volatility(List<double> data)
        {
            if (data == null || data.Count < 2)
                return 0;

            var returns = new List<double>();

            for (int i = 1; i < data.Count; i++)
            {
                double r = (data[i] - data[i - 1]) / data[i - 1];
                returns.Add(r);
            }

            return StdDev(returns);
        }
    }
}

