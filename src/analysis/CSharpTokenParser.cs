using System.Collections.Generic;

namespace CodeMaker.Analysis
{

class CSharpTokenParser
{
	public CSharpTokenParser()
	{
		
	}
	
	public List<List<CodeToken>> parseStatements(List<CodeToken> tokens)
	{
		List<List<CodeToken>> statements = new List<List<CodeToken>>();
		
		List<CodeToken> statement = new List<CodeToken>();
		
		foreach (CodeToken token in tokens)
		{
			if (token.getType() == CodeToken.TokenType.END_STATEMENT || token.getType() == CodeToken.TokenType.CLOSE_BLOCK || token.getType() == CodeToken.TokenType.OPEN_BLOCK)
			{
				if (statement.Count > 0)
				{
					statements.Add(new List<CodeToken>(statement));
					statement.Clear();			
				}
				continue;
			}
			statement.Add(token);
		}
		
		return statements;
	}		
}

}