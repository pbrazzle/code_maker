using System;
using System.Windows.Forms;
using System.IO;

namespace CodeMaker
{

class CodeMaker
{
	private static CodeMakerForm mainForm;
	
	private static bool textChanged;
	private static string currentFile;
	
	static CodeMaker()
	{
		textChanged = false;
		currentFile = "";
	}
	
	public static void setForm(CodeMakerForm form)
	{
		mainForm = form;
	}
	
	public static void buildProject(object sender, EventArgs e)
	{
		
	}
	
	public static void textChangedEvent(object sender, EventArgs e)
	{
		textChanged = true;
	}
	
	public static void runProject(object sender, EventArgs e)
	{
		
	}
	
	/*
	private void buildProject(object sender, EventArgs e)
	{
		Process buildProcess = new Process();
		buildProcess.StartInfo.FileName = "C:\\code\\code_maker\\build.bat";
		buildProcess.StartInfo.CreateNoWindow = true;
		buildProcess.StartInfo.UseShellExecute = false;
		buildProcess.StartInfo.RedirectStandardOutput = true;
		
		buildProcess.OutputDataReceived += new DataReceivedEventHandler(buildOutputHandler);
		buildProcess.Start();
		buildProcess.BeginOutputReadLine();
		buildProcess.WaitForExit();
	}
	
	private void buildOutputHandler(object sender, DataReceivedEventArgs e)
	{
		//Trace.WriteLine(e.Data);
		this.BeginInvoke(new MethodInvoker(() =>
		{
			if (e.Data != null)
			{
				terminalTextBox.AppendText(e.Data+'\n');
			}	
		}));
	}
	
	private void runProject(object sender, EventArgs e)
	{
		Process.Start("C:\\code\\code_maker\\bin\\CodeMaker.exe");
	}
	*/
	
	public static void openFile(object sender, EventArgs e)
	{
		using (OpenFileDialog fileDialog = new OpenFileDialog())
		{
			fileDialog.InitialDirectory = "C:\\code\\";
			if (currentFile != "")
			{
				fileDialog.InitialDirectory = currentFile;
			}
			
			if (fileDialog.ShowDialog() == DialogResult.OK)
			{
				currentFile = fileDialog.FileName;
				mainForm.setEditorContents(File.ReadAllText(currentFile));
			}
		}
		if (textChanged)
		{
			MessageBoxButtons buttons = MessageBoxButtons.YesNo;
			DialogResult result = MessageBox.Show("Save current work?", "Open file", buttons);
			if (result == DialogResult.Yes)
			{
				CodeMaker.saveFile(null, new EventArgs());
			}
			textChanged = false;
		}
	}
	
	public static void saveFile(object sender, EventArgs e)
	{
		if (currentFile != "")
		{
			File.WriteAllText(currentFile, mainForm.getEditorContents());
		}
	}
	
	public static void saveFileAs(object sender, EventArgs e)
	{
		using (SaveFileDialog fileDialog = new SaveFileDialog())
		{
			fileDialog.InitialDirectory = "C:\\code\\";
			if (currentFile != "")
			{
				fileDialog.InitialDirectory = currentFile;
			}
			
			if (fileDialog.ShowDialog() == DialogResult.OK)
			{
				currentFile = fileDialog.FileName;
				File.WriteAllText(currentFile, mainForm.getEditorContents());
			}
		}
	}
	
	public static void createNewProject(object sender, EventArgs e)
	{
		ProjectCreationForm newProjectForm = new ProjectCreationForm();
		newProjectForm.ShowDialog();
	}
}

}