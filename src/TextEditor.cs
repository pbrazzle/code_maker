using System.IO;
using System.Windows.Forms;

namespace CodeMaker
{

class TextEditor
{
	private string currentFile;
	
	public TextEditor()
	{
		currentFile = "";
	}
	
	public string openFile()
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
				return File.ReadAllText(currentFile);
			}
			return null;
		}
	}
	
	public void saveFile(string contents)
	{
		if (currentFile != "")
		{
			File.WriteAllText(currentFile, contents);
		}
	}
	
	public void saveFileAs(string contents)
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
				File.WriteAllText(currentFile, contents);
			}
		}	
	}
}

}