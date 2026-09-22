# Factorial, Fibonacci y Torres de Hanói

Aplicación de escritorio en C# y Windows Forms con un único menú de estilo retro.

- **Factorial:** calcula recursivamente el valor factorial de un número $n \ge 0$ ($n! = n \times (n - 1)!$, con caso base $0! = 1$) utilizando `BigInteger` para soportar cálculos de gran magnitud sin desbordamientos de memoria. Incluye validación de excepciones ("Hay datos faltantes", "Introducir sólo números", números negativos) y despliega la traza y árbol de llamadas recursivas en tiempo real.
- **Fibonacci:** calcula recursivamente la serie desde `F(0) = 0` hasta la cantidad de términos elegida (1 a 40). Usa memoria de resultados para evitar recalcular llamadas repetidas.
- **Torres de Hanói:** genera recursivamente los movimientos mínimos para llevar de 1 a 10 discos de la torre A a la C, con la torre B como auxiliar. Muestra el tablero, la lista de movimientos, reproducción automática, pausa, paso a paso y reinicio. También permite resolverlo manualmente.

Para jugar manualmente, haz clic en el disco superior de cualquier torre: se vuelve gris. Pasa el cursor sobre otra torre donde pueda colocarse legalmente: aparecerá una copia gris con borde punteado. Haz clic en esa torre para mover el disco; la vista previa desaparecerá. Un movimiento inválido no se aplica. Al empezar a jugar manualmente se detiene la reproducción automática; pulsa **REINICIAR** para volver a ella.

## Descargar el ejecutable

Descarga [`Ejecutable/Fibonacci_Hanoi.exe`](Ejecutable/Fibonacci_Hanoi.exe) y ábrelo en Windows de 64 bits. Es un archivo único y autónomo: no necesitas instalar .NET por separado. Usa la opción **Download raw file** de GitHub para descargar el archivo, en lugar de abrir su vista previa.

El repositorio incluye tanto el código fuente como el ejecutable autónomo compilado a partir de este proyecto.
