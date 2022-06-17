using System.Windows.Forms;
using System;
using System.IO;

using CodeMaker;

namespace CodeMaker.UI
{

class CodeMakerMainMenu : MainMenu
{
	public CodeMakerMainMenu() : base()
	{
		MenuItem fileMenu = new MenuItem("File");
		
		MenuItem newMenu = new MenuItem("New");
		MenuItem createProjectMenuItem = new MenuItem("Project");
		createProjectMenuItem.Click += CodeMaker.createNewProject;
		newMenu.MenuItems.Add(createProjectMenuItem);
		
		MenuItem createClassMenuItem = new MenuItem("Class");
		newMenu.MenuItems.Add(createClassMenuItem);
		
		MenuItem createFileMenuItem = new MenuItem("File");
		newMenu.MenuItems.Add(createFileMenuItem);
		fileMenu.MenuItems.Add(newMenu);
		
		MenuItem openMenu = new MenuItem("Open");
		
		MenuItem openFileMenu = new MenuItem("File");
		openFileMenu.Click += CodeMaker.openFile;
		openMenu.MenuItems.Add(openFileMenu);
		
		MenuItem openProjectMenu = new MenuItem("Project");
		openMenu.MenuItems.Add(openProjectMenu);
		fileMenu.MenuItems.Add(openMenu);
		
		MenuItem saveMenu = new MenuItem("Save");
		saveMenu.Click += CodeMaker.saveFile;
		fileMenu.MenuItems.Add(saveMenu);
		
		MenuItem saveAsMenu = new MenuItem("Save As");
		saveAsMenu.Click += CodeMaker.saveFileAs;
		fileMenu.MenuItems.Add(saveAsMenu);
		this.MenuItems.Add(fileMenu);
		
		MenuItem buildMenu = new MenuItem("Build");
		
		MenuItem buildProjectMenuItem = new MenuItem("Build Project");
		buildProjectMenuItem.Click += CodeMaker.buildProject;
		buildMenu.MenuItems.Add(buildProjectMenuItem);
		
		MenuItem runProjectMenuItem = new MenuItem("Run Project");
		runProjectMenuItem.Click += CodeMaker.runProject;
		buildMenu.MenuItems.Add(runProjectMenuItem);
		this.MenuItems.Add(buildMenu);
	}
}

}