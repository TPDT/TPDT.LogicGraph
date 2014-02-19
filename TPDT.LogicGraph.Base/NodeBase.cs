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
    public abstract class NodeBase : GameEntity
    {
        public ArmyBase Army { get; protected set; }
        public Tuple<float, float> Position { get; protected set; }
        public NodeDefinition Definition { get; protected set; }

        public List<RoadBase> Roads { get; private set; }

        private static int index = 0;

        public NodeBase(int id, NodeDefinition node, Tuple<float, float> position)
        {
            this.Id = id;
            Name = node.DefaultName;
            Description = node.DefaultDescription;
            Definition = node;
            Position = position;
            Roads = new List<RoadBase>();
        }

        public virtual void WriteNode(BinaryWriter writer)
        {
            writer.Write(Id);
            writer.Write(this.GetType().FullName);
        }

        public static NodeBase CreateNode(NodeDefinition nodeDefinition, Tuple<float, float> position)
        {
            NodeBase node;

            node = ResourceManager.CurrentResouceManager.LoadedNodes[nodeDefinition.EntityType].InvokeMember(
                null,
                System.Reflection.BindingFlags.CreateInstance,
                null,
                null,
                new object[] { index++, nodeDefinition, position }
                ) as NodeBase;

            return node;
        }

        public override void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
