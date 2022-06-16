using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace CodeMaker
{
class CodeMakerForm : Form
{
	private TreeView fileStructureTreeView;
	private RichTextBox fileTextBox;
	
	private bool textChanged;
	private string currentFile;

	public CodeMakerForm()
	{
		buildGUI();
		
		textChanged = false;
		currentFile = "";
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
		
		ClassStructureReader csReader = new ClassStructureReader();
		ClassStructureInfo csInfo = csReader.readProject("C:\\code\\code_maker");
		classStructureTreeView.Nodes.Clear();
		
		foreach (TreeNode node in csInfo.asTreeView())
		{
			classStructureTreeView.Nodes.Add(node);
		}
		
		classViewTab.Controls.Add(classStructureTreeView);
	}
	
	private void buildTextEditor()
	{
		fileTextBox = new RichTextBox();
		fileTextBox.Multiline = true;
		fileTextBox.Dock = DockStyle.Fill;
		fileTextBox.ReadOnly = false;
		fileTextBox.SelectionAlignment = HorizontalAlignment.Left;
		fileTextBox.AcceptsTab = true;
		fileTextBox.KeyPress += fileTextEditor_keyPress;
		this.Controls.Add(fileTextBox);
		this.Controls.SetChildIndex(fileTextBox, 0);
	}
	
	private void buildMenu()
	{
		MainMenu formMenu = new MainMenu();
		
		MenuItem fileMenu = new MenuItem("File");
		
		MenuItem newMenu = new MenuItem("New");
		MenuItem createProjectMenuItem = new MenuItem("Project");
		createProjectMenuItem.Click += new EventHandler(newProjectMenuItem_click);
		newMenu.MenuItems.Add(createProjectMenuItem);
		MenuItem createClassMenuItem = new MenuItem("Class");
		newMenu.MenuItems.Add(createClassMenuItem);
		MenuItem createFileMenuItem = new MenuItem("File");
		
		newMenu.MenuItems.Add(createFileMenuItem);
		fileMenu.MenuItems.Add(newMenu);
		
		MenuItem openMenu = new MenuItem("Open");
		MenuItem openFileMenu = new MenuItem("File");
		openFileMenu.Click += new EventHandler(openFileMenuItem_click);
		openMenu.MenuItems.Add(openFileMenu);
		MenuItem openProjectMenu = new MenuItem("Project");
		
		foreach (string dirname in Directory.GetDirectories("C:\\code\\"))
		{
			MenuItem projectMenuItem = new MenuItem(Path.GetFileName(dirname));
			openProjectMenu.MenuItems.Add(projectMenuItem);
		}
		
		openMenu.MenuItems.Add(openProjectMenu);
		fileMenu.MenuItems.Add(openMenu);
		
		MenuItem saveMenu = new MenuItem("Save");
		saveMenu.Click += new EventHandler(saveFileMenuItem_click);
		fileMenu.MenuItems.Add(saveMenu);
		
		MenuItem saveAsMenu = new MenuItem("Save As");
		saveAsMenu.Click += new EventHandler(saveAsFileMenuItem_click);
		fileMenu.MenuItems.Add(saveAsMenu);
		
		formMenu.MenuItems.Add(fileMenu);
		
		Menu = formMenu;
	}
	
	private void buildGUI()
	{
		//Form info
		this.Size = new Size(800, 600);
		this.Text = "CodeMaker";
		this.FormClosing += CodeMakerForm_Closing;
		
		buildProjectViewTabControl();
		buildTextEditor();
		buildMenu();
	}

	private void openFileMenuItem_click(object sender, EventArgs e)
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
				fileTextBox.Text = File.ReadAllText(currentFile);
			}
		}
		if (textChanged)
		{
			MessageBoxButtons buttons = MessageBoxButtons.YesNo;
			DialogResult result = MessageBox.Show("Save current work?", "Open file", buttons);
			if (result == DialogResult.Yes)
			{
				saveFileMenuItem_click(this, new EventArgs());
			}
			textChanged = false;
		}
	}
	
	private void saveFileMenuItem_click(object sender, EventArgs e)
	{
		if (currentFile != "")
		{
			File.WriteAllText(currentFile, fileTextBox.Text);
		}
	}
	
	private void saveAsFileMenuItem_click(object sender, EventArgs e)
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
				File.WriteAllText(currentFile, fileTextBox.Text);
			}
		}
	}

	private void newProjectMenuItem_click(object sender, EventArgs e)
	{
		ProjectCreationForm newProjectForm = new ProjectCreationForm();
		newProjectForm.ShowDialog();
	}

	private void fileTextEditor_keyPress(object sender, KeyPressEventArgs e)
	{
		textChanged = true;
	}
	
	private void CodeMakerForm_Closing(object sender, FormClosingEventArgs e)
	{
		if (textChanged)
		{
			MessageBoxButtons buttons = MessageBoxButtons.YesNo;
			DialogResult result = MessageBox.Show("Save current work?", "Exiting", buttons);
			if (result == DialogResult.Yes)
			{
				saveFileMenuItem_click(this, new EventArgs());
			}
		}
	}

	[STAThread]
	public static void Main()
	{
		Application.EnableVisualStyles();
		Application.Run(new CodeMakerForm());
	}
}
}