using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPDT.LogicGraph.SharpDXGame
{
    public abstract class GameComponent : IGameComponent, IUpdateable, IDisposable
    {
        private bool enabled;
        private int updateOrder;

        public bool Enabled
        {
            get
            {
                return this.enabled;
            }
            set
            {
                if (this.enabled != value)
                {
                    this.enabled = value;
                    this.OnEnabledChanged(this, EventArgs.Empty);
                }
            }
        }
        public Game2DBase Game { get; private set; }
        public int UpdateOrder
        {
            get
            {
                return this.updateOrder;
            }
            set
            {
                if (this.updateOrder != value)
                {
                    this.updateOrder = value;
                    this.OnUpdateOrderChanged(this, EventArgs.Empty);
                }
            }
        }

        public event EventHandler<EventArgs> Disposed;
        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> UpdateOrderChanged;

        public GameComponent(Game2DBase game)
        {
            this.enabled = true;
            this.Game = game;
        }
        ~GameComponent()
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
                    if (this.Game != null)
                    {
                        this.Game.Components.Remove(this);
                    }
                    if (this.Disposed != null)
                    {
                        this.Disposed(this, EventArgs.Empty);
                    }
                }
            }
        }
        protected virtual void OnEnabledChanged(object sender, EventArgs args)
        {
            if (this.EnabledChanged != null)
            {
                this.EnabledChanged(this, args);
            }
        }
        protected virtual void OnUpdateOrderChanged(object sender, EventArgs args)
        {
            if (this.UpdateOrderChanged != null)
            {
                this.UpdateOrderChanged(this, args);
            }
        }
        public virtual void Initialize()
        {
        }
        public virtual void Update(GameTime gameTime)
        {
        }
    }
    public interface IGameComponent
    {
        // Methods
        void Initialize();
    }
    public interface IUpdateable
    {
        // Events
         event EventHandler<EventArgs> EnabledChanged;
         event EventHandler<EventArgs> UpdateOrderChanged;

        // Methods
        void Update(GameTime gameTime);

        // Properties
        bool Enabled { get; }
        int UpdateOrder { get; }
    }
}

