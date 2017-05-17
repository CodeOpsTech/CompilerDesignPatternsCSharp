using System; 
using System.Collections.Generic; 

abstract class Expr {
	public abstract void GenCode(); 
}

class Constant : Expr {
	int _val; 
	public Constant(int val) {
		_val = val; 
	} 
	public override void GenCode() {
		Console.WriteLine("ldc.i4.s " + _val);
	}
}

class BinaryExpr : Expr {
	private Expr _left, _right; 
	public BinaryExpr(Expr arg1, Expr arg2) {
		_left = arg1;
		_right = arg2; 	
	}
	public override void GenCode() {
		_left.GenCode();
		_right.GenCode(); 
	}
}

class Plus : BinaryExpr {
	public Plus (Expr arg1, Expr arg2) : base(arg1, arg2) { 
	}
	public override void GenCode() { 
		base.GenCode();
		Console.WriteLine("add"); 
	}
}

class Multiply : BinaryExpr {
	public Multiply(Expr arg1, Expr arg2) : base(arg1, arg2) { }

	public override void GenCode() { 
		base.GenCode(); 
		Console.WriteLine("mul"); 
	}
}

class Compiler {
	public static void Main() {
		// ((10 * 20) + 10)  
		Expr expr = new Plus(
				new Multiply(
					new Constant(10), 
					new Constant(20)), 
				new Constant(30)); 	
		expr.GenCode(); 
	}
}
