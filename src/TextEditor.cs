using System.IO;
using System.Windows.Forms;

namespace CodeMaker
{
	class TextEditor
	{
		private static string currentFile;
		
		static TextEditor()
		{
			currentFile = "";
		}
		
		public static string openFile()
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
				return "";
			}
		}
		
		public static void saveFile(string fileContents)
		{
			if (currentFile != "")
			{
				File.WriteAllText(currentFile, fileContents);
			}
		}
		
		public static void saveAsFile(string fileContents)
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
					File.WriteAllText(currentFile, fileContents);
				}
			}
		}
	}
}