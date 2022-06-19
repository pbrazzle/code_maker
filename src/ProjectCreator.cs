using System;
using System.IO;

namespace CodeMaker
{
class ProjectCreator
{
	private string buildFileContents = 
	"@echo off\n"
	+"csc /out:bin\\$PROJ_NAME$.exe src\\$PROJ_NAME$.cs\n";
	
	private string helloWorldFileContents =
	"namespace $PROJ_NAME$\n"
	+"{\n"
	+"	class $PROJ_NAME$\n"
	+"	{\n"
	+"		static void Main(string[] args)\n"
	+"		{\n"
	+"			System.Console.WriteLine(\"Hello World!\");\n"
	+"		}\n"
	+"	}\n"
	+"}\n";
	
	public ProjectCreator()
	{
		
	}
	
	public void createProject(string name)
	{
		Directory.CreateDirectory("C:\\code\\"+name);
		Directory.CreateDirectory("C:\\code\\"+name+"\\src");
		Directory.CreateDirectory("C:\\code\\"+name+"\\bin");
		
		string buildFile = buildFileContents.Replace("$PROJ_NAME$", name);
		StreamWriter writer = new StreamWriter("C:\\code\\"+name+"\\build.bat");
		writer.Write(buildFile);
		writer.Close();
		
		string codeFile = helloWorldFileContents.Replace("$PROJ_NAME$", name);
		writer = new StreamWriter("C:\\code\\"+name+"\\src\\"+name+".cs");
		writer.Write(codeFile);
		writer.Close();
	}
}
}