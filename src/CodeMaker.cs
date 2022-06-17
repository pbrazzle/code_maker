using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

using CodeMaker.UI;

namespace CodeMaker
{

class CodeMaker
{
	private CodeMakerForm mainForm;
	
	private bool textChanged;
	private string currentFile;
	
	public CodeMaker()
	{
		textChanged = false;
		currentFile = "";
		
		mainForm = new CodeMakerForm();
		mainForm.OpenFile += () => openFile();
		mainForm.SaveFile += () => saveFile();
		mainForm.SaveFileAs += () => saveFileAs();
		mainForm.BuildProject += () => buildProject();
		mainForm.RunProject += () => runProject();
		mainForm.NewProject += () => createNewProject();
		mainForm.TextEditorChanged += () => textChangedEvent();
		Application.EnableVisualStyles();
		Application.Run(mainForm);
	}
	
	public void openFile()
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
				saveFile();
			}
			textChanged = false;
		}
	}
	
	[STAThread]
	public static void Main()
	{
		new CodeMaker();
	}
	
	public void setForm(CodeMakerForm form)
	{
		mainForm = form;
	}
	
	public void buildProject()
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
		mainForm.BeginInvoke(new MethodInvoker(() =>
		{
			if (e.Data != null)
			{
				mainForm.appendToTerminal(e.Data+'\n');
			}	
		}));
	}
	
	public void textChangedEvent()
	{
		textChanged = true;
	}
	
	public void runProject()
	{
		Process.Start("C:\\code\\code_maker\\bin\\CodeMaker.exe");
	}
	
	public void saveFile()
	{
		if (currentFile != "")
		{
			File.WriteAllText(currentFile, mainForm.getEditorContents());
		}
	}
	
	public void saveFileAs()
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
	
	public void createNewProject()
	{
		ProjectCreationForm newProjectForm = new ProjectCreationForm();
		newProjectForm.ShowDialog();
	}
}

}