using System.Collections.ObjectModel;

namespace CodeMaker.Analysis
{

class CodeToken
{
	private const string specials = ";:(){}.";
	private readonly ReadOnlyCollection<string> keywords = new ReadOnlyCollection<string>(new[]
	{
			"abstract", "as", "base", "bool", "break","byte", "case", "catch", "char", "checked",
			"class", "const", "continue","decimal","default","delegate","do","double","else","event",
			"explicit","extern","false","finally","fixed","float","for","foreach","goto","if",
			"implicit","in","int","interface","internal","is","lock","long","namespace","new",
			"null","object","operator","out","override","params","private","protected","public","readonly",
			"ref","return","sbyte","sealed","short","sizeof","stackalloc","static","string","struct",
			"switch","this","throw","true","try","typeof","uint","ulong","unchecked","unsafe",
			"ushort","using","virtual","void","volatile","while"
	});
	
	public enum TokenType
	{
		KEYWORD,
		SPECIAL,
		SYMBOL
	}
	private string val;
	private TokenType type;
	
	public CodeToken(string v)
	{
		val = v;
		decideType();
	}
	
	public string getValue() { return val; }
	
	public TokenType getType() { return type; }
	
	public string asString()
	{
		string typeString = "";
		
		switch (type)
		{
			case TokenType.KEYWORD:
			typeString = "Keyword";
			break;
			
			case TokenType.SPECIAL:
			typeString = "Special";
			break;
			
			case TokenType.SYMBOL:
			typeString = "Symbol";
			break;
		}
		
		return typeString + ": " + val;
	}
	
	private void decideType()
	{
		if (specials.Contains(val))
		{
			type = TokenType.SPECIAL;
			return;
		}
		
		foreach (string keyword in keywords)
		{
			if (val == keyword)
			{
				type = TokenType.KEYWORD;
				return;
			}
		}
		
		type = TokenType.SYMBOL;
	}
}

}