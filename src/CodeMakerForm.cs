using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CodeMaker
{
class CodeMakerForm : Form
{

	public CodeMakerForm()
	{
		buildGUI();
	}
	
	private void buildGUI()
	{
		this.Size = new Size(250, 110);
		this.Text = "CodeMaker";
		this.FormBorderStyle = FormBorderStyle.FixedSingle;
	}

	private void makeProjectButton_click(object sender, EventArgs e)
	{
		ProjectCreator.createProject(projectNameTextBox.Text);
	}

	[STAThread]
	public static void Main()
	{
		Application.EnableVisualStyles();
		Application.Run(new CodeMakerForm());
	}
}
}