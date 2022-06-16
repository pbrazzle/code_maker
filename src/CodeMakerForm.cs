using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CodeMaker
{
class CodeMakerForm : Form
{
	private TreeView fileStructureTreeView;

	public CodeMakerForm()
	{
		buildGUI();
	}
	
	private void buildGUI()
	{
		this.Size = new Size(250, 110);
		this.Text = "CodeMaker";
		//this.FormBorderStyle = FormBorderStyle.FixedSingle;
		
		fileStructureTreeView = new TreeView();
		fileStructureTreeView.Size = new Size(200, 100);
		fileStructureTreeView.Location = new Point(0, 0);
		
		FileStructureReader fsReader = new FileStructureReader();
		FileStructureInfo fsInfo = fsReader.readDirectory("C:\\code");
		fileStructureTreeView.Nodes.Clear();

		foreach (TreeNode node in fsInfo.asTreeView())
		{
			fileStructureTreeView.Nodes.Add(node);
		}
		
		this.Controls.Add(fileStructureTreeView);
	}

	[STAThread]
	public static void Main()
	{
		Application.EnableVisualStyles();
		Application.Run(new CodeMakerForm());
	}
}
}