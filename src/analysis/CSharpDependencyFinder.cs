using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CodeMaker.Analysis
{

class CSharpDependencyFinder
{
	public CSharpDependencyFinder()
	{
		
	}
	
	public List<string> findDependencies(string rawCode)
	{
		List<string> dependencies = new List<string>();
		
		Regex tokenizer = new Regex("(?<=using ).+(?=;)");
		MatchCollection tokenMatches = tokenizer.Matches(rawCode);
		
		foreach (Match match in tokenMatches)
		{
			dependencies.Add(match.Value);
		}
		
		return dependencies;
	}
}

}