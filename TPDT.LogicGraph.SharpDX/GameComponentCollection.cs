using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPDT.LogicGraph.SharpDXGame
{
    public class OrderedCollection<T> : List<T>,IDisposable
    {
        protected Queue<T> waitAdd = new Queue<T>();
        public static List<OrderedCollection<T>> collections = new List<OrderedCollection<T>>();
        public Comparison<T> Comparer { get; protected set; }
        public static void LoadComponents()
        {
            foreach (var item in collections)
            {
                bool has = item.waitAdd.Count > 0;
                while (item.waitAdd.Count > 0)
                    item.Add(item.waitAdd.Dequeue());
                if (has)
                    item.Sort(item.Comparer);
            }
        }

        public OrderedCollection()
        {
            collections.Add(this);
        }

        ~OrderedCollection()
        {
            this.Dispose(false);
        }
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                lock (this)
                {
                    if (collections != null)
                    {
                        collections.Remove(this);
                    }
                }
            }
        }
    }
    public class GameComponentCollection : OrderedCollection<GameComponent>
    {
        public GameComponentCollection()
        {
            Comparer = (i, j) => i.UpdateOrder.CompareTo(j.UpdateOrder);
        }
        public new void Add(GameComponent item)
        {
            waitAdd.Enqueue(item);
        }

    }
    public class DrawableComponentCollection : OrderedCollection<DrawableGameComponent>
    {
        public DrawableComponentCollection()
        {
            Comparer = (i, j) => i.DrawOrder.CompareTo(j.DrawOrder);
        }
        public new void Add(DrawableGameComponent item)
        {
            waitAdd.Enqueue(item);
        }
    }
    //public class ByUpdateOrder : Comparer<GameComponent>
    //{
    //    public override int Compare(GameComponent x, GameComponent y)
    //    {
    //        return x.UpdateOrder.CompareTo(y.UpdateOrder);
    //    }
    //}
    //public class ByDraawOrder : Comparer<DrawableGameComponent>
    //{
    //    public override int Compare(DrawableGameComponent x, DrawableGameComponent y)
    //    {
    //        return x.DrawOrder.CompareTo(y.DrawOrder);
    //    }
    //}
}
