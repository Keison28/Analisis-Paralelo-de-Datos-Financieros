using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Analisis_Paralelo_de_Datos_Financieros.Analysis
{
    public class ParallelAnalysis
    {
        private readonly int _cantidadNucleos;
        private readonly int _tamanoLote;

        public ParallelAnalysis(int cantidadNucleos, int tamanoLote)
        {
            _cantidadNucleos = cantidadNucleos;
            _tamanoLote = tamanoLote;
        }

        public List<PartialResult> Ejecutar(List<double> datos)
        {
            var resultados = new List<PartialResult>();
            var lotes = DividirEnLotes(datos, _tamanoLote);

            var opciones = new ParallelOptions
            {
                MaxDegreeOfParallelism = _cantidadNucleos
            };

            object locker = new object();

            Parallel.ForEach(lotes, opciones, lote =>
            {
                var pr = AnalizarLote(lote);

                lock (locker)
                {
                    resultados.Add(pr);
                }
            });

            return resultados;
        }

        private List<List<double>> DividirEnLotes(List<double> datos, int tamanoLote)
        {
            var lotes = new List<List<double>>();

            for (int i = 0; i < datos.Count; i += tamanoLote)
            {
                int cantidad = Math.Min(tamanoLote, datos.Count - i);
                lotes.Add(datos.GetRange(i, cantidad));
            }

            return lotes;
        }

        private PartialResult AnalizarLote(List<double> lote)
        {
            var pr = new PartialResult();

            if (lote == null || lote.Count == 0)
            {
                pr.Count = 0;
                pr.Min = 0;
                pr.Max = 0;
                return pr;
            }

            int n = lote.Count;
            double sum = 0.0;
            double sumSq = 0.0;
            double min = double.MaxValue;
            double max = double.MinValue;

            for (int i = 0; i < n; i++)
            {
                double v = lote[i];
                sum += v;
                sumSq += v * v;
                if (v < min) min = v;
                if (v > max) max = v;
            }

            // Calcular retornos
            int returnsCount = Math.Max(0, n - 1);
            double sumR = 0.0;
            double sumRSq = 0.0;

            for (int i = 1; i < n; i++)
            {
                double r = (lote[i] - lote[i - 1]) / lote[i - 1];
                sumR += r;
                sumRSq += r * r;
            }

            pr.Count = n;
            pr.Sum = sum;
            pr.SumSq = sumSq;
            pr.Min = min;
            pr.Max = max;
            pr.ReturnsCount = returnsCount;
            pr.SumReturns = sumR;
            pr.SumReturnsSq = sumRSq;

            return pr;
        }
    }
}