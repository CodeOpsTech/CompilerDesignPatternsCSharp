// This code users Builder Pattern for creating expression trees 

using System; 
using System.Collections.Generic; 

abstract class Expr {
	public abstract void GenCode(); 
}

class ExprBuilder {
        private Expr expr = null;
        public ExprBuilder() {}
        public ExprBuilder Const(int arg) {
                expr = Constant.Make(arg);
                return this;
        }
        public ExprBuilder Plus(int arg) {
                expr = new Addition(expr, Constant.Make(arg));
                return this;
        }
        public ExprBuilder Mult(int arg) {
                expr = new Multiplication(expr, Constant.Make(arg));
                return this;
        }
        public Expr Build() {
                return expr;
        }
}

class Constant : Expr {
	int _val; 
	private static Dictionary<int, Constant> _pool = new Dictionary<int, Constant>(); 
	private Constant(int val) {
		_val = val; 
	} 
	public static Constant Make(int arg) {
		if(!_pool.ContainsKey(arg)) {
			_pool[arg] = new Constant(arg); 
		}
		return _pool[arg];
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

class Addition : BinaryExpr {
	public Addition(Expr arg1, Expr arg2) : base(arg1, arg2) { 
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
	public Multiplication(Expr arg1, Expr arg2) : base(arg1, arg2) { 
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
                // ((10 * 20) + 30)
                Expr expr = new ExprBuilder().Const(10).Mult(20).Plus(30).Build();
                expr.GenCode();
        }
}

