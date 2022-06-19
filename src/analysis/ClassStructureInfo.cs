using System.Windows.Forms;
using System.Collections.Generic;

using CodeMaker.Analysis;

namespace CodeMaker
{

class ClassStructureInfo
{
	private List<ClassInfo> topLevelClassInfoList;
	
	public ClassStructureInfo(List<ClassInfo> topClasses)
	{
		topLevelClassInfoList = topClasses;
	}
	
	public List<TreeNode> asTreeView()
	{
		List<TreeNode> treeNodeList = new List<TreeNode>();
		
		foreach (ClassInfo topClass in topLevelClassInfoList)
		{
			treeNodeList.Add(topClass.asTreeNode());
		}
		
		return treeNodeList;
	}
}

}