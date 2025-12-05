using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        // Ejecuta el análisis en paralelo dividiendo los datos en lotes
        public List<double> Ejecutar(List<double> datos)
        {
            List<double> resultados = new List<double>();
            List<List<double>> lotes = DividirEnLotes(datos, _tamanoLote);

            ParallelOptions opciones = new ParallelOptions
            {
                MaxDegreeOfParallelism = _cantidadNucleos
            };

            object locker = new object();

            // Se procesa cada lote al mismo tiempo
            Parallel.ForEach(lotes, opciones, lote =>
            {
                double resultadoLote = AnalizarLote(lote);

                // Se protege la lista de resultados para evitar errores
                lock (locker)
                {
                    resultados.Add(resultadoLote);
                }
            });

            return resultados;
        }

        // Divide el dataset en lotes del tamaño configurado
        private List<List<double>> DividirEnLotes(List<double> datos, int tamanoLote)
        {
            List<List<double>> lotes = new List<List<double>>();

            for (int i = 0; i < datos.Count; i += tamanoLote)
            {
                int cantidad = Math.Min(tamanoLote, datos.Count - i);
                lotes.Add(datos.GetRange(i, cantidad));
            }

            return lotes;
        }

        // Aplica el análisis financiero al lote
        private double AnalizarLote(List<double> lote)
        {
            double suma = 0;

            foreach (double v in lote)
                suma += v;

            // temporal: se usa suma como ejemplo
            return suma;
        }
    }
}