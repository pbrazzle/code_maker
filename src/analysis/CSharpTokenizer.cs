using System.Text.RegularExpressions;
using System.Collections.Generic;
using System;

namespace CodeMaker.Analysis
{

class CSharpTokenizer
{
	public CSharpTokenizer() { }
	
	public List<CodeToken> getTokenList(string rawCode)
	{
		List<CodeToken> tokenList = new List<CodeToken>();
		
		Regex tokenizer = new Regex("[\\w.]+|[{}()<>;]|(\".+?[^\\\\]\")|(\\/\\/.+)");
		MatchCollection tokenMatches = tokenizer.Matches(rawCode);
		
		foreach (Match match in tokenMatches)
		{
			tokenList.Add(new CodeToken(match.Value));
		}
		
		return tokenList;
	}
}

}