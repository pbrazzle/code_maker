using System.Windows.Forms;

using CodeMaker;

namespace CodeMaker.UI
{

class ProjectViewTabControl : TabControl
{
	public ProjectViewTabControl() : base()
	{
		this.Dock = DockStyle.Left;
		this.Controls.Add(new FileStructureTabPage());
		this.Controls.Add(new ClassStructureTabPage());
	}
}

}