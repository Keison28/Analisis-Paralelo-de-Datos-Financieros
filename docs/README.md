# Análisis Paralelo de Datos Financieros

Este repositorio contiene el proyecto final de la materia Programación Paralela.  
El objetivo del proyecto es analizar una serie de precios financieros aplicando técnicas de programación paralela y comparar su comportamiento frente a una implementación secuencial.

Se desarrollan dos enfoques de análisis: uno secuencial y otro paralelo, con el fin de evaluar el impacto del uso de múltiples núcleos al procesar grandes volúmenes de datos.

## Objetivo del proyecto

El objetivo principal es aplicar paralelismo de datos para mejorar el rendimiento del procesamiento, manteniendo resultados consistentes con una solución secuencial.

De forma específica, el proyecto busca:
- Implementar un análisis estadístico secuencial como referencia
- Diseñar una versión paralela del mismo análisis
- Comparar tiempos de ejecución entre ambos enfoques
- Calcular métricas de rendimiento como speedup
- Analizar ventajas y limitaciones del paralelismo aplicado al problema

## Alcance

El análisis se centra en el cálculo de métricas estadísticas a partir de una serie temporal de precios financieros, entre ellas:
- Promedio
- Valor mínimo y máximo
- Desviación estándar
- Volatilidad

El objetivo del proyecto no es realizar predicciones financieras ni análisis de mercado, sino utilizar datos financieros como un caso práctico para aplicar conceptos de programación paralela.

## Estructura del proyecto

El proyecto está organizado de forma modular para facilitar la comprensión del flujo y la separación de responsabilidades:

- Program.cs  
  Punto de entrada del programa y controlador del flujo de ejecución.

- Config  
  Define los parámetros de ejecución, como cantidad de núcleos y tamaño de los lotes.

- DataLoader  
  Encargado de cargar y limpiar los datos desde un archivo CSV.

- SequentialAnalysis  
  Implementa el análisis secuencial de los datos.

- ParallelAnalysis  
  Implementa el análisis paralelo utilizando múltiples hilos.

- ResultCombiner  
  Combina los resultados parciales generados durante el procesamiento paralelo.

La carpeta src contiene la implementación completa del sistema.

## Documentación

La documentación técnica y de apoyo se encuentra en la carpeta docs, donde se incluyen los siguientes archivos:

- Design.md  
  Describe el diseño del sistema y el enfoque de paralelismo utilizado.

- Execution.md  
  Explica cómo ejecutar el proyecto y cómo validar los resultados obtenidos.


## Consideraciones finales

Este proyecto permite observar de forma práctica cómo el paralelismo puede mejorar el rendimiento en tareas de procesamiento intensivo, así como las limitaciones que aparecen al dividir datos y combinar resultados.

Las decisiones de diseño priorizan la claridad del modelo paralelo y la facilidad de comparación entre ambas implementaciones.
