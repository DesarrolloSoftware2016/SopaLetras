using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SopaLetras
{
    /// La clase Game se encarga de mantener el estado del juego y dibuja las
    /// actualizaciones de la pantalla. Utiliza una instancia de la clase Puzzle
    /// para poder encargarse de las letras en la cuadrícula del Puzzle y una
    /// instancia de la clase VerificarPalabra para encargarse de la validación de las
    /// palabras y revisar las respuestas del jugador.
    class Game
    {
        /// Delegando el objeto VerificarPalabra para revisar las palabras encontradas
        private VerificarPalabra verificarPalabra;

        /// Número de palabras encontradas
        public int PalabrasEncontradas
        {
            // El objeto VerificarPalabra mantiene un registro del mismo
            get { return verificarPalabra.PalabrasEncontradas; }
        }

        /// Delegando el objeto Puzzle que mantiene un registro de las letras aleatorias
        /// y de verificar las palabras
        private Puzzle puzzle;

        /// El ingreso actual del jugador
        public string IngresoActual { private get; set; }

        /// Constructores
        /// <parámetro="longitudPuzzle">El número de letras en el Puzzle</parámetro>
        /// <parámetro="vocalCada">Añádir una vocal cada n letras</parámetro>
        /// <parámetro="palabrasValidas">El listado de palabras válidas</parámetro>
        public Game(int longitudPuzzle, int vocalCada, IEnumerable<string> palabrasValidas)
        {
            this.verificarPalabra = new VerificarPalabra(palabrasValidas);
            this.puzzle = new Puzzle(longitudPuzzle, vocalCada);
            IngresoActual = String.Empty;
        }

        /// Dibuja la pantalla inicial de la aplicación
        public void DibujarPantallaInicial()
        {
            Console.Clear();
            Console.Title = "Sopa de Letras";
            puzzle.Dibujar(25, 3);
            Console.SetCursorPosition(7, 11);
            Console.Write("┌═══════════════════════════════════════════════════════╖");
            Console.SetCursorPosition(7, 12);
            Console.Write("║");
            Console.SetCursorPosition(63, 12);
            Console.Write("║");
            Console.SetCursorPosition(7, 13);
            Console.Write("╘═══════════════════════════════════════════════════════╝");
            ActualizarPantalla();
        }

        /// Actualiza la pantalla
        public void ActualizarPantalla()
        {
            // Utilizar String.PadRight() para asegurarnos de que la casilla amarilla de ingreso se
            // mantenga de un tamaño constante, sin importar la longitud de la palabra ingresada
            Console.SetCursorPosition(8, 12);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            string mensaje = String.Format("Ingrese la palabra #{0}: {1}",
                    verificarPalabra.PalabrasEncontradas, IngresoActual);
            Console.Write(mensaje.PadRight(54));
            Console.ResetColor();

            Console.SetCursorPosition(0, 18);
            Console.Write("Palabras encontradas: ");
            foreach (string palabra in verificarPalabra.ListaPalabrasEncontradas)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write("{0} ", palabra);
                Console.ResetColor();
            }

            Console.SetCursorPosition(15, 14);
            Console.WriteLine("Ingrese cualquier palabra que encuentre.");
            Console.SetCursorPosition(17, 15);
            Console.Write("Presione <ESC> para limpiar la línea.");
            Console.SetCursorPosition(21, 16);
            Console.Write("Para salir presione Ctrl + C.");
        }

        /// Procesa el ingreso de una nueva letra por parte del jugador
        public void ProcesarIngreso()
        {
            verificarPalabra.VerificarRespuesta(IngresoActual, puzzle);
        }

        /// Retorna true si se presionó una tecla válida
        /// <parámetro="key">La tecla presionada</parámetro>
        /// <retorna>True sólo si la tecla es una vocal o una consonante</retorna>
        public bool LetraValida(string key)
        {
            if (key.Length == 1)
            {
                char c = key.ToCharArray()[0];
                return Puzzle.Consonantes.Contains(c) || Puzzle.Vocales.Contains(c);
            }
            return false;
        }

    }
}
