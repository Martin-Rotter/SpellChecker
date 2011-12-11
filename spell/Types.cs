using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace System.Collections.Generic {
	public class Pair<TCar, TCdr> {

		public static int Count { get; protected set; }

		public TCar Car { get; set; }
		public TCdr Cdr { get; set; }

		public Pair(TCar Car = default(TCar), TCdr Cdr = default(TCdr)) {
			Count++;
			this.Car = Car;
			this.Cdr = Cdr;
		}

		public Pair<TCdr, TCar> Reverse() {
			return new Pair<TCdr, TCar>(Cdr, Car);
		}

		~Pair() {
			Count--;
		}

		public override string ToString() {
			return String.Format("Car: {0} Cdr: {1}", this.Car.ToString(), this.Cdr.ToString());
		}
	}
}
