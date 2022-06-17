using System.Windows.Forms;

namespace CodeMaker.UI
{

class TerminalRichTextBox : RichTextBox
{
	public TerminalRichTextBox() : base()
	{
		this.Multiline = true;
		this.Dock = DockStyle.Bottom;
		this.ReadOnly = true;
		this.SelectionAlignment = HorizontalAlignment.Left;
	}
}

}