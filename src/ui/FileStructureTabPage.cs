using System.Windows.Forms;

using CodeMaker;

namespace CodeMaker.UI
{

class FileStructureTabPage : TabPage
{
	public FileStructureTabPage() : base("Files")
	{
		TreeView fileStructureTreeView = new TreeView();
		fileStructureTreeView.Dock = DockStyle.Fill;
		this.Controls.Add(fileStructureTreeView);
	}
}

}