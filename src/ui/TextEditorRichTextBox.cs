using System.Windows.Forms;

namespace CodeMaker.UI
{

class TextEditorRichTextBox : RichTextBox
{	
	public TextEditorRichTextBox() : base()
	{
		this.Multiline = true;
		this.Dock = DockStyle.Fill;
		this.ReadOnly = false;
		this.SelectionAlignment = HorizontalAlignment.Left;
		this.AcceptsTab = true;
		this.KeyPress += CodeMaker.textChangedEvent;
	}
}

}