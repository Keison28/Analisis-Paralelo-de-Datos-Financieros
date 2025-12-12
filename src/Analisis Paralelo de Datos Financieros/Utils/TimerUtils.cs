using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analisis_Paralelo_de_Datos_Financieros.Utils
{
        public class TimerUtil
        {
            
            private Stopwatch _stopwatch;

            public TimerUtil()
            {
                _stopwatch = new Stopwatch();
            }

          
            /// Inicia el cronómetro.
       
            public void Start()
            {
                _stopwatch.Restart();
            }

            /// Detiene el cronómetro.
            public void Stop()
            {
                _stopwatch.Stop();
            }

            /// Devuelve el tiempo transcurrido en milisegundos.
            public long ElapsedMilliseconds()
            {
                return _stopwatch.ElapsedMilliseconds;
            }

         
        }

}
