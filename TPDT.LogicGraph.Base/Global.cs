/*
 * 由SharpDevelop创建。
 * 用户： Tangent.CZ
 * 日期: 06/19/2013
 * 时间: 18:46
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.IO;

namespace TPDT.LogicGraph.Base
{
	/// <summary>
	/// Description of Global.
	/// </summary>
	public static class Global
	{		
		public static void Wirte(this BinaryWriter writer,NodeBase node)
		{
			node.WriteNode(writer);
		}
		public static int Version=1;
	}
}
