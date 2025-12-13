# Diseño del sistema y enfoque de procesamiento paralelo

Este proyecto analiza datos financieros utilizando dos enfoques distintos: un análisis secuencial y un análisis paralelo. Más allá de calcular métricas estadísticas, el objetivo principal es evaluar cómo se comporta el paralelismo al procesar grandes volúmenes de datos y cómo ciertas decisiones de diseño influyen tanto en el rendimiento como en la precisión de los resultados.

La estructura del proyecto busca separar responsabilidades de forma clara y permitir una comparación directa entre ambos modelos de ejecución.

## Flujo general de ejecución

La ejecución completa del programa se controla desde Program.cs, que actúa como punto de entrada y coordinador del flujo.

En primer lugar, los datos de precios se cargan desde un archivo CSV utilizando la clase DataLoader. Durante esta etapa se validan y limpian los datos, descartando valores inválidos o mal formateados para evitar errores en los cálculos posteriores.

Una vez cargado el dataset, se ejecuta el análisis secuencial. Esta versión recorre toda la lista de precios utilizando un solo hilo y cumple dos funciones importantes: sirve como referencia de correctitud y establece un tiempo base para comparar el rendimiento del enfoque paralelo.

Posteriormente, se ejecuta el análisis paralelo. En este caso, el dataset se divide en segmentos más pequeños llamados lotes. Cada lote se procesa de forma independiente y concurrente, aprovechando múltiples núcleos del procesador. Cada ejecución genera resultados parciales que luego se combinan en un resultado global.

Finalmente, se miden los tiempos de ejecución de ambos enfoques, lo que permite calcular métricas de rendimiento como el speedup.

## Componentes principales y responsabilidades

Program.cs  
Es el controlador principal del programa. Se encarga de:
- Leer los parámetros de ejecución definidos en Config
- Cargar el dataset
- Ejecutar el análisis secuencial y el paralelo
- Medir los tiempos de ejecución
- Mostrar resultados y métricas de rendimiento

Config  
Centraliza todos los parámetros de configuración del sistema, incluyendo:
- Cantidad de núcleos disponibles y a utilizar
- Tamaño de los lotes
- Ruta del archivo CSV
- Opciones de ejecución paralela

DataLoader  
Lee el archivo CSV y convierte los valores a precios numéricos válidos.  
También elimina datos inconsistentes para garantizar estabilidad en los cálculos estadísticos.

SequentialAnalysis  
Implementa el enfoque tradicional de análisis recorriendo todo el dataset en un solo hilo.  
Este componente prioriza claridad y exactitud, lo que lo convierte en una base confiable para validar el análisis paralelo.

ParallelAnalysis  
Implementa un modelo de paralelismo de datos dividiendo el dataset en lotes y procesándolos de manera concurrente utilizando Parallel.ForEach.  
Cada lote produce un resultado parcial con estadísticas agregadas.

ResultCombiner  
Recibe todos los resultados parciales y los combina para generar métricas globales.  
Este proceso se realiza utilizando fórmulas estadísticas que permiten reconstruir los valores finales sin necesidad de reprocesar el dataset completo.

## Modelo de paralelismo

El proyecto utiliza un modelo de paralelismo de datos, donde cada lote representa una unidad de trabajo independiente. Esto permite que múltiples lotes se procesen al mismo tiempo sin introducir dependencias complejas entre hilos.

El tamaño de los lotes es configurable y representa un balance entre la sobrecarga de gestión de tareas y la cantidad de trabajo asignada a cada hilo. Lotes grandes reducen la sobrecarga, mientras que lotes pequeños incrementan el nivel de paralelismo.

El grado de paralelismo se controla explícitamente mediante ParallelOptions, evitando la saturación del sistema y manteniendo un comportamiento predecible.

## Sincronización y limitaciones del diseño

Durante el procesamiento de los lotes, los hilos no comparten estado mientras realizan los cálculos numéricos. La única sección compartida es la estructura donde se almacenan los resultados parciales.

El acceso a esta estructura se protege mediante un lock aplicado únicamente al momento de agregar un resultado parcial, manteniendo la sección crítica lo más pequeña posible.

En cuanto a las métricas de volatilidad, los retornos se calculan dentro de cada lote. Esto implica que no se consideran los retornos entre lotes consecutivos, lo cual introduce una ligera aproximación. Esta decisión simplifica el paralelismo y se considera aceptable dentro del alcance del proyecto.
