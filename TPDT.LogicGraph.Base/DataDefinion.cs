using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TPDT.LogicGraph.Base
{
    public class DataDefinion
    {
        public int Id { get;  set; }
        public string EntityType { get; set; }
        public string DefaultName { get;  set; }
        public string DefaultDescription { get;  set; }
        public Dictionary<string, string> Tags { get; protected set; }

        public DataDefinion()
        {
            EntityType = typeof(object).FullName;
            Tags = new Dictionary<string, string>();
            DefaultDescription = string.Empty;
        }

        public virtual void Save(BinaryWriter writer)
        {
            writer.Write(Id);
            writer.Write(EntityType);
            writer.Write(DefaultName);
            writer.Write(DefaultDescription);
            writer.Write(Tags.Count);
            foreach (var t in Tags)
            {
                writer.Write(t.Key);
                writer.Write(t.Value);
            }
        }

        protected static T Load<T>(BinaryReader reader) where T : DataDefinion, new()
        {
            T nd = new T();
            nd.Id = reader.ReadInt32();
            nd.EntityType = reader.ReadString();
            nd.DefaultName = reader.ReadString();
            nd.DefaultDescription = reader.ReadString();
            int count = reader.ReadInt32();
            nd.Tags = new Dictionary<string, string>(count);
            for (int i = 0; i < count; i++)
            {
                nd.Tags.Add(reader.ReadString(), reader.ReadString());
            }
            return nd;
        }
    }
}
