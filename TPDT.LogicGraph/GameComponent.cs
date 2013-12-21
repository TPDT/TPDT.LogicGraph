using SharpDX;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Content;
using SharpDX.Toolkit.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPDT.LogicGraph
{
    public abstract class GameComponent : Component, IGameSystem, IUpdateable, IContentable
    {
        private readonly DisposeCollector contentCollector = new DisposeCollector();
        private readonly IServiceRegistry registry;
        private bool enabled;
        private Game game;
        private int updateOrder;
        private IContentManager contentManager;


        /// <summary>
        /// Initializes a new instance of the <see cref="GameSystem" /> class.
        /// </summary>
        /// <param name="registry">The registry.</param>
        public GameComponent(IServiceRegistry registry)
        {
            if (registry == null) throw new ArgumentNullException("registry");
            this.registry = registry;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameSystem" /> class.
        /// </summary>
        /// <param name="game">The game.</param>
        public GameComponent(Game game)
        {
            if (game == null) throw new ArgumentNullException("game");
            this.game = game;
            this.registry = game.Services;
        }

        /// <summary>
        /// Gets the <see cref="Game"/> associated with this <see cref="GameSystem"/>. This value can be null in a mock environment.
        /// </summary>
        /// <value>The game.</value>
        public Game Game
        {
            get { return game; }
        }

        /// <summary>
        /// Gets the services registry.
        /// </summary>
        /// <value>The services registry.</value>
        public IServiceRegistry Services
        {
            get
            {
                return registry;
            }
        }

        /// <summary>
        /// Gets the content manager.
        /// </summary>
        /// <value>The content.</value>
        protected IContentManager Content
        {
            get
            {
                return contentManager;
            }
        }

        #region IGameSystem Members

        public virtual void Initialize()
        {
            // Gets the Content Manager
            contentManager = (IContentManager)registry.GetService(typeof(IContentManager));
        }

        #endregion

        #region IUpdateable Members

        public event EventHandler<EventArgs> EnabledChanged;

        public event EventHandler<EventArgs> UpdateOrderChanged;

        public virtual void Update(GameTime gameTime)
        {
        }

        public bool Enabled
        {
            get { return enabled; }
            set
            {
                if (enabled != value)
                {
                    enabled = value;
                    OnEnabledChanged(EventArgs.Empty);
                }
            }
        }

        public int UpdateOrder
        {
            get { return updateOrder; }
            set
            {
                if (updateOrder != value)
                {
                    updateOrder = value;
                    OnUpdateOrderChanged(this, EventArgs.Empty);
                }
            }
        }

        #endregion

        private void OnEnabledChanged(EventArgs e)
        {
            EventHandler<EventArgs> handler = EnabledChanged;
            if (handler != null) handler(this, e);
        }

        protected virtual void OnUpdateOrderChanged(object source, EventArgs e)
        {
            EventHandler<EventArgs> handler = UpdateOrderChanged;
            if (handler != null) handler(source, e);
        }

        #region Implementation of IContentable

        void IContentable.LoadContent()
        {
            LoadContent();
        }

        void IContentable.UnloadContent()
        {
            contentCollector.DisposeAndClear();

            UnloadContent();
        }

        protected virtual void LoadContent()
        {
        }

        protected virtual void UnloadContent()
        {
        }

        #endregion

        /// <summary>
        /// Adds an object to be disposed automatically when <see cref="UnloadContent"/> is called. See remarks.
        /// </summary>
        /// <typeparam name="T">Type of the object to dispose</typeparam>
        /// <param name="disposable">The disposable object.</param>
        /// <returns>The disposable object.</returns>
        /// <remarks>
        /// Use this method for any content that is not loaded through the <see cref="ContentManager"/>.
        /// </remarks>
        protected T ToDisposeContent<T>(T disposable) where T : IDisposable
        {
            return contentCollector.Collect(disposable);
        }
    }
}

