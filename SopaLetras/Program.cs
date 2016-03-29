using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace SopaLetras
{
    /// La clase Program contiene el ciclo principal, finaliza el juego, ejecuta el tiempo de juego
    /// y maneja la salida del programa. Todo lo referente a las reglas de juego y los dibujos en
    /// pantalla está manejado por la clase Game.
    /// 
    class Program
    {
        /// Longitud del puzzle en letras
        const int LONGITUD_PUZZLE = 49;

        /// Cada n letra debe ser una vocal
        const int VOCAL_CADA = 5;

        /// El límite de tiempo para completar el puzzle
        const int LIMITE_TIEMPO_SEGUNDOS = 60;

        /// El listado de palabras para el juego. El listado debe estar en el mismo directorio
        /// en el que se encuentra el ejecutable. El nombre del archivo es palabras.txt
        static string[] palabras = File.ReadAllLines("palabras.txt");

        /// Creando el objeto Game
        /// Éste contiene toda la funcionalidad del juego
        static Game game;

        /// Cuando salir sea igual a verdadero, se termina el juego
        static bool salir = false;

        /// La palabra ingresada por el jugador
        static string palabra = String.Empty;

        /// Punto de entrada de la aplicación. Inicializa el juego y lo envía al ciclo principal
        static void Main(string[] args)
        {
            /// El jugador también puede cerrar la aplicación al presionar ^C (Ctrl + C)
            Console.CancelKeyPress += new ConsoleCancelEventHandler(Console_CancelKeyPress);

            Console.CursorVisible = false;

            game = new Game(LONGITUD_PUZZLE, VOCAL_CADA, palabras);
            game.DibujarPantallaInicial();
            MainLoop();
        }

        /// Método encargado de manejar el ingreso de las teclas C^  (Ctrl + C)
        static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            Console.SetCursorPosition(0, 19);
            Console.WriteLine("{0} presionado, saliendo...", e.SpecialKey);
            salir = true;
            e.Cancel = true;
        }

        /// El ciclo principal de la aplicación
        static void MainLoop()
        {
            int tiempoMilisegundos = 0;
            int totalMilisegundos = LIMITE_TIEMPO_SEGUNDOS * 1000;
            const int INTERVALO = 100;

            ImprimirTitulo();

            while (tiempoMilisegundos < totalMilisegundos && !salir)
            {
                // Pausar un breve período de tiempo
                Thread.Sleep(INTERVALO);
                tiempoMilisegundos += INTERVALO;

                IngresoJugador();

                ImprimirTiempoRestante(tiempoMilisegundos, totalMilisegundos);
            }

            Console.SetCursorPosition(0, 20);
            Console.WriteLine(Environment.NewLine + Environment.NewLine
                + "¡Juego Terminado! Lograste encontrar {0} palabras.", game.PalabrasEncontradas);
        }

        /// Imprime el título de la aplicación
        private static void ImprimirTitulo()
        {
            // Guardar la posición actual del cursor
            int izquierda = Console.CursorLeft;
            int derecha = Console.CursorTop;

            // Imprimir el tiempo en la esquina superior derecha
            Console.SetCursorPosition(0,0);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Desarrollo de Software - UNICAH");

            // Restaurar el color del texto y la posición del cursor
            Console.ResetColor();
            Console.SetCursorPosition(izquierda, derecha);
        }

        /// Imprime el tiempo restante en la esquina superior derecha de la pantalla
        private static void ImprimirTiempoRestante(int tiempoMilisegundos, int totalMilisegundos)
        {
            int milisegundosRestantes = totalMilisegundos - tiempoMilisegundos;
            double segundosRestantes = (double)milisegundosRestantes / 1000;
            string tiempoRestante = String.Format("{0:00.0} segundos restantes", segundosRestantes);

            // Guardar la posición actual del cursor
            int izquierda = Console.CursorLeft;
            int derecha = Console.CursorTop;

            // Imprimir el tiempo en la esquina superior derecha
            Console.SetCursorPosition(Console.WindowWidth - tiempoRestante.Length, 0);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(tiempoRestante);

            // Restaurar el color del texto y la posición del cursor
            Console.ResetColor();
            Console.SetCursorPosition(izquierda, derecha);
        }

        /// Maneja y espera por el ingreso de un valor por parte del usuario
        static void IngresoJugador()
        {
            Thread.Sleep(50);
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    if (palabra.Length > 0)
                        palabra = palabra.Substring(0, palabra.Length - 1);
                }
                else if (keyInfo.Key == ConsoleKey.Escape)
                {
                    palabra = String.Empty;
                }
                else
                {
                    string key = keyInfo.KeyChar.ToString().ToUpper();
                    if (game.LetraValida(key))
                    {
                        palabra = palabra + key;
                    }
                }
                game.IngresoActual = palabra;
                game.ProcesarIngreso();
                game.ActualizarPantalla();
            }
        }
    }
}
