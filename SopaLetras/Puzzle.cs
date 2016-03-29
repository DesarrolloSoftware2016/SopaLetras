using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SopaLetras
{
    /// La clase Puzzle mantiene un registro de la cuadrícula del puzzle. La clase Game la utiliza
    /// para imprimir la cuadrícula inicial en la pantalla, y la clase VerificarPalabra la utiliza para verificar
    /// si el ingreso del usuario contiene sólo letras de la cuadrícula
    class Puzzle
    {
        /// Aleatorio
        private Random random = new Random();

        /// Consonantes (incluyendo la Y)
        public static readonly char[] Consonantes = { 'B', 'C', 'D', 'F', 'G', 'H', 'J', 'K', 
               'L', 'M', 'N', 'Ñ', 'P', 'Q', 'R', 'S', 'T', 'V', 'W', 'X', 'Y', 'Z' };

        /// Vocales (incluyendo la Y)
        public static readonly char[] Vocales = { 'A', 'E', 'I', 'O', 'U', 'Y' };

        /// Utilizado por la propiedad Letras
        char[] letras;

        /// Obtener las letras en el puzzle
        public IEnumerable<char> Letras
        {
            get { return letras; }
        }

        /// El número de letras en el puzzle
        private int longitudPuzzle;

        /// Constructores
        /// <parámetro="longitudPuzzle">El número de letras en elpuzzle</parámetro>
        /// <parámetro="vocalCada">Vocal cada n letras</parámetro>
        public Puzzle(int longitudPuzzle, int vocalCada)
        {
            this.longitudPuzzle = longitudPuzzle;

            letras = new char[longitudPuzzle];

            for (int i = 0; i < longitudPuzzle; i++)
            {
                if (i % vocalCada == 0)
                    letras[i] = Vocales[random.Next(Vocales.Length)];
                else
                    letras[i] = Consonantes[random.Next(Consonantes.Length)];
            }
        }

        /// Dibuja el puzzle en un punto específico de la pantalla
        /// <parámetro="izquierda">La posición del cursor en una columna</parámetro>
        /// <parámetro="arriba">La posición del cursor en una fila</parámetro>
        public void Dibujar(int izquierda, int arriba)
        {
            int arribaAnterior = Console.CursorTop;
            int izquierdaAnterior = Console.CursorLeft;

            Console.BackgroundColor = ConsoleColor.White;

            // Crear un puzzle aleatorio utilizando letras aleatorias e imprimirlo
            for (int i = 0; i < longitudPuzzle; i++)
            {
                // Utilizar el movimiento del cursos para dibujar las filas de la cuadrícula del puzzle
                if (i % Math.Floor(Math.Sqrt(longitudPuzzle)) == 0)
                {
                    Console.CursorTop = arriba++;
                    Console.CursorLeft = izquierda;
                }

                if (Vocales.Contains(letras[i]))
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                else
                    Console.ForegroundColor = ConsoleColor.DarkBlue;

                Console.Write(" {0} ", letras[i]);
            }

            Console.ResetColor();

            Console.CursorTop = arribaAnterior;
            Console.CursorLeft = izquierdaAnterior;
        }
    }
}
