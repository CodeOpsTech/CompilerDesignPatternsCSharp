// This code works, but follows procedural approach
// Task: Convert it to Object Oriented approach and apply Composite pattern

using System;

class Expr {
	private Expr _left;
	private string _value;
	private Expr _right;
	public Expr(Expr left, string value, Expr right) {
		_left = left;
		_value = value;
		_right = right; 
	} 

	public void GenCode() {
		if(this == null) 
			return;
		if((_left == null) && (_right == null)) {
			Console.WriteLine("ldc.i4.s " + _value); 
		} 
		else { 	// its an intermediate node 
			_left.GenCode();
			_right.GenCode(); 
			switch(_value) {
			case "+": Console.WriteLine("add"); break; 
			case "-": Console.WriteLine("sub"); break; 
			case "*": Console.WriteLine("mul"); break; 
			case "/": Console.WriteLine("div"); break; 
			default: 
				Console.WriteLine("Not implemented yet!"); break; 
			}
		} 
	}
}

class Compiler { 
	public static void Main() {
		// ((10 * 20) + 30)  
		Expr expr = new Expr(
				new Expr(
					new Expr(null, "10", null), "*", new Expr(null, "20", null)), 
				     	"+", 
				     	new Expr(null, "30", null)); 
		expr.GenCode(); 
	} 
}

