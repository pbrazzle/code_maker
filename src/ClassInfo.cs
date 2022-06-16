using System.Windows.Forms;
using System.Collections.Generic;
using System;

namespace CodeMaker
{

class ClassInfo : IEquatable<ClassInfo>
{
	private string className, parentClassName;
	private List<ClassInfo> childClassInfoList;
	
	public ClassInfo(string name, string parentName)
	{
		className = name;
		parentClassName = parentName;
		childClassInfoList = new List<ClassInfo>();
	}
	
	public void addChild(ClassInfo child)
	{
		childClassInfoList.Add(child);
	}
	
	public TreeNode asTreeNode()
	{
		TreeNode classInfoNode = new TreeNode(className);
		foreach (ClassInfo child in childClassInfoList)
		{
			classInfoNode.Nodes.Add(child.asTreeNode());
		}
		return classInfoNode;
	}
	
	public bool isParentOf(ClassInfo info)
	{
		return className == info.parentClassName;
	}
	
	public bool Equals(ClassInfo info)
	{
		return className == info.className;
	}
	
	public override int GetHashCode()
	{
		return 0;
	}
}

}