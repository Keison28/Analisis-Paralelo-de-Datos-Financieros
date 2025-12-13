# Métricas y comparativas de rendimiento

Este documento contiene los resultados obtenidos al ejecutar el proyecto bajo distintas configuraciones de paralelismo.  
Aquí se documentan únicamente resultados medidos y observaciones de rendimiento. No se incluye código.

Las pruebas se realizaron para evaluar el impacto del número de núcleos utilizados en el tiempo de ejecución, el speedup y la eficiencia del análisis paralelo.

---

## Configuración general de las pruebas

Todas las ejecuciones se realizaron bajo las siguientes condiciones:

- Núcleos disponibles en el sistema: 16
- Tamaño de lote: 10000
- Dataset procesado: 1,000,000 registros
- Archivo configurado: cacao_5millones.csv  
  (los datos reales no se cargaron correctamente, por lo que se utilizaron datos generados para las pruebas)
- Número total de lotes procesados: 100

Para cada configuración se ejecutó primero el análisis secuencial y luego el análisis paralelo, midiendo el tiempo de ejecución de ambos.

---

## Resultados con distintos números de núcleos

### Ejecución con 4 núcleos

El análisis secuencial tomó aproximadamente 133 ms, mientras que el análisis paralelo tomó alrededor de 27 ms.  
El speedup obtenido fue cercano a 4.9x.

La eficiencia calculada fue superior al 100%, lo cual indica que, en este caso, el paralelismo logró aprovechar de forma muy efectiva los recursos disponibles. Este comportamiento puede explicarse por efectos de caché y optimizaciones internas del sistema.

---

### Ejecución con 6 núcleos

El tiempo secuencial fue de aproximadamente 135 ms y el tiempo paralelo se redujo a unos 30 ms.  
El speedup obtenido fue de aproximadamente 4.5x.

La eficiencia se mantuvo alrededor del 75%, lo cual se considera un valor adecuado y cercano al punto óptimo para este tipo de procesamiento.

---

### Ejecución con 8 núcleos

Al aumentar a 8 núcleos, el tiempo paralelo fue de aproximadamente 38 ms frente a un tiempo secuencial de 150 ms.  
El speedup disminuyó a alrededor de 3.95x.

La eficiencia cayó por debajo del 50%, lo que indica que el overhead del paralelismo comienza a superar los beneficios de seguir agregando núcleos.

---

### Ejecución con 12 núcleos

Con 12 núcleos, el tiempo paralelo fue cercano a 49 ms, mientras que el secuencial fue de aproximadamente 120 ms.  
El speedup bajó a alrededor de 2.45x.

La eficiencia se redujo considerablemente, situándose cerca del 20%, lo que demuestra que el sistema ya no escala de forma efectiva con esta cantidad de núcleos.

---

### Ejecución con 16 núcleos

Al utilizar los 16 núcleos disponibles, el tiempo paralelo fue de aproximadamente 62 ms frente a un tiempo secuencial de 160 ms.  
El speedup se mantuvo alrededor de 2.6x.

La eficiencia fue cercana al 16%, confirmando que seguir aumentando el número de núcleos no resulta rentable para este volumen de datos y esta configuración.

---

## Consistencia de los resultados

En todas las ejecuciones se observó que los valores estadísticos calculados por el análisis secuencial y el paralelo coincidieron:

- El promedio, mínimo y máximo fueron iguales.
- La desviación estándar se mantuvo consistente.
- La volatilidad no presentó variaciones significativas.

Esto confirma que el paralelismo no afecta la correctitud de los resultados, sino únicamente el tiempo de ejecución.

---

## Observaciones sobre escalabilidad

Los resultados muestran claramente que el paralelismo ofrece beneficios significativos cuando se utiliza un número reducido de núcleos.  
El mejor equilibrio entre reducción de tiempo y eficiencia se obtiene entre 4 y 6 núcleos.

A partir de 8 núcleos, la eficiencia cae por debajo del umbral recomendado del 60%, y el sistema deja de escalar de forma efectiva.  
Con 12 y 16 núcleos, el overhead de paralelización y sincronización domina el costo total de ejecución.

---

## Representación gráfica

A partir de los datos obtenidos se generaron gráficas externas de:

- Speedup en función del número de núcleos
- Eficiencia en función del número de núcleos

Estas gráficas permiten visualizar claramente el punto a partir del cual agregar más núcleos deja de ser beneficioso.

---

## Conclusión final

El análisis paralelo mejora de forma significativa el rendimiento frente al enfoque secuencial cuando se utilizan pocos núcleos.  
Sin embargo, el sistema presenta un punto de saturación a partir del cual aumentar el paralelismo no resulta eficiente.

Para el tamaño de dataset probado y la configuración actual, el uso de entre 4 y 6 núcleos ofrece el mejor compromiso entre rendimiento y eficiencia.  
Superar este rango no es recomendable, ya que la eficiencia cae por debajo de valores aceptables.
