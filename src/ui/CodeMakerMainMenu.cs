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
		createProjectMenuItem.Click += (sender, e) => onNewProject();
		newMenu.MenuItems.Add(createProjectMenuItem);
		
		MenuItem createClassMenuItem = new MenuItem("Class");
		newMenu.MenuItems.Add(createClassMenuItem);
		
		MenuItem createFileMenuItem = new MenuItem("File");
		newMenu.MenuItems.Add(createFileMenuItem);
		fileMenu.MenuItems.Add(newMenu);
		
		MenuItem openMenu = new MenuItem("Open");
		
		MenuItem openFileMenu = new MenuItem("File");
		openFileMenu.Click += (sender, e) => onOpenFile();
		openMenu.MenuItems.Add(openFileMenu);
		
		MenuItem openProjectMenu = new MenuItem("Project");
		openMenu.MenuItems.Add(openProjectMenu);
		fileMenu.MenuItems.Add(openMenu);
		
		MenuItem saveMenu = new MenuItem("Save");
		saveMenu.Click += (sender, e) => onSaveFile();
		fileMenu.MenuItems.Add(saveMenu);
		
		MenuItem saveAsMenu = new MenuItem("Save As");
		saveAsMenu.Click += (sender, e) => onSaveFileAs();
		fileMenu.MenuItems.Add(saveAsMenu);
		this.MenuItems.Add(fileMenu);
		
		MenuItem buildMenu = new MenuItem("Build");
		
		MenuItem buildProjectMenuItem = new MenuItem("Build Project");
		buildProjectMenuItem.Click += (sender, e) => onBuildProject();
		buildMenu.MenuItems.Add(buildProjectMenuItem);
		
		MenuItem runProjectMenuItem = new MenuItem("Run Project");
		runProjectMenuItem.Click += (sender, e) => onRunProject();
		buildMenu.MenuItems.Add(runProjectMenuItem);
		this.MenuItems.Add(buildMenu);
	}
	
	public delegate void Notify();
	public event Notify OpenFile, SaveFile, SaveFileAs, BuildProject, RunProject, NewProject;
	
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
}

}