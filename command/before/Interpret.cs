// This code implements a tiny interpreter for a couple of 
// bytecode instructions in CIL (Common Intermediate Language)
// This code is written in procedural style - refactor by applying 
// Object Oriented design principles (and optionally design patterns) 

using System;
using System.Collections.Generic;

[Flags] 
enum BYTECODE : byte { LDCI4S = 0x10, ADD = 0x60 }; 

class Interpreter { 
	private static Stack<int> _executionStack = new Stack<int>(); 
	private static int Interpret(byte[] byteCodes) {
		int pc = 0;
		while(pc < byteCodes.Length) { 
		switch(byteCodes[pc++]) {
		case (byte) BYTECODE.ADD: 
			_executionStack.Push(_executionStack.Pop() + _executionStack.Pop());  break; 	
		case (byte) BYTECODE.LDCI4S: 
			_executionStack.Push(byteCodes[pc++]); break;  
		} 
		}	  
		return _executionStack.Pop(); 
	} 

	public static void Main() {
		// ((10 + 20) + 30)
		byte []byteCodes = { (byte) BYTECODE.LDCI4S, 0x0A, (byte) BYTECODE.LDCI4S, 0x14, (byte) BYTECODE.ADD, (byte) BYTECODE.LDCI4S, 0x1E, (byte) BYTECODE.ADD }; 
		int result = Interpreter.Interpret(byteCodes); 
		Console.WriteLine("Execution result is: {0}", result); 
	} 
}
