using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CodeMaker
{
class CodeMakerForm : Form
{
	private TreeView fileStructureTreeView;
	private TextBox fileTextBox;

	public CodeMakerForm()
	{
		buildGUI();
	}
	
	private void buildProjectViewTabControl()
	{
		//Tab control
		TabControl projectViewerTabControl = new TabControl();
		projectViewerTabControl.Dock = DockStyle.Left;
		this.Controls.Add(projectViewerTabControl);
		this.Controls.SetChildIndex(projectViewerTabControl, 0);
		
		TabPage fileViewTab = new TabPage("Files");
		TabPage classViewTab = new TabPage("Classes");
		projectViewerTabControl.Controls.Add(fileViewTab);
		projectViewerTabControl.Controls.Add(classViewTab);
		
		//File explorer
		fileStructureTreeView = new TreeView();
		fileStructureTreeView.Dock = DockStyle.Fill;
		
		FileStructureReader fsReader = new FileStructureReader();
		FileStructureInfo fsInfo = fsReader.readDirectory("C:\\code");
		fileStructureTreeView.Nodes.Clear();

		foreach (TreeNode node in fsInfo.asTreeView())
		{
			fileStructureTreeView.Nodes.Add(node);
		}
		
		fileViewTab.Controls.Add(fileStructureTreeView);
		
		//Class explorer
		TreeView classStructureTreeView = new TreeView();
		classStructureTreeView.Dock = DockStyle.Fill;
		classViewTab.Controls.Add(classStructureTreeView);
	}
	
	private void buildTextEditor()
	{
		fileTextBox = new TextBox();
		fileTextBox.Multiline = true;
		fileTextBox.Dock = DockStyle.Fill;
		fileTextBox.ReadOnly = false;
		fileTextBox.TextAlign = HorizontalAlignment.Left;
		this.Controls.Add(fileTextBox);
		this.Controls.SetChildIndex(fileTextBox, 0);
	}
	
	private void buildMenu()
	{
		MainMenu formMenu = new MainMenu();
		
		MenuItem fileMenu = new MenuItem("File");
		
		MenuItem newMenu = new MenuItem("New");
		MenuItem createProjectMenuItem = new MenuItem("Project");
		MenuItem createClassMenuItem = new MenuItem("Class");
		newMenu.MenuItems.Add(createProjectMenuItem);
		newMenu.MenuItems.Add(createClassMenuItem);
		fileMenu.MenuItems.Add(newMenu);
		
		MenuItem openMenu = new MenuItem("Open");
		MenuItem openFileMenu = new MenuItem("File");
		openMenu.MenuItems.Add(openFileMenu);
		MenuItem openProjectMenu = new MenuItem("Project");
		openMenu.MenuItems.Add(openProjectMenu);
		fileMenu.MenuItems.Add(openMenu);
		
		MenuItem saveMenu = new MenuItem("Save");
		fileMenu.MenuItems.Add(saveMenu);
		
		MenuItem saveAsMenu = new MenuItem("Save As");
		fileMenu.MenuItems.Add(saveAsMenu);
		
		formMenu.MenuItems.Add(fileMenu);
		
		Menu = formMenu;
	}
	
	private void buildGUI()
	{
		//Form info
		this.Size = new Size(800, 600);
		this.Text = "CodeMaker";
		
		buildProjectViewTabControl();
		buildTextEditor();
		buildMenu();
	}

	[STAThread]
	public static void Main()
	{
		Application.EnableVisualStyles();
		Application.Run(new CodeMakerForm());
	}
}
}