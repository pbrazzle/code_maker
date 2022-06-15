using System;
using System.IO;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CodeMaker
{

class CodeMaker
{
	public static void createProject(string name)
	{
		Directory.CreateDirectory("C:\\code\\"+name);
		Directory.CreateDirectory("C:\\code\\"+name+"\\src");
		Directory.CreateDirectory("C:\\code\\"+name+"\\bin");
	}
}

class CodeMakerForm : Form
{
	private Button makeProjectButton;
	private TextBox projectNameTextBox;

	public CodeMakerForm()
	{
		buildGUI();
	}
	
	private void buildGUI()
	{
		this.Size = new Size(250, 110);
		this.Text = "CodeMaker";
		this.FormBorderStyle = FormBorderStyle.FixedSingle;

		makeProjectButton = new Button();
		makeProjectButton.Size = new Size(100, 20);
		makeProjectButton.Location = new Point(10, 40);
		makeProjectButton.Text = "Create Project";
		makeProjectButton.Click += new EventHandler(makeProjectButton_click);
		this.Controls.Add(makeProjectButton);

		projectNameTextBox = new TextBox();
		projectNameTextBox.Size = new Size(200, 20);
		projectNameTextBox.Location = new Point(10, 10);
		this.Controls.Add(projectNameTextBox);
	}

	private void makeProjectButton_click(object sender, EventArgs e)
	{
		CodeMaker.createProject(projectNameTextBox.Text);
	}

	[STAThread]
	public static void Main()
	{
		Application.EnableVisualStyles();
		Application.Run(new CodeMakerForm());
	}
}

}