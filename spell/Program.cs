using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


using SpellChecK.Core;
using SpellChecK.Gui;


namespace spell {
	class Program {
		static void Main(string[] args) {
			Manager manager = new Manager();
			SpellCheck spellcheck = new SpellCheck(1, 1, 1);
			if (args.Length == 2) {
				manager.SetInput(args[1]);
				manager.SetVocabulary(args[0]);
				manager.Exec();
			}
			else
				Console.WriteLine("Program musí být spuštěn se dvěma parametry: program <slovník.txt> <text.txt>");
		}

	}
}
