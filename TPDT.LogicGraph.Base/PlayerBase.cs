/*
 * 由SharpDevelop创建。
 * 用户： Tangent.CZ
 * 日期: 06/19/2013
 * 时间: 16:45
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Collections.Generic;
using TPDT.LogicGraph.Army;

namespace TPDT.LogicGraph.Base
{
	/// <summary>
	/// Description of PlayerBase.
	/// </summary>
	public abstract class PlayerBase
	{
		public int Id{get;private set;}
		public string Name{get;private set;}
		public Dictionary<int,ArmyBase> Armys{get;private set;}
		public PlayerBase()
		{
			
		}
	}
}
