/*
 * 由SharpDevelop创建。
 * 用户： Tangent.CZ
 * 日期: 06/19/2013
 * 时间: 16:33
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.IO;
using TPDT.LogicGraph.Base;

namespace TPDT.LogicGraph.Army
{
    /// <summary>
    /// Description of ArmyBase.
    /// </summary>
    public abstract class ArmyBase
    {
        public int Id { get; protected set; }
        public string Name { get; set; }
        public int Attack { get; set; }
        public int Move { get; set; }
        public ArmyDefinition Definition { get; protected set; }
        public string Description { get; protected set; }
        public NodeBase Position { get; protected set; }
        public PlayerBase Owner { get; protected set; }

        private static int index = 0;

        protected ArmyBase(int id, ArmyDefinition army,NodeBase position, PlayerBase owner)
        {
            Id = id;
            Owner = owner;
            Position = position;
            Name = army.DefaultName;
            Attack = army.DefaultAttack;
            Move = army.DefaultMove;
            Description = army.DefaultDescription;
            Definition = army;
        }

        public virtual void WriteArmy(BinaryWriter writer)
        {
            writer.Write(Id);
            writer.Write(this.GetType().FullName);
        }

        public static ArmyBase CreateArmy(ArmyDefinition armyDefinition, NodeBase position, PlayerBase owner)
        {
            ArmyBase army;

            army = ResourceManager.CurrentResouceManager.LoadedArmys[armyDefinition.EntityType].InvokeMember(
                null,
                System.Reflection.BindingFlags.CreateInstance,
                null,
                null,
                new object[] { index++, armyDefinition, position, owner }
                ) as ArmyBase;

            return army;
        }
    }
}
