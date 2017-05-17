// The Interpreter object has an execution stack object; for every bytecode object, 
// the evaluation is done by calling Exec method on that object 

using System; 
using System.Collections; 
using System.Collections.Generic; 

class Interpreter {
        private Stack<Int32> _evalStack = new Stack<Int32>();
        public int Interpret(ByteCode[] byteCodes) {
                foreach(ByteCode byteCode in byteCodes) {
                        byteCode.Exec(_evalStack);
                }
                return _evalStack.Pop();
        }
}

abstract class ByteCode {
        abstract public void Exec(Stack<Int32> evalStack);
        abstract public void UnExec(Stack<Int32> evalStack);
}

class LDCI4S : ByteCode {
	int _val; 
	public LDCI4S(int arg) {
		_val = arg; 
	}
	public override void Exec(Stack<Int32> evalStack) {
		evalStack.Push(_val); 
	}
	public override void UnExec(Stack<Int32> evalStack) {
		evalStack.Pop(); 
	} 
} 

class ADD : ByteCode {
	public override void Exec(Stack<Int32> evalStack) {
		int lval = evalStack.Pop(); 
		int rval = evalStack.Pop();
		evalStack.Push(rval + lval); 
	}
} 

class ExprEval {
	public static void Main() {
		// ((10 + 20) + 30)
		// ByteCode []byteCodes = { new LDCI4S(0x0A), new LDCI4S(0x14), new ADD(), new LDCI4S(0x1E), new ADD() }; 
		get the bytecodes from the .exe or .dll file 
		foreach bytecode, call the factory method 
		ByteCode[]byteCodes =ByteCode.makeByteCodeCommands(ints); 
		var interpreter = new Interpreter(); 
		Console.WriteLine(interpreter.Interpret(byteCodes)); 
	}
} 

