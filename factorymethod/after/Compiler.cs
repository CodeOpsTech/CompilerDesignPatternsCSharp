// This code uses factory methods for creating expression objects 
using System; 
using System.Collections.Generic; 

abstract class Expr {
	public abstract void GenCode(); 
}

class Constant : Expr {
	int _val; 
	private Constant(int val) {
		_val = val; 
	} 
	public static Constant Make(int arg) {
		return new Constant(arg); 
	}
	public override void GenCode() {
		Console.WriteLine("ldc.i4.s " + _val);
	}
}

abstract class BinaryExpr : Expr {
	private Expr _left, _right; 
	protected BinaryExpr(Expr arg1, Expr arg2) {
		_left = arg1;
		_right = arg2; 	
	}
	public override void GenCode() {
		_left.GenCode();
		_right.GenCode(); 
	}
}

class Addition : BinaryExpr {
	private Addition(Expr arg1, Expr arg2) : base(arg1, arg2) { 
	}
	public static Expr Make(Expr left, Expr right) {
		return new Addition(left, right); 
	} 
	public override void GenCode() { 
		base.GenCode();
		Console.WriteLine("add"); 
	}
}

class Multiplication : BinaryExpr {
	private Multiplication(Expr arg1, Expr arg2) : base(arg1, arg2) { 
	}
	public static Expr Make(Expr left, Expr right) {
		return new Multiplication(left, right);
	} 
	public override void GenCode() { 
		base.GenCode(); 
		Console.WriteLine("mul"); 
	}
}

class Compiler {
	public static void Main() {
		// ((10 * 20) + 10)  
		Expr expr = Addition.Make(
				Multiplication.Make(
					Constant.Make(10), 
					Constant.Make(20)), 
				Constant.Make(10)); 	
		expr.GenCode(); 
	}
}
