using System.IO;
using System.Collections.Generic;

namespace CodeMaker.Analysis
{

class FileStructureReader
{
	public FileStructureReader() { }
	
	public FileStructureInfo readDirectory(string path)
	{
		List<FileInfoNode> topLevelNodes = new List<FileInfoNode>();
		foreach (string fileName in Directory.GetFiles(path))
		{
			string name = new DirectoryInfo(fileName).Name;
			topLevelNodes.Add(new FileInfoNode(name, false));
		}
		foreach (string dirName in Directory.GetDirectories(path))
		{
			topLevelNodes.Add(buildDirNode(dirName));
		}
		FileStructureInfo info = new FileStructureInfo(topLevelNodes);
		return info;
	}
	
	private FileInfoNode buildDirNode(string path)
	{
		string dirName = new DirectoryInfo(path).Name;
		FileInfoNode node = new FileInfoNode(dirName, true);
		foreach (string fileName in Directory.GetFiles(path))
		{
			string name = new DirectoryInfo(fileName).Name;
			node.addChild(new FileInfoNode(name, false));
		}
		foreach (string subdirName in Directory.GetDirectories(path))
		{
			node.addChild(buildDirNode(subdirName));
		}
		return node;
	}
}

}