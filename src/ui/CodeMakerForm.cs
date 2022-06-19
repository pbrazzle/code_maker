using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

using CodeMaker;

namespace CodeMaker.UI
{
class CodeMakerForm : Form
{
	CodeMakerMainMenu mainMenu;
	TextEditorRichTextBox textEditorTextBox;
	TerminalRichTextBox terminalTextBox;
	bool textChanged;
	string currentFile;
	
	private CodeMaker codeMaker;
	
	public CodeMakerForm()
	{
		textChanged = false;
		currentFile = "";
		this.Size = new Size(800, 600);
		this.Text = "CodeMaker";
		
		textEditorTextBox = new TextEditorRichTextBox();
		textEditorTextBox.KeyPress += (sender, e) => onTextEditorChanged();
		this.Controls.Add(textEditorTextBox);
		this.Controls.Add(new ProjectViewTabControl());
		
		terminalTextBox = new TerminalRichTextBox();
		this.Controls.Add(terminalTextBox);
		
		mainMenu = new CodeMakerMainMenu();
		mainMenu.OpenFile += () => onOpenFile();
		mainMenu.SaveFile += () => onSaveFile();
		mainMenu.SaveFileAs += () => onSaveFileAs();
		mainMenu.BuildProject += () => onBuildProject();
		mainMenu.RunProject += () => onRunProject();
		mainMenu.NewProject += () => onNewProject();
		this.Menu = mainMenu;
		
		codeMaker = new CodeMaker();
		
		Application.EnableVisualStyles();
		Application.Run(this);
	}
	
	[STAThread]
	public static void Main()
	{
		new CodeMakerForm();
	}
	
	public void show()
	{
		Application.EnableVisualStyles();
		Application.Run(this);
	}
	
	public string getEditorContents()
	{
		return textEditorTextBox.Text;
	}
	
	public void setEditorContents(string text)
	{
		textEditorTextBox.Text = text;
	}
	
	public void appendToTerminal(string line)
	{
		terminalTextBox.AppendText(line);
	}
	
	public void setFileStructureInfo(FileStructureInfo info)
	{
		
	}
	
	public void setClassStructureInfo(ClassStructureInfo info)
	{
		
	}
	
	protected virtual void onOpenFile()
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
				textEditorTextBox.Text = File.ReadAllText(currentFile);
			}
		}
		if (textChanged)
		{
			MessageBoxButtons buttons = MessageBoxButtons.YesNo;
			DialogResult result = MessageBox.Show("Save current work?", "Open file", buttons);
			if (result == DialogResult.Yes)
			{
				onSaveFile();
			}
			textChanged = false;
		}
	}
	
	protected virtual void onSaveFile()
	{
		if (currentFile != "")
		{
			File.WriteAllText(currentFile, textEditorTextBox.Text);
		}
	}
	
	protected virtual void onSaveFileAs()
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
				File.WriteAllText(currentFile, textEditorTextBox.Text);
			}
		}
	}
	
	protected virtual void onBuildProject()
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
		if (e.Data != null)
		{
			terminalTextBox.AppendText(e.Data);
		}	
	}
	
	protected virtual void onRunProject()
	{
		Process.Start("C:\\code\\code_maker\\bin\\CodeMaker.exe");
	}
	
	protected virtual void onNewProject()
	{
		ProjectCreationForm newProjectForm = new ProjectCreationForm();
		newProjectForm.ShowDialog();
	}
	
	protected virtual void onTextEditorChanged()
	{
		textChanged = true;
	}
}
}