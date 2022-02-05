using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet_Pendu
{
    internal class DessinASCII
    {
        private string[] _drawing =
        {
@"
  +---+
  |   |
      |
      |
      |
      |
=========",
@"
  +---+
  |   |
  O   |
      |
      |
      |
=========",
@"
  +---+
  |   |
  O   |
  |   |
      |
      |
=========",
@"
  +---+
  |   |
  O   |
 /|   |
      |
      |
=========",
@"
  +---+
  |   |
  O   |
 /|\  |
      |
      |
=========",
@"
  +---+
  |   |
  O   |
 /|\  |
 /    |
      |
=========",
@"
  +---+
  |   |
  O   |
 /|\  |
 / \  |
      |
========="
        };

        /// <summary>
        /// Obtenir le dessin à l'index spécifié. 0 au démarrage, 1 pour le premier coup manqué.
        /// </summary>
        /// <param name="i">index</param>
        /// <returns>Dessin ASCII</returns>
        public string GetDrawingAtIndex(int i) => _drawing[i];
    }
}
