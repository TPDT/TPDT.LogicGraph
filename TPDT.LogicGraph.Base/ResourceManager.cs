using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using TPDT.LogicGraph.Army;

namespace TPDT.LogicGraph.Base
{
    public class ResourceManager
    {
        public List<Assembly> LoadedAssemblys { get; protected set; }
        public Dictionary<int, ArmyDefinition> ArmyDefinitions {  get; protected set; }
        public Dictionary<int, CardDefinition> CardDefinitions { get; protected set; }
        public Dictionary<int, NodeDefinition> NodeDefinitions { get; protected set; }
        public Dictionary<int, string> RoadDefinitions { get; protected set; }
        public Dictionary<string, Type> LoadedArmys { get; protected set; }
        public Dictionary<string, Type> LoadedCards { get; protected set; }
        public Dictionary<string, Type> LoadedNodes { get; protected set; }
        public Dictionary<string, Type> LoadedRodes { get; protected set; }

        public static Game CurrentGame {  get; protected set; }
        public static ResourceManager CurrentResouceManager {  get; protected set; }

        public ResourceManager()
        {
            ArmyDefinitions = new Dictionary<int, ArmyDefinition>();
            CardDefinitions = new Dictionary<int, CardDefinition>();
            NodeDefinitions = new Dictionary<int, NodeDefinition>();
            RoadDefinitions = new Dictionary<int, string>();
            LoadedArmys = new Dictionary<string, Type>();
            LoadedCards = new Dictionary<string, Type>();
            LoadedNodes = new Dictionary<string, Type>();
            LoadedRodes = new Dictionary<string, Type>();
            LoadedAssemblys = new List<Assembly>();
            ResourceManager.CurrentResouceManager = this;
        }

        public void Save(string path)
        {
            using (BinaryWriter writer = new BinaryWriter(new FileStream(path, FileMode.Create)))
            {
                writer.Write((byte)0);
                writer.Write(LoadedAssemblys.Count );
                foreach (var ass in LoadedAssemblys)
                {
                    writer.Write(Path.GetFileName( ass.Location));
                }

                writer.Write((byte)1);
                writer.Write(ArmyDefinitions.Count);
                foreach (var ad in ArmyDefinitions)
                {
                    ad.Value.Save(writer);
                }

                writer.Write((byte)2);
                writer.Write(CardDefinitions.Count);
                foreach (var nd in CardDefinitions)
                {
                    nd.Value.Save(writer);
                }

                writer.Write((byte)3);
                writer.Write(NodeDefinitions.Count);
                foreach (var nd in NodeDefinitions)
                {
                    nd.Value.Save(writer);
                }

                writer.Write((byte)4);
                writer.Write(RoadDefinitions.Count);
                foreach (var nd in RoadDefinitions)
                {
                    writer.Write(nd.Key);
                    writer.Write(nd.Value);
                }
            }
        }

        public void Load(string path)
        {
            using (BinaryReader reader = new BinaryReader(new FileStream(path, FileMode.Open)))
            {
                if (reader.Read() == 0)
                {
                    int count = reader.ReadInt32();
                    for (int i = 0; i < count; i++)
                    {
                        Assembly ass = Assembly.LoadFrom(reader.ReadString());
                        LoadedAssemblys.Add(ass);
                        foreach (var t in ass.GetTypes())
                        {
                            if (t.IsSubclassOf(typeof(ArmyBase)))
                            {
                                LoadedArmys.Add(t.FullName, t);
                            }
                            else if (t.IsSubclassOf(typeof(CardBase)))
                            {
                                LoadedCards.Add(t.FullName, t);
                            }
                            else if (t.IsSubclassOf(typeof(NodeBase)))
                            {
                                LoadedNodes.Add(t.FullName, t);
                            }
                            else if (t.IsSubclassOf(typeof(RoadBase)))
                            {
                                LoadedRodes.Add(t.FullName, t);
                            }
                        }
                    }
                }
                else
                    throw new Exception("Load Assemblys Failed");
                if (reader.Read() == 1)
                {
                    int count = reader.ReadInt32();
                    for (int i = 0; i < count; i++)
                    {
                        ArmyDefinition ad = ArmyDefinition.Load(reader);
                        ArmyDefinitions[ad.Id] = ad;
                    }
                }
                else
                    throw new Exception("no ArmyDefinitions");

                if (reader.Read() == 2)
                {
                    int count = reader.ReadInt32();
                    for (int i = 0; i < count; i++)
                    {
                        CardDefinition cd = CardDefinition.Load(reader);
                        CardDefinitions[cd.Id] = cd;
                    }
                }
                else
                    throw new Exception("no CardDefinitions");

                if (reader.Read() == 3)
                {
                    int count = reader.ReadInt32();
                    for (int i = 0; i < count; i++)
                    {
                        NodeDefinition nd = NodeDefinition.Load(reader);
                        NodeDefinitions[nd.Id] = nd;
                    }
                }
                else
                    throw new Exception("no NodeDefinitions");

                if (reader.Read() == 4)
                {
                    int count = reader.ReadInt32();
                    for (int i = 0; i < count; i++)
                    {
                        string rd = reader.ReadString();
                        RoadDefinitions[reader.ReadInt32()] = rd;
                    }
                }
                else
                    throw new Exception("no NodeDefinitions");
            }
        }
    }
}
