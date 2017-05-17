using System; 

class Register {
	private int _index; 
	private Register(int index) {
		_index = index;
	} 
	public static Register GetRegister(int index) {
		return new Register(index); 
	} 
	public int GetIndex() {
		return _index;
	} 
} 

class RegisterAllocator {
	private static int _registerIndex = 0; 
	public static Register GetNextRegister() {
		return Register.GetRegister(_registerIndex++); 
	}
}

abstract class Expr {
	public abstract Register GenCode(); 
}

class Constant : Expr {
	int val; 
	public Constant(int arg) {
		val = arg; 
	}
	public override Register GenCode() {
		Register targetRegister = RegisterAllocator.GetNextRegister(); 
		Console.WriteLine("const/16 v{0}, #int {1}", targetRegister.GetIndex(), val);
		return targetRegister; 
	}
}

class Plus : Expr {
	private Expr left, right; 
	public Plus(Expr arg1, Expr arg2) {
		left = arg1;
		right = arg2; 	
	}
	public override Register GenCode() {
		Register firstRegister = left.GenCode();
		Register secondRegister = right.GenCode(); 
		Register targetRegister = RegisterAllocator.GetNextRegister(); 
		Console.WriteLine("add-int v{0}, v{1}, v{2}", targetRegister.GetIndex(), firstRegister.GetIndex(), secondRegister.GetIndex()); 
		return targetRegister; 
	}
}

class GenCodeRegisterIL {
	public static void Main() {
		// ((10 + 20) + 30)  
		Expr expr = new Plus(
				new Plus(
					new Constant(10), 
					new Constant(20)), 
				new Constant(30)); 	
		expr.GenCode(); 
	}
}
