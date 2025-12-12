# Descripción del dataset utilizado

Este documento describe el formato de los datos utilizados por el proyecto y las distintas versiones del dataset empleadas para las pruebas de rendimiento.

Los datos se cargan desde archivos CSV y representan series de precios financieros utilizados como entrada para el análisis secuencial y paralelo.

## Formato del archivo CSV

Los archivos utilizados siguen una estructura tabular simple. Cada fila representa un registro de precios correspondiente a una fecha específica.

El archivo CSV contiene un encabezado y columnas separadas por delimitadores (según el origen del archivo). El proyecto se encarga de leer las filas y extraer únicamente los valores necesarios para el análisis.

En el ejemplo de dataset utilizado, las columnas principales son:

- Fecha  
  Representa la fecha asociada al registro del precio.

- Precio  
  Corresponde al valor numérico del precio en esa fecha.  
  Durante la carga de datos, este valor se limpia y convierte a formato numérico, descartando símbolos o valores inválidos.

Otras columnas presentes en el archivo original no son utilizadas por el sistema y se ignoran durante la carga.

## Limpieza y validación de datos

Durante el proceso de importación:
- Se ignoran filas con valores vacíos o no numéricos
- Se eliminan símbolos y caracteres innecesarios
- Solo se almacenan precios válidos para evitar errores en los cálculos estadísticos

Este proceso garantiza que el análisis trabaje únicamente con datos consistentes.

## Tamaños de dataset utilizados

Para evaluar el impacto del paralelismo, se trabajó con diferentes tamaños de dataset:

- Dataset pequeño  
  Utilizado para pruebas rápidas y validación inicial del funcionamiento.

- Dataset mediano (1 millón de registros)  
  Utilizado para observar mejoras de rendimiento con paralelismo moderado.

- Dataset grande (5 millones de registros)  
  Permite evaluar la escalabilidad del sistema y el comportamiento con mayor carga.

- Dataset muy grande (10 millones de registros)  
  Utilizado para pruebas de estrés y análisis de rendimiento en escenarios de alto volumen de datos.

Estos archivos se encuentran disponibles de forma local para las pruebas, pero no se incluyen en el repositorio debido a su tamaño.

## Uso dentro del proyecto

Los archivos CSV son cargados al inicio de la ejecución mediante la clase DataLoader, que se encarga de leer los datos, limpiarlos y convertirlos en una lista de valores numéricos.

Esta lista es posteriormente utilizada por los módulos de análisis secuencial y paralelo.
