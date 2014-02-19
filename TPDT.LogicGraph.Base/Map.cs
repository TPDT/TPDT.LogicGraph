/*
 * 由SharpDevelop创建。
 * 用户： Tangent.CZ
 * 日期: 06/19/2013
 * 时间: 16:22
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TPDT.LogicGraph.Base
{
	/// <summary>
	/// Description of Map.
	/// </summary>
	public class Map
	{
		public int Id{get;private set;}
		public string Name{get;private set;}
		public List<NodeBase> Nodes{get;private set;}
		public string Author{get;private set;}
		public string Description{get;private set;}

        public List<RoadBase> Roads { get; private set; }
		
		public Map()
		{
            Nodes = new List<NodeBase>();
            Roads = new List<RoadBase>();
		}
		public void Save(string path)
		{
			using (FileStream fs=new FileStream(path,FileMode.CreateNew))
			{
				BinaryWriter bw=new BinaryWriter(fs,Encoding.Unicode);
				bw.Write(Id);
				bw.Write(Name);
				bw.Write(Global.Version);
				bw.Write(Author);
				bw.Write(Description);
				foreach (var node in Nodes)
				{
					bw.Wirte(node);
				}
				
				bw.Flush();
			}
		}
	}
}