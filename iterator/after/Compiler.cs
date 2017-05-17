using System; 
using System.Collections.Generic; 

public abstract class Expr {
	public abstract void GenCode(); 
	public virtual Expr GetLeft() { return null; } 
	public virtual Expr GetRight() { return null; } 
} 

// foreach(Expr node in ExprIterator.Traverse(expr)) {
//                        node.GenCode();
//                }
class ExprIterator { 
	public static IEnumerable<Expr> Traverse(Expr node) {
		if(node != null) { 
			foreach(Expr left in Traverse(node.GetLeft())) {
				yield return left; 
			} 
			foreach(Expr right in Traverse(node.GetRight())) {
				yield return right; 
			} 
    			yield return node;
		} 
	}
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

abstract class BinaryExpr : Expr {
	private Expr _left, _right; 
	public BinaryExpr(Expr arg1, Expr arg2) {
		_left = arg1;
		_right = arg2; 	
	}
	public override Expr GetLeft() { 
		return _left; 
	} 
	public override Expr GetRight() { 
		return _right; 
	} 
}

class Plus : BinaryExpr {
	public Plus (Expr arg1, Expr arg2) : base(arg1, arg2) { 
	}
	public override void GenCode() { 
		Console.WriteLine("add"); 
	}
}

class Multiply : BinaryExpr {
	public Multiply(Expr arg1, Expr arg2) : base(arg1, arg2) { }

	public override void GenCode() { 
		Console.WriteLine("mul"); 
	}
}

class Compiler {
	public static void Main() {
		// ((10 * 20) + 30)  
		Expr expr = new Plus(
				new Multiply(
					new Constant(10), 
					new Constant(20)), 
					new Constant(30) 
				); 	
		foreach(Expr node in ExprIterator.Traverse(expr)) {  
			node.GenCode(); 
		} 
	}
}
