using System.Windows.Forms;
using System.Collections.Generic;

namespace CodeMaker
{

class FileInfoNode
{
	string name;
	bool isDir;
	List<FileInfoNode> children;
	
	public FileInfoNode(string n, bool d)
	{
		name = n;
		isDir = d;
		children = new List<FileInfoNode>();
	}
	
	public void addChild(FileInfoNode child)
	{
		if (isDir)
		{
			children.Add(child);
		}
	}
	
	public TreeNode asTreeNode()
	{
		TreeNode fileInfoNode = new TreeNode(name);
		foreach (FileInfoNode child in children)
		{
			fileInfoNode.Nodes.Add(child.asTreeNode());
		}
		return fileInfoNode;
	}
}

}