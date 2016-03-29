using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SopaLetras
{
    /// La clase VerificarPalabra mantiene un registro de la lista de palabras válidas
    /// y verifica que la palabra ingresada sea válida y que sólo pueda estar compuesta
    /// de letras dentro de la cuadrícula.
    class VerificarPalabra
    {
        /// La lista de palabras válidas
        private List<string> palabras = new List<string>();

        /// La lista de palabras encontradas
        private List<string> palabrasEncontradas = new List<string>();

        /// Retorna el número de palabras que se han encontrado
        public int PalabrasEncontradas
        {
            get { return palabrasEncontradas.Count; }
        }

        /// Obtiene el listado de palabras encontradas
        public IEnumerable<string> ListaPalabrasEncontradas
        {
            get
            {
                List<string> valor = new List<string>();
                foreach (string palabra in palabrasEncontradas)
                {
                    valor.Add(palabra.ToUpper());
                }
                return valor;
            }
        }

        /// Constructores
        /// <parámetro="palabrasValidas">El listado de palabras válidas</parámetro>
        public VerificarPalabra(IEnumerable<string> palabrasValidas)
        {
            // Convertir cada palabra a mayúscula y añadirla a la lista de palabras
            foreach (string palabra in palabrasValidas)
                this.palabras.Add(palabra.ToUpper());
        }

        /// Verificar si la palabra del jugador es una palabra válida que se encuentra dentro de la cuadrícula
        /// <parámetro="palabra">Palabra a verificar</parámetro>
        /// <parámetro="puzzle">Referencia al objeto puzzle</parámetro>
        public void VerificarRespuesta(string palabra, Puzzle puzzle)
        {
            // Verificar que la palabra no sea null o vacía, y que la palabra sea de al menos 4 caracteres de longitud
            if (String.IsNullOrEmpty(palabra) || palabrasEncontradas.Contains(palabra) || palabra.Length < 4)
                return;

            // Verificar que la palabra esté en mayúscula y que el string palabraMayuscula será destruido
            // Se remueven todas las letras del puzzle de la palabra. Si sobra alguna letra, la palabra
            // no se encuentra en el puzzle.
            string palabraMayuscula = palabra.ToUpper();
            if (palabras.Contains(palabraMayuscula))
            {
                // Verificar que la palabra está formada por letras del puzzle
                foreach (char letra in puzzle.Letras)
                {
                    // Remover cada letra del puzzle letter de la palabra
                    if (palabraMayuscula.Contains(letra))
                    {
                        if (palabraMayuscula.StartsWith(letra.ToString()))
                            palabraMayuscula = palabraMayuscula.Substring(1);
                        else
                        {
                            int index = palabraMayuscula.IndexOf(letra);
                            palabraMayuscula = palabraMayuscula.Substring(0, index - 1) + palabraMayuscula.Substring(index + 1);
                        }
                    }
                }
            }

            // Si removiendo todas las letras del puzzle de la palabraMayuscula nos deja un string vacío,
            // se econtró una palabra. Reproducir un sonido y añadirla a la lista de palabras encontradas.
            if (String.IsNullOrEmpty(palabraMayuscula))
            {
                Console.Beep();
                palabrasEncontradas.Add(palabra);
            }
        }
    }
}
