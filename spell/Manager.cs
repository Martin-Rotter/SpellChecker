using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using SpellChecK.Core;


namespace SpellChecK.Gui {

	public class Manager {

		public string VocabularyPath { get; private set; }
		public string InputPath { get; private set; }

		public Manager() {
			Welcome();
			VocabularyPath = InputPath = string.Empty;
		}

		// identifikátory pro příkazy
		public const string _CMD_QUIT		= "quit";
		public const string _CMD_CLEAR		= "clear";
		public const string _CMD_SCAN		= "scan";

		public void Exec() {
			Console.WriteLine("\nSlovník obsahuje: {0} slov.", SpellCheck.LoadWords(VocabularyPath).Count);
			Console.WriteLine();
			string cmd = string.Empty;
			do {
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.Write("> ");
				Console.ForegroundColor = ConsoleColor.Green;
				cmd = Console.ReadLine();
				ProcessCommand(cmd);
			} while (cmd != Manager._CMD_QUIT);
		}

		public void Welcome() {
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine(string.Format("Primitive SpellCheck\n" +
											"Made by: {0}\n\n", "Martin Rotter\n\n" +
											"Pro spuštění skanování souboru napište scan a odENTERujte.\nPro ukončení napište quit."));
		}

		public void SetVocabulary(string path) {
			VocabularyPath = path;
		}

		public void SetInput(string path) {
			InputPath = path;
		}

		public int SetValue(string prompt) {
			Console.Write(prompt);
			string result = Console.ReadLine();
			try {
				int.Parse(result);
			}
			catch (Exception) {
				return SetValue(prompt);
			}
			return int.Parse(result);
		}

		public void ScanAll() {
			int bestGuesses = SetValue("Počet možností, které chcete zobrazit u slov, jenž nejsou ve slovníku: ");
			Console.WriteLine("\n");
			List<string> vocabulary = SpellCheck.LoadWords(VocabularyPath);
			List<string> words = SpellCheck.LoadWords(InputPath);
			foreach (var word in words) {
				// přesné slovo není ve slovníku
				if (SpellCheck.IsInVocabulary(word.ToLower(), vocabulary) == false) {
					List<Pair<int, string>> result = SpellCheck.BestDistance(word.ToLower(), vocabulary, bestGuesses);
					foreach (Pair<int, string> item in result)
						Console.WriteLine("slovo: {0} | edit. vzdálenost: {1} od slova: {2}", word.ToLower(), item.Car, item.Cdr);
					Console.WriteLine("===================");
				}
			}
		}

		public void ProcessCommand(string command) {
			switch (command) {
				case Manager._CMD_QUIT:
					Console.WriteLine("Aplikace byla ukončena.");
					break;
				case Manager._CMD_SCAN:
					ScanAll();
					break;
				case Manager._CMD_CLEAR:
					Console.Clear();
					break;
				default:
					Console.WriteLine("Neznámý příkaz.");
					break;
			}
		}
	};

}
