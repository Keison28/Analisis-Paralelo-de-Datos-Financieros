# Ejecución, validación y resultados

Este documento describe cómo ejecutar el proyecto, cómo validar la correctitud de los resultados y cómo interpretar el rendimiento del análisis paralelo en comparación con el secuencial.

## Requisitos

- .NET SDK instalado
- Un equipo con múltiples núcleos para observar mejoras de rendimiento

## Ejecución del programa

Desde la carpeta del proyecto, ejecutar los siguientes comandos:

dotnet restore  
dotnet build  
dotnet run  

El programa ejecutará automáticamente tanto el análisis secuencial como el paralelo utilizando los parámetros definidos en Config.

## Métricas calculadas

Para ambos enfoques se calculan las siguientes métricas:
- Valor mínimo y máximo
- Desviación estándar
- Volatilidad
- Tiempo de ejecución

A partir de los tiempos obtenidos, se calcula el speedup y la eficiencia comparando el tiempo del análisis secuencial con el del análisis paralelo.

## Validación de correctitud

Los resultados del análisis secuencial y del paralelo deben ser muy similares.  
Pueden existir pequeñas diferencias debido al orden de las operaciones en punto flotante.

Los valores mínimos y máximos deben coincidir exactamente, siempre que el dataset de entrada sea el mismo.

En el caso de la volatilidad, pueden observarse variaciones leves, ya que los retornos se calculan dentro de cada lote y no entre lotes consecutivos. Este comportamiento es una consecuencia directa del diseño del procesamiento paralelo.

## Evaluación de rendimiento

Para evaluar el impacto del paralelismo, se recomienda:
- Ejecutar el programa utilizando diferentes cantidades de núcleos
- Probar distintos tamaños de lote
- Comparar tiempos de ejecución y valores de speedup

Estas pruebas permiten analizar la escalabilidad del sistema y entender cómo influyen las decisiones de configuración en el rendimiento.

## Resultados experimentales

Los resultados concretos obtenidos durante las pruebas, incluyendo tiempos de ejecución, speedup y comparaciones entre distintas configuraciones, se documentan de forma separada en el archivo metrics.md.

Esta separación permite mantener los detalles de implementación y los resultados experimentales claramente organizados.
