using System; 

abstract class Expr {
	public abstract int Interpret(); 
}

class Constant : Expr {
	int _val; 
	public Constant(int arg) {
		_val = arg; 
	}
	public override int Interpret() {
		return _val; 
	}
}

class Plus : Expr {
	private Expr left, right; 
	public Plus(Expr arg1, Expr arg2) {
		left = arg1;
		right = arg2; 	
	}
	public override int Interpret() {
		return left.Interpret() + right.Interpret();
	}
}

class Interpret {
	public static void Main() {
		// ((10 + 20) + 30)  
		Expr expr = new Plus(
				new Plus(
					new Constant(10), 
					new Constant(20)), 
				new Constant(30)); 	
		Console.WriteLine(expr.Interpret()); 
	}
}
