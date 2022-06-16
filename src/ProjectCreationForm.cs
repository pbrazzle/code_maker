using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CodeMaker
{
class ProjectCreationForm : Form
{
	private Button makeProjectButton;
	private TextBox projectNameTextBox;

	public ProjectCreationForm()
	{
		buildGUI();
	}
	
	private void buildGUI()
	{
		this.Size = new Size(250, 110);
		this.Text = "New Project";
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
		ProjectCreator.createProject(projectNameTextBox.Text);
	}
}
}