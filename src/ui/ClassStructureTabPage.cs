using System.Windows.Forms;

using CodeMaker;

namespace CodeMaker.UI
{

class ClassStructureTabPage : TabPage
{

	public ClassStructureTabPage() : base("Classes")
	{
		TreeView classStructureTreeView = new TreeView();
		classStructureTreeView.Dock = DockStyle.Fill;
		this.Controls.Add(classStructureTreeView);
	}
}

}