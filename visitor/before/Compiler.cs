// This code uses conditional checks (conditional-type checking code)
// Use visitor pattern to cleanly separate the domain logic from 
// code generation logic 
using System; 

enum Target { JVM, DOTNET }

abstract class Expr {
	public static Target _target  = Target.JVM; 
	public static void SetTarget(Target target) {
		_target = target; 	
	}
	public abstract void GenCode(); 
}

class Constant : Expr {
	int _val; 
	public Constant(int arg) {
		_val = arg; 
	}
	public override void GenCode() {
		if(_target == Target.JVM) {
			Console.WriteLine("bipush " + _val); 
		}
		else { // DOTNET 
			Console.WriteLine("ldc.i4.s " + _val);
		}
	}
}

class Plus : Expr {
	private Expr _left, _right; 
	public Plus(Expr arg1, Expr arg2) {
		_left = arg1;
		_right = arg2; 	
	}
	public override void GenCode() {
		_left.GenCode();
		_right.GenCode(); 
		if(_target == Target.JVM) { 
			Console.WriteLine("iadd"); 
		}
		else { // DOTNET 
			Console.WriteLine("add"); 
		}
	}
}

class Sub : Expr {
	private Expr _left, _right; 
	public Sub(Expr arg1, Expr arg2) {
		_left = arg1;
		_right = arg2; 	
	}
	public override void GenCode() {
		_left.GenCode();
		_right.GenCode(); 
		if(_target == Target.JVM) { 
			Console.WriteLine("isub"); 
		}
		else { // DOTNET 
			Console.WriteLine("sub"); 
		}	
	}
}

class ExprEval {
	public static void Main() {
		Expr.SetTarget(Target.DOTNET); 
		// (10 + (20 - 30))  
		Expr expr = new Plus(
				new Constant(10), 
				new Sub(
					new Constant(20), 
					new Constant(30))); 	
		expr.GenCode(); 
	}
}
