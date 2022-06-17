namespace CodeMaker
{

interface ICodeMakerMainView
{
	event Notify OpenFile, SaveFile, SaveFileAs, RunProject, BuildProject, TextEditorChanged, NewProject;
	
	void show();
	
	string getEditorContents();
	void setEditorContents(string text);
	
	void appendToTerminal(string line);
	
	void setFileStructureInfo(FileStructureInfo info);
	
	void setClassStructureInfo(ClassStructureInfo info);
}

}