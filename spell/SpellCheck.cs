using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;


namespace SpellChecK.Core {
	public class SpellCheck {

		public readonly int _PEN_DELETE;
		public readonly int _PEN_INSERT;
		public readonly int _PEN_SWAP;

		public SpellCheck(int penDelete, int penInsert, int penSwap) {
			_PEN_DELETE	= penDelete;
			_PEN_INSERT	= penInsert;
			_PEN_SWAP	= penSwap;
		}


		public static bool IsInVocabulary(string word, List<string> vocabulary) {
			foreach (var vocabularyItem in vocabulary) {
				if (word == vocabularyItem) return true;
			}
			return false;
		}

		public static List<Pair<int, string>> BestDistance(string word, List<string> vocabulary, int howMany) {
			int temp;
			List<Pair<int, string>> result = new List<Pair<int, string>>(howMany);
			foreach (string value in vocabulary) {
				temp = TwoWordsDistance(word, value);
				if (result.Count >= howMany) {
					result.Sort(Compare);
					if (result.Last().Car > temp) result[result.Count - 1] = new Pair<int, string>(temp, value);
				}
				else
					result.Add(new Pair<int, string>(temp, value));

			}
			return result;
		}

		public static int Compare(Pair<int, string> left, Pair<int, string> right) {
			if (left.Car < right.Car) return -1;
			return 1;
		}

		// firstWord je z vět, secondWord ze slovníku třeba
		public static int TwoWordsDistance(string firstWord, string secondWord) {
			int[,] matrix = new int[firstWord.Length + 1, secondWord.Length + 1];
			for (int i = 0; i <= firstWord.Length; i++) {
				matrix[i, 0] = i;
			}
			for (int i = 0; i <= secondWord.Length; i++) {
				matrix[0, i] = i;
			}
			for (int j = 1; j <= secondWord.Length; j++) {
				for (int i = 1; i <= firstWord.Length; i++) {
					if (firstWord[i - 1] == secondWord[j - 1])
						matrix[i, j] = matrix[i - 1, j - 1];
					else
						matrix[i, j] = Minimum(new List<int>() { matrix[i - 1, j] + 1, matrix[i, j - 1] + 1, matrix[i - 1, j - 1] + 1 });
				}
			}
			return matrix[firstWord.Length, secondWord.Length];
		}

		public static int Minimum(List<int> ints) {
			int result = int.MaxValue;
			foreach (var value in ints) {
				if (value < result) result = value;
			}
			return result;
		}

		public static List<string> LoadWords(string path) {
			List<string> words = new List<string>();
			try {
				using (StreamReader sr = new StreamReader(path)) {
					int temp;
					string tmp = string.Empty;
					while ((temp = sr.Read()) != -1) {
						if (temp == ' ' || temp == '.' || temp == ',' || temp == '\r' || temp == '\n') {
							if (tmp != "")
								words.Add(tmp);
							tmp = string.Empty;
						}
						else
							tmp += (char)temp;
					}
					return words;
				}
			}
			catch (System.IO.FileNotFoundException) {
				Console.WriteLine("Soubor " + path + " nenalezen!!!");
			}
			return words;
		}
	}
}
