using System.Diagnostics;

namespace CodeMaker
{

class CodeMakerProject
{
	private string path;
	private string name;
	
	public event DataReceivedEventHandler buildStandardOutput;
	
	public CodeMakerProject(string p, string n)
	{
		path = p;
		name = n;
	}
	
	public void run()
	{
		Process.Start(path+"\\bin\\"+name+".exe");
	}
	
	public void build()
	{
		Process buildProcess = new Process();
		buildProcess.StartInfo.FileName = path+"\\build.bat";
		buildProcess.StartInfo.CreateNoWindow = true;
		buildProcess.StartInfo.UseShellExecute = false;
		buildProcess.StartInfo.RedirectStandardOutput = true;
		
		buildProcess.OutputDataReceived += (sender, e) => buildStandardOutput.Invoke(sender, e);
		buildProcess.Start();
		buildProcess.BeginOutputReadLine();
		buildProcess.WaitForExit();
	}
}

}