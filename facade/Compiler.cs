using System.IO;  

class Scanner {
	public Scanner(StreamReader input) { } 
} 

class BytecodeStream { 
} 

class Node {} 

class ProgramNode {
	public void Traverse(RISCCodeGenerator generator) { } 
} 

class ProgramNodeBuilder { 
	public ProgramNode GetRootNode() { return null; } 
} 

class Parser {
	public void Parse(Scanner scanner, ProgramNodeBuilder builder) { } 
} 

class RISCCodeGenerator { 
	public RISCCodeGenerator(BytecodeStream output) { } 
} 

class Compiler {
        public Compiler() {} 
 
	public static void Main() {
		new Compiler().Compile(null, null); 
	}    
        public virtual void Compile(StreamReader input, BytecodeStream output) {
 	       	Scanner scanner = new Scanner(input);
        	var builder = new ProgramNodeBuilder();
	        Parser parser = new Parser();
    
	        parser.Parse(scanner, builder);
    
        	RISCCodeGenerator generator = new RISCCodeGenerator(output);
	        ProgramNode parseTree = builder.GetRootNode();
        	parseTree.Traverse(generator);
	}
}
