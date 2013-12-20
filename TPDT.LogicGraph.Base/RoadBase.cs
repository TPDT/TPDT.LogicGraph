/*
 * 由SharpDevelop创建。
 * 用户： Tangent.CZ
 * 日期: 06/19/2013
 * 时间: 17:02
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using TPDT.LogicGraph.Army;

namespace TPDT.LogicGraph.Base
{
	/// <summary>
	/// Description of RoadBase.
	/// </summary>
	public abstract class RoadBase
	{		
		public int Id{get;private set;}
		public static string Name{get;private set;}
		public static string Description{get;private set;}
		public NodeBase Node1{get;private set;}
		public NodeBase Node2{get;private set;}
		
		public abstract bool IsMoveable(ArmyBase army);
	}
}
