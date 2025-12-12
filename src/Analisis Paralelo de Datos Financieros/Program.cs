using System;
using System.Collections.Generic;
using Analisis_Paralelo_de_Datos_Financieros.Data;
using Analisis_Paralelo_de_Datos_Financieros.Analysis;
using Analisis_Paralelo_de_Datos_Financieros.Utils;

namespace Analisis_Paralelo_de_Datos_Financieros
{
    class Program
    {
        public static void Main(string[] args)
        {
            // Cargar configuración
            var config = new Config();

            Console.WriteLine("=== Análisis Paralelo de Datos Financieros ===\n");
            Console.WriteLine($"Núcleos disponibles: {config.NucleosDisponibles}");
            Console.WriteLine($"Núcleos a usar: {config.NucleosAUsar}");
            Console.WriteLine($"Tamaño de lote: {config.TamanoLote}");
            Console.WriteLine($"Archivo CSV: {config.RutaArchivoCSV}\n");

            // Cargar datos desde CSV
            var loader = new DataLoader();
            List<double> precios;

            // Intentar cargar desde archivo real
            precios = loader.CargarPrecios(config.RutaArchivoCSV);

            // Si no hay datos en el CSV, generar datos de prueba
            if (precios.Count == 0)
            {
                Console.WriteLine("Usando datos generados para pruebas...");
                precios = loader.GenerarPrecios(1000000);
            }

            Console.WriteLine($"\nTotal de datos a procesar: {precios.Count}\n");

            // ANÁLISIS SECUENCIAL
            Console.WriteLine("--- Ejecutando Análisis Secuencial ---");
            TimerUtil timer = new TimerUtil();
            SequentialAnalysis seqAnalysis = new SequentialAnalysis();

            timer.Start();
            ResultadoAnalisis resultadoSecuencial = seqAnalysis.Analizar(precios);
            timer.Stop();
            float tiempoSecuencial = timer.ElapsedMilliseconds();

            // ANÁLISIS PARALELO
            Console.WriteLine("\n--- Ejecutando Análisis Paralelo ---");
            ParallelAnalysis parallelAnalysis = new ParallelAnalysis(
                config.NucleosAUsar,
                config.TamanoLote);

            timer.Start();
            List<PartialResult> resultadosParciales = parallelAnalysis.Ejecutar(precios);
            timer.Stop();
            float tiempoParalelo = timer.ElapsedMilliseconds();

            ResultCombiner combiner = new ResultCombiner();
            CombinedResult resultadoParalelo = combiner.CombinarResultados(resultadosParciales);

            // RESULTADOS SECUENCIALES
            Console.WriteLine("\n=== Resultados del Análisis Secuencial ===");
            Console.WriteLine($"Promedio: {resultadoSecuencial.Promedio:F2}$");
            Console.WriteLine($"Mínimo: {resultadoSecuencial.Minimo:F2}$");
            Console.WriteLine($"Máximo: {resultadoSecuencial.Maximo:F2}$");
            Console.WriteLine($"Volatilidad: {resultadoSecuencial.Volatilidad:F4}");
            Console.WriteLine($"Desviación Estándar: {resultadoSecuencial.DesviacionEstandar:F2}$");
            Console.WriteLine($"Tiempo de ejecución: {tiempoSecuencial} ms");

            // RESULTADOS PARALELOS
            Console.WriteLine("\n=== Resultados del Análisis Paralelo ===");
            Console.WriteLine($"Promedio: {resultadoParalelo.Promedio:F2}$");
            Console.WriteLine($"Mínimo: {resultadoParalelo.Minimo:F2}$");
            Console.WriteLine($"Máximo: {resultadoParalelo.Maximo:F2}$");
            Console.WriteLine($"Volatilidad: {resultadoParalelo.Volatilidad:F4}");
            Console.WriteLine($"Desviación Estándar: {resultadoParalelo.DesviacionEstandar:F2}$");
            Console.WriteLine($"Tiempo de ejecución: {tiempoParalelo} ms");
            Console.WriteLine($"Número de lotes procesados: {resultadosParciales.Count}");

            // MÉTRICAS DE RENDIMIENTO
            Console.WriteLine("\n=== Métricas de Rendimiento ===");
            double speedup = tiempoSecuencial / tiempoParalelo;
            Console.WriteLine($"Speedup: {speedup:F2}x");

            double eficiencia = (speedup / config.NucleosAUsar) * 100;
            Console.WriteLine($"Eficiencia: {eficiencia:F2}%");

        }
    }
}