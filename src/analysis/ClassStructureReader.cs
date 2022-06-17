using System.Collections.Generic;
using System.IO;
using System;
using System.Text.RegularExpressions;

namespace CodeMaker.Analysis
{

class ClassStructureReader
{
	public ClassStructureReader() { }
	
	public ClassStructureInfo readProject(string path)
	{
		string[] codeFileNames = findAllCodeFiles(path);
		List<ClassInfo> classInfoList = findAllClasses(codeFileNames);
		classInfoList = linkChildren(classInfoList);
		return new ClassStructureInfo(classInfoList);
	}
	
	private string[] findAllCodeFiles(string path)
	{
		return Directory.GetFiles(path, "*.cs", SearchOption.AllDirectories);
	}
	
	private List<ClassInfo> findAllClasses(string[] codeFileNames)
	{
		List<ClassInfo> classInfoList = new List<ClassInfo>();
		foreach(string filename in codeFileNames)
		{
			Console.WriteLine(filename);
			string fileContents = File.ReadAllText(filename);
			
			Regex findClasses = new Regex("(?<=\\bclass\\s+)[^\"]*?(?={)");
			MatchCollection matches = findClasses.Matches(fileContents);
			
			foreach (Match match in matches)
			{
				Console.WriteLine(match.Value);

				int index = match.Index;
				string classNameString = match.Value;
				
				string className = classNameString.Split(':')[0].Trim();
				string parentClassName = "";
				
				Console.WriteLine(className);
				
				if (classNameString.Split(':').Length > 1)
				{
					parentClassName = classNameString.Split(':')[1].Trim();
				}
				
				ClassInfo newClass = new ClassInfo(className, parentClassName);
				if (!classInfoList.Contains(newClass))
				{
					classInfoList.Add(newClass);
				}
			}
		}
		return classInfoList;
	}
	
	private List<ClassInfo> linkChildren(List<ClassInfo> classInfoList)
	{
		int lastLength = -1;
		while (lastLength != classInfoList.Count)
		{
			lastLength = classInfoList.Count;
			foreach (ClassInfo classInfo in classInfoList)
			{
				foreach (ClassInfo childClassInfo in classInfoList)
				{
					if (classInfo.isParentOf(childClassInfo))
					{
						classInfo.addChild(childClassInfo);
						classInfoList.Remove(childClassInfo);
					}
				}
			}
		}
		
		return classInfoList;
	}
}

}