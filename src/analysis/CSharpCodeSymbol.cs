namespace CodeMaker.Analysis
{

class CSharpCodeSymbol
{
	public enum SymbolType
	{
		NAMESPACE,
		CLASS,
		VARIABLE,
		DEPENDENCY
	}
	
	private string name;
	private SymbolType type;
	
	public CSharpCodeSymbol(string n, SymbolType t)
	{
		name = n;
		type = t;
	}
	
	public string getName() { return name; }
	
	public SymbolType getType() { return type; }
}

}