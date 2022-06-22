using System.Collections.ObjectModel;

namespace CodeMaker.Analysis
{

class CodeToken
{
	private const string specials = ":";
	
	private const string blockOpeners = "{";
	private const string blockClosers = "}";
	private const string delimiters = "()<>";
	
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
		OPEN_BLOCK,
		CLOSE_BLOCK,
		END_STATEMENT,
		COMMENT,
		LITERAL,
		SYMBOL,
		DELIMITER
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
			
			case TokenType.COMMENT:
			typeString = "Comment";
			break;
			
			case TokenType.LITERAL:
			typeString = "Literal";
			break;
			
			case TokenType.END_STATEMENT:
			typeString = "End Statement";
			break;
			
			case TokenType.OPEN_BLOCK:
			typeString = "Open Block";
			break;
			
			case TokenType.CLOSE_BLOCK:
			typeString = "Close Block";
			break;
			
			case TokenType.DELIMITER:
			typeString = "Delimiter";
			break;
		}
		
		return typeString + ": " + val;
	}
	
	private void decideType()
	{
		if (val.StartsWith("//"))
		{
			type = TokenType.COMMENT;
			return;
		}
		
		if (val.StartsWith("\""))
		{
			type = TokenType.LITERAL;
			return;
		}
		
		if (val == ";")
		{
			type = TokenType.END_STATEMENT;
			return;
		}
		
		if (specials.Contains(val))
		{
			type = TokenType.SPECIAL;
			return;
		}
		
		if (blockOpeners.Contains(val))
		{
			type = TokenType.OPEN_BLOCK;
			return;
		}
		
		if (blockClosers.Contains(val))
		{
			type = TokenType.CLOSE_BLOCK;
			return;
		}
		
		if (delimiters.Contains(val))
		{
			type = TokenType.DELIMITER;
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