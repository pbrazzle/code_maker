using System;
using System.IO;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CodeMaker
{

class CodeMaker
{
	private static string buildFileContents = 
	"@echo off\n"
	+"csc /out:bin\\$PROJ_NAME$.exe src\\$PROJ_NAME$.cs\n";
	
	private static string helloWorldFileContents =
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
	
	public static void createProject(string name)
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