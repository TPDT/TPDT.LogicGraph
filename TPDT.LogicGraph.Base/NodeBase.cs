/*
 * 由SharpDevelop创建。
 * 用户： Tangent.CZ
 * 日期: 06/19/2013
 * 时间: 16:25
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TPDT.LogicGraph.Army;

namespace TPDT.LogicGraph.Base
{
    /// <summary>
    /// Description of NodeBase.
    /// </summary>
    public abstract class NodeBase
    {
        public int Id { get; protected set; }
        public ArmyBase Army { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }

        protected NodeBase(NodeDefinition node)
        {
            Name = node.DefaultName;
            Description = node.DefaultDescription;
        }

        public virtual void WriteNode(BinaryWriter writer)
        {
            writer.Write(Id);
            writer.Write(this.GetType().FullName);
        }

        public static NodeBase CreateNode(NodeDefinition nodeDefinition)
        {
            NodeBase node;

            node = ResouceManager.CurrentResouceManager.LoadedNodes[nodeDefinition.EntityType].InvokeMember(
                null,
                System.Reflection.BindingFlags.CreateInstance,
                null,
                null,
                new object[] { nodeDefinition }
                ) as NodeBase;

            return node;
        }
    }
}
