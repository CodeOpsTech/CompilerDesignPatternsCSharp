// This code targets two platforms: JVM and DOTNET
// This code uses strategy pattern 

using System; 

abstract class Target { 
	public abstract void GenCode(Constant constant); 
	public abstract void GenCode(Plus plus); 
	public abstract void GenCode(Mult mult); 
}

class JVMTarget : Target { 
	public override void GenCode(Constant constant) { 
		Console.WriteLine("bipush " + constant.GetValue()); 
	}
	public override void GenCode(Plus plus) { 
		Console.WriteLine("iadd"); 
	}
	public override void GenCode(Mult mult) { 
		Console.WriteLine("imul"); 
	}
} 

class DotNetTarget : Target { 
	public override void GenCode(Constant constant) {
		Console.WriteLine("ldarg " + constant.GetValue());
	}
	public override void GenCode(Plus plus) { 
		Console.WriteLine("add"); 
	}
	public override void GenCode(Mult mult) { 
		Console.WriteLine("mul"); 
	}
} 

abstract class Expr {
	protected static Target _target  = new JVMTarget(); 
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
	public int GetValue() { 
		return _val; 
	} 	
	public override void GenCode() {
		_target.GenCode(this); 
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
		_target.GenCode(this); 
	}
}

class Mult : Expr {
	private Expr _left, _right; 
	public Mult(Expr arg1, Expr arg2) {
		_left = arg1;
		_right = arg2; 	
	}
	public override void GenCode() {
		_left.GenCode();
		_right.GenCode(); 
		_target.GenCode(this); 
	}
}

class ExprEval {
	public static void Main() {
		Expr.SetTarget(new JVMTarget()); 
		// ((10 * 20) + 30)  
		Expr expr = new Plus(
				new Mult(
					new Constant(10), 
					new Constant(20)), 
				new Constant(30)); 	
		expr.GenCode(); 
	}
}
