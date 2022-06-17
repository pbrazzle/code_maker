using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace CodeMaker.UI
{
class CodeMakerForm : Form, ICodeMakerMainView
{
	CodeMakerMainMenu mainMenu;
	TextEditorRichTextBox textEditorTextBox;
	TerminalRichTextBox terminalTextBox;
	
	public CodeMakerForm()
	{
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
	
	public event Notify OpenFile, SaveFile, SaveFileAs, RunProject, BuildProject, TextEditorChanged, NewProject;
	
	protected virtual void onOpenFile()
	{
		Notify handler = OpenFile;
		if (handler != null) handler.Invoke();
	}
	
	protected virtual void onSaveFile()
	{
		Notify handler = SaveFile;
		if (handler != null) handler.Invoke();
	}
	
	protected virtual void onSaveFileAs()
	{
		Notify handler = SaveFileAs;
		if (handler != null) handler.Invoke();
	}
	
	protected virtual void onBuildProject()
	{
		Notify handler = BuildProject;
		if (handler != null) handler.Invoke();
	}
	
	protected virtual void onRunProject()
	{
		Notify handler = RunProject;
		if (handler != null) handler.Invoke();
	}
	
	protected virtual void onNewProject()
	{
		Notify handler = NewProject;
		if (handler != null) handler.Invoke();
	}
	
	protected virtual void onTextEditorChanged()
	{
		Notify handler = TextEditorChanged;
		if (handler != null) handler.Invoke();
	}
}
}