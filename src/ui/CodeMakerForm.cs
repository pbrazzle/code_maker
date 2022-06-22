using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;

using CodeMaker;
using CodeMaker.Analysis;

namespace CodeMaker.UI
{
class CodeMakerForm : Form
{
	private CodeMakerMainMenu mainMenu;
	private TextEditorRichTextBox textEditorTextBox;
	private TerminalRichTextBox terminalTextBox;
	private TextEditor textEditor;
	private bool textChanged;
	private CodeMaker codeMaker;
	
	public CodeMakerForm()
	{
		this.Size = new Size(800, 600);
		this.Text = "CodeMaker";
		textEditor = new TextEditor();
		textChanged = false;
		
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
		
		CSharpDependencyFinder finder = new CSharpDependencyFinder();
		List<string> dependencies = finder.findDependencies(File.ReadAllText("C:\\code\\code_maker\\src\\ui\\CodeMakerForm.cs"));
		
		Console.WriteLine("Dependencies...");
		foreach (string depend in dependencies)
		{
			Console.WriteLine(depend);
		}
	}
	
	private void onOpenFile()
	{
		if (textChanged)
		{
			MessageBoxButtons buttons = MessageBoxButtons.YesNo;
			DialogResult result = MessageBox.Show("Save current work?", "Open file", buttons);
			if (result == DialogResult.Yes)
			{
				textEditor.saveFile(textEditorTextBox.Text);
			}
			textChanged = false;
		}
		string contents = textEditor.openFile();
		textEditorTextBox.Text = contents != null ? contents : textEditorTextBox.Text;
		terminalTextBox.AppendText("Opened file\n");
	}
	
	private void onSaveFile()
	{
		textEditor.saveFile(textEditorTextBox.Text);
		textChanged = false;
		terminalTextBox.AppendText("File Saved\n");
	}
	
	private void onSaveFileAs()
	{
		textEditor.saveFileAs(textEditorTextBox.Text);
		textChanged = false;
		terminalTextBox.AppendText("File Saved\n");
	}
	
	private void onTextEditorChanged()
	{
		textChanged = true;
	}
	
	//Project management should be moved to main package class
	private void onBuildProject()
	{
		terminalTextBox.AppendText("Building Project...\n");
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
			terminalTextBox.AppendText(e.Data+'\n');
		}	
	}
	
	private void onRunProject()
	{
		terminalTextBox.AppendText("Running Project...\n");
		Process.Start("C:\\code\\code_maker\\bin\\CodeMaker.exe");
	}
	
	private void onNewProject()
	{
		ProjectCreationForm newProjectForm = new ProjectCreationForm();
		newProjectForm.ShowDialog();
	}
}
}