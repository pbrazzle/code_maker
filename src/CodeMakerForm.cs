using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

using CodeMaker.UI;

namespace CodeMaker
{
class CodeMakerForm : Form
{
	public CodeMakerForm()
	{
		//Form info
		this.Size = new Size(800, 600);
		this.Text = "CodeMaker";
		
		this.Controls.Add(new TextEditorRichTextBox());
		this.Controls.Add(new ProjectViewTabControl());
		this.Controls.Add(new TerminalRichTextBox());
		this.Menu = new CodeMakerMainMenu();
	}
	
	public string getEditorContents()
	{
		return "";
	}
	
	public void setEditorContents(string text)
	{
		
	}
	
	public void loadDirectory(string path)
	{
		
	}

	[STAThread]
	public static void Main()
	{
		CodeMakerForm form = new CodeMakerForm();
		CodeMaker.setForm(form);
		Application.EnableVisualStyles();
		Application.Run(form);
	}
}
}