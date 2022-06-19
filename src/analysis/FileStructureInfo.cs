using System.Windows.Forms;
using System.Collections.Generic;

using CodeMaker.Analysis;

namespace CodeMaker
{

class FileStructureInfo
{
	private List<FileInfoNode> topLevelNodes;
	
	public FileStructureInfo(List<FileInfoNode> top)
	{
		topLevelNodes = top;
	}
	
	public List<TreeNode> asTreeView()
	{
		List<TreeNode> nodes = new List<TreeNode>();
		foreach (FileInfoNode node in topLevelNodes)
		{
			nodes.Add(node.asTreeNode());
		}
		return nodes;
	}
}

}