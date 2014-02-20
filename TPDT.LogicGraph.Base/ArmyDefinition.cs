using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TPDT.LogicGraph.Base
{
    public class ArmyDefinition:DataDefinion
    {
        public int DefaultAttack { get;  set; }
        public int DefaultAttackRange { get;  set; }
        public int DefaultMove { get;  set; }
        public NodeDefinition[] DefaultAttackableNodes { get;  set; }
        public NodeDefinition[] DefaultMoveableNodes { get;  set; }

        public ArmyDefinition()
        {
            Tags = new Dictionary<string, string>();
            DefaultAttackableNodes = new NodeDefinition[0];
            DefaultMoveableNodes = new NodeDefinition[0];
        }

        public override void Save(BinaryWriter writer)
        {
            base.Save(writer);
            writer.Write(DefaultAttack);
            writer.Write(DefaultAttackRange);
            writer.Write(DefaultMove);
            writer.Write(DefaultAttackableNodes.Length);
            foreach (var nd in DefaultAttackableNodes)
            {
                writer.Write(nd.Id);
            }
            writer.Write(DefaultMoveableNodes.Length);
            foreach (var nd in DefaultMoveableNodes)
            {
                writer.Write(nd.Id);
            }

            writer.Flush();
        }

        public  static ArmyDefinition Load(BinaryReader reader)
        {
            ArmyDefinition ad = DataDefinion.Load<ArmyDefinition>(reader);
            ad.DefaultAttack = reader.ReadInt32();
            ad.DefaultAttackRange = reader.ReadInt32();
            ad.DefaultMove = reader.ReadInt32();

            ad.DefaultAttackableNodes = new NodeDefinition[reader.ReadInt32()];
            for (int i = 0; i < ad.DefaultAttackableNodes.Length; i++)
            {
                ad.DefaultAttackableNodes[i] = ResourceManager.CurrentResouceManager.NodeDefinitions[reader.ReadInt32()];
            }
            ad.DefaultMoveableNodes = new NodeDefinition[reader.ReadInt32()];
            for (int i = 0; i < ad.DefaultMoveableNodes.Length; i++)
            {
                ad.DefaultMoveableNodes[i] = ResourceManager.CurrentResouceManager.NodeDefinitions[reader.ReadInt32()];
            }

            return ad;
        }
    }
}
