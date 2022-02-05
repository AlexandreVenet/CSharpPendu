using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DotNet_Pendu
{
	internal class Pendu
	{
		// L’ordinateur choisit un mot au hasard dans une liste, un mot de huit lettres maximum.
		// Le joueur tente de trouver les lettres composant le mot.
		//	- À chaque coup, il saisit une lettre.
		//	- Si la lettre figure dans le mot, l’ordinateur affiche le mot avec les lettres déjà trouvées.
		//	- Celles qui ne le sont pas encore sont remplacées par des barres (_).
		//	- Le joueur a 6 chances. Au delà, il a perdu.

		// Stocker la liste des lettres utilisées pour indiquer à l'utilisateur les lettres qu'il a déjà entrées.

		#region Fields

		private bool _hasPressedEnter;

		private bool _isRunning = true;

		private string[] _words =
		{
			"laitue", "hareng", "jambon", "pharynx", "phoque", "langue", "stylo","agent","fromage","whisky","billet","boyaux",
			"laser","joystick","crâne","joyeux","cahier","camping","argent", "rivage","physique","aujourd'hui"
		};

		private string _myWord;
		private string _myCurrentString;

		private int _myCurrentchance = 0;
		private int _chancesTotal = 6;

		private List<char> _userLetters = new List<char>();

		private DessinASCII _drawingASCII = new DessinASCII();

		private bool _showMyWord = true;

		#endregion



		#region Constructor

		public Pendu()
		{
			App();
		}

		#endregion




		#region Methods
		
		/// <summary>
		/// Méthode principale du jeu.
		/// </summary>
		private void App()
		{
			while (_isRunning)
			{
				// Démarrage
				Introduction();				

				// La partie
				while (_myCurrentchance < _chancesTotal)
				{
					Console.WriteLine($"ton mot : {_myCurrentString}. Tu as {_myCurrentchance} / {_chancesTotal} chances. Tu as joué : {GetStringUserLetters(_userLetters)}.");
					Console.WriteLine(_drawingASCII.GetDrawingAtIndex(_myCurrentchance));

					string input = Console.ReadLine();

					Regex rx = new Regex(@"^[a-zA-Zéèêëçàâîïüô'-]");

					if (rx.IsMatch(input))
					{
						input = input.ToLower();
						char firstChar = input[0];

						if (_myWord.Contains(firstChar))
						{
							if (!_userLetters.Contains(firstChar))
							{
								StringBuilder sb = new StringBuilder(_myCurrentString);

								for (int i = 0; i < _myWord.Length; i++)
								{
									if (_myWord[i] == firstChar)
									{
										sb[i] = firstChar;
									}
								}

								_myCurrentString = sb.ToString();
								
								_userLetters.Add(firstChar);
							}
						}
						else
						{
							if (!_userLetters.Contains(firstChar))
							{
								_userLetters.Add(firstChar);

								_myCurrentchance++;
							}
						}
						// sinon rien

						
						// Test de victoire
						if (_myCurrentString == _myWord)
						{
							Console.WriteLine($"Gagné. Tu as trouvé, c'est le mot {_myWord}.");
							HasPressedEnter();
							break;
						}
					}
				}

				// Tests de défaite. Ici, j'ai perdu : nombre coups perso == chances total
				if(_myCurrentchance >= _chancesTotal)
				{
					Console.WriteLine("Perdu. Tu as joué tous tes coups. Tu es pendu-e.");
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine(_drawingASCII.GetDrawingAtIndex(_myCurrentchance));
					Console.ResetColor();
					HasPressedEnter();
				}

			}
		}


		/// <summary>
		/// Attendre que l'utilisateur ait appuyé sur Entrée.
		/// </summary>
		private void HasPressedEnter()
		{
			_hasPressedEnter = false;
			while (!_hasPressedEnter)
			{
				if (Console.ReadKey().Key == ConsoleKey.Enter)
				{
					_hasPressedEnter = true;
				}
			}
		}
		
		/// <summary>
		/// Démarrage du  jeu avec des infos pour l'utilisateur.
		/// </summary>
		private void Introduction()
		{
			Console.Clear();

			Console.WriteLine("Bonjour, taper Entrée pour continuer.");
			HasPressedEnter();

			Console.WriteLine("Je vais choisir un mot au hasard dans une liste.");
			HasPressedEnter();

			Random random = new Random();
			_myWord = _words[random.Next(0, _words.Length)];

			string tip = string.Empty;
			if (_showMyWord)
			{
				tip = $"({_myWord})";
			}

			if (_showMyWord) Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($"Voilà, j'ai choisis ! {tip}");
			if (_showMyWord) Console.ResetColor();
			HasPressedEnter();

			Console.WriteLine("On commence à jouer. Entre une lettre, tiret, accent... pour trouver le mot.");

			_myCurrentchance = 0;
			_myCurrentString = SetCurrentString(_myWord.Length);
			_userLetters.Clear();

			HasPressedEnter();
		}
		
		/// <summary>
		/// Fabriquer la chaîne de caractère du joueur.
		/// </summary>
		/// <param name="length">Nombre de caractères</param>
		/// <returns>Chaîne de caractères "*". </returns>
		private string SetCurrentString(int length)
		{
			StringBuilder sb = new StringBuilder();

			for (int i = 0; i < length; i++)
			{
				sb.Append("*");
			}

			return sb.ToString();
		}

		private string GetStringUserLetters(List<char> list)
		{
			StringBuilder sb = new StringBuilder();

			foreach(char c in list)
			{
				sb.Append(c);
				sb.Append(",");
			}

			if(sb.Length == 0)
			{
				return "rien";
			}
			else
			{
				return sb.ToString();
			}
		}

		#endregion
	}

}
