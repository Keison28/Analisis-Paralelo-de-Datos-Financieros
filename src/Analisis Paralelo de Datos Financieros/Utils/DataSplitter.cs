using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analisis_Paralelo_de_Datos_Financieros.Utils
{
    public class DataSplitter
    {
        // Divide una lista en lotes del tamaño especificado.
        public static List<List<double>> DividirEnLotes(List<double> datos, int tamanoLote)
        {
            var lotes = new List<List<double>>();

            for (int i = 0; i < datos.Count; i += tamanoLote)
            {
                int cantidad = Math.Min(tamanoLote, datos.Count - i);
                var lote = datos.GetRange(i, cantidad);
                lotes.Add(lote);
            }

            return lotes;
        }
    }
}