using System;
using System.Collections.Generic;

namespace CodeMaker.Analysis
{

class CSharpDependencyFinder
{
	public CSharpDependencyFinder()
	{
		
	}
	
	public List<string> findDependencies(List<CodeToken> tokens)
	{
		List<string> dependencies = new List<string>();
		
		List<List<CodeToken>> statements = splitStatements(tokens);
		
		foreach (List<CodeToken> statement in statements)
		{
			//dependencies.Add(statementToString(statement));
			
			foreach (CodeToken token in statement)
			{
				if (token.getType() == CodeToken.TokenType.SYMBOL)
				{
					if (!dependencies.Contains(token.getValue()))
					{
						dependencies.Add(token.getValue());
					}
				}
			}
		}
		
		return dependencies;
	}
	
	private List<List<CodeToken>> splitStatements(List<CodeToken> tokens)
	{
		CSharpTokenParser parser = new CSharpTokenParser();
		List<List<CodeToken>> statements = parser.parseStatements(tokens);
		
		return statements;
	}
	
	private string statementToString(List<CodeToken> statement)
	{
		string state = "";
		
		foreach (CodeToken token in statement)
		{
			state += token.getValue() + " ";
		}
		
		return state;
	}
}

}