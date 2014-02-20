using SharpDX.Collections;
using SharpDX.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPDT.LogicGraph
{
    public class ComponentContainer : DrawableGameComponent
    {
        private readonly List<IDrawable> currentlyDrawingGameSystems;
        private readonly List<IUpdateable> currentlyUpdatingGameSystems;
        private readonly List<IContentable> currentlyContentGameSystems;
        private readonly List<IDrawable> drawableGameSystems;
        private readonly List<IGameSystem> pendingGameSystems;
        private readonly List<IUpdateable> updateableGameSystems;
        private readonly List<IContentable> contentableGameSystems;
        private readonly int[] lastUpdateCount;
        
        private bool contentLoaded = false;

        public ObservableCollection<GameComponent> Components { get; set; }
        public ComponentContainer(LogicGraph game)
            : base(game)
        {
            drawableGameSystems = new List<IDrawable>();
            currentlyContentGameSystems = new List<IContentable>();
            currentlyDrawingGameSystems = new List<IDrawable>();
            pendingGameSystems = new List<IGameSystem>();
            updateableGameSystems = new List<IUpdateable>();
            currentlyUpdatingGameSystems = new List<IUpdateable>();
            contentableGameSystems = new List<IContentable>();
            lastUpdateCount = new int[4];

            Components = new ObservableCollection<GameComponent>();
            Components.ItemAdded += Components_ItemAdded;
            Components.ItemRemoved += Components_ItemRemoved;
        }
        /// <summary>
        /// Reference page contains code sample.
        /// </summary>
        /// <param name="gameTime">
        /// Time passed since the last call to Draw.
        /// </param>
        public override void Draw(GameTime gameTime)
        {
            if (!contentLoaded)
                return;
            // Just lock current drawable game systems to grab them in a temporary list.
            lock (drawableGameSystems)
            {
                for (int i = 0; i < drawableGameSystems.Count; i++)
                {
                    currentlyDrawingGameSystems.Add(drawableGameSystems[i]);
                }
            }

            for (int i = 0; i < currentlyDrawingGameSystems.Count; i++)
            {
                var drawable = currentlyDrawingGameSystems[i];
                if (drawable.Visible)
                {
                    if (drawable.BeginDraw())
                    {
                        drawable.Draw(gameTime);
                        drawable.EndDraw();
                    }
                }
            }

            currentlyDrawingGameSystems.Clear();
            base.Draw(gameTime);
        }
        protected override void Dispose(bool disposeManagedResources)
        {
            if (disposeManagedResources)
            {
                lock (this)
                {
                    var array = new GameComponent[Components.Count];
                    this.Components.CopyTo(array, 0);
                    for (int i = 0; i < array.Length; i++)
                    {
                        var disposable = array[i] as IDisposable;
                        if (disposable != null)
                        {
                            disposable.Dispose();
                        }
                    }
                }
            }

            base.Dispose(disposeManagedResources);
        }
        /// <summary>Called after the Game and GraphicsDevice are created, but before LoadContent.  Reference page contains code sample.</summary>
        public override void Initialize()
        {
            // Setup the graphics device if it was not already setup.
            InitializePendingGameSystems();
            base.Initialize();
        }

        private void InitializePendingGameSystems(bool loadContent = false)
        {
            // Add all game systems that were added to this game instance before the game started.
            while (pendingGameSystems.Count != 0)
            {
                pendingGameSystems[0].Initialize();

                if (loadContent && pendingGameSystems[0] is IContentable)
                {
                    ((IContentable)pendingGameSystems[0]).LoadContent();
                }

                pendingGameSystems.RemoveAt(0);
            }
        }
        /// <summary>
        /// Loads the content.
        /// </summary>
        protected override void LoadContent()
        {
            LoadContentSystems();
            contentLoaded = true;
            base.LoadContent();
        }

        private void LoadContentSystems()
        {
            lock (contentableGameSystems)
            {
                foreach (var contentable in contentableGameSystems)
                {
                    currentlyContentGameSystems.Add(contentable);
                }
            }

            foreach (var contentable in currentlyContentGameSystems)
            {
                contentable.LoadContent();
            }

            currentlyContentGameSystems.Clear();
        }
        /// <summary>
        /// Called when graphics resources need to be unloaded. Override this method to unload any game-specific graphics resources.
        /// </summary>
        protected override void UnloadContent()
        {
            // Dispose all objects allocated from content
            base.DisposeCollector.DisposeAndClear();

            lock (contentableGameSystems)
            {
                foreach (var contentable in contentableGameSystems)
                {
                    currentlyContentGameSystems.Add(contentable);
                }
            }

            foreach (var contentable in currentlyContentGameSystems)
            {
                contentable.UnloadContent();
            }

            currentlyContentGameSystems.Clear();
            contentLoaded = false;
            base.UnloadContent();
        }
        /// <summary>
        /// Reference page contains links to related conceptual articles.
        /// </summary>
        /// <param name="gameTime">
        /// Time passed since the last call to Update.
        /// </param>
        public override void Update(GameTime gameTime)
        {
            if (!contentLoaded)
                return;
            lock (updateableGameSystems)
            {
                foreach (var updateable in updateableGameSystems)
                {
                    currentlyUpdatingGameSystems.Add(updateable);
                }
            }

            foreach (var updateable in currentlyUpdatingGameSystems)
            {
                if (updateable.Enabled)
                {
                    updateable.Update(gameTime);
                }
            }

            currentlyUpdatingGameSystems.Clear();
            base.Update(gameTime);
            //isFirstUpdateDone = true;
        }
        private void Components_ItemRemoved(object sender, ObservableCollectionEventArgs<GameComponent> e)
        {
            var gameSystem = e.Item;
            if (this.Enabled)
            {
                pendingGameSystems.Remove(gameSystem);
            }

            var contentableSystem = gameSystem as IContentable;
            if (contentableSystem != null)
            {
                lock (contentableGameSystems)
                {
                    contentableGameSystems.Remove(contentableSystem);
                }
                if (this.Enabled)
                {
                    contentableSystem.UnloadContent();
                }
            }

            var gameComponent = gameSystem as IUpdateable;
            if (gameComponent != null)
            {
                lock (updateableGameSystems)
                {
                    updateableGameSystems.Remove(gameComponent);
                }

                gameComponent.UpdateOrderChanged -= updateableGameSystem_UpdateOrderChanged;
            }

            var item = gameSystem as IDrawable;
            if (item != null)
            {
                lock (drawableGameSystems)
                {
                    drawableGameSystems.Remove(item);
                }

                item.DrawOrderChanged -= drawableGameSystem_DrawOrderChanged;
            }
        }

        private static bool AddGameSystem<T>(T gameSystem, List<T> gameSystems, IComparer<T> comparer, Comparison<T> orderComparer, bool removePreviousSystem = false)
        {
            lock (gameSystems)
            {
                // If we are updating the order
                if (removePreviousSystem)
                {
                    gameSystems.Remove(gameSystem);
                }

                // Find this gameSystem
                int index = gameSystems.BinarySearch(gameSystem, comparer);
                if (index < 0)
                {
                    // If index is negative, that is the bitwise complement of the index of the next element that is larger than item 
                    // or, if there is no larger element, the bitwise complement of Count.
                    index = ~index;

                    // Iterate until the order is different or we are at the end of the list
                    while ((index < gameSystems.Count) && (orderComparer(gameSystems[index], gameSystem) == 0))
                    {
                        index++;
                    }

                    gameSystems.Insert(index, gameSystem);

                    // True, the system was inserted
                    return true;
                }
            }

            // False, it is already in the list
            return false;
        }

        private void Components_ItemAdded(object sender, ObservableCollectionEventArgs<GameComponent> e)
        {
            var component = e.Item;

            // If the game is already running, then we can initialize the game system now
            if (this.Enabled)
            {
                component.Initialize();
            }
            else
            {
                // else we need to initialize it later
                pendingGameSystems.Add(component);
            }

            // Add a contentable system to a separate list
            var contentableSystem = component as IContentable;
            if (contentableSystem != null)
            {
                lock (contentableGameSystems)
                {
                    if (!contentableGameSystems.Contains(contentableSystem))
                    {
                        contentableGameSystems.Add(contentableSystem);
                    }
                }

                if (this.Enabled)
                {
                    // Load the content of the system if the game is already running
                    contentableSystem.LoadContent();
                }
            }

            // Add an updateable system to the separate list
            var updateableGameSystem = component as IUpdateable;
            if (updateableGameSystem != null && AddGameSystem(updateableGameSystem, updateableGameSystems, UpdateableSearcher.Default, UpdateableComparison))
            {
                updateableGameSystem.UpdateOrderChanged += updateableGameSystem_UpdateOrderChanged;
            }

            // Add a drawable system to the separate list
            var drawableGameSystem = component as IDrawable;
            if (drawableGameSystem != null && AddGameSystem(drawableGameSystem, drawableGameSystems, DrawableSearcher.Default, DrawableComparison))
            {
                drawableGameSystem.DrawOrderChanged += drawableGameSystem_DrawOrderChanged;
            }
        }
        private static int UpdateableComparison(IUpdateable left, IUpdateable right)
        {
            return left.UpdateOrder.CompareTo(right.UpdateOrder);
        }

        private static int DrawableComparison(IDrawable left, IDrawable right)
        {
            return left.DrawOrder.CompareTo(right.DrawOrder);
        }

        private void drawableGameSystem_DrawOrderChanged(object sender, EventArgs e)
        {
            AddGameSystem((IDrawable)sender, drawableGameSystems, DrawableSearcher.Default, DrawableComparison, true);
        }
        private void updateableGameSystem_UpdateOrderChanged(object sender, EventArgs e)
        {
            AddGameSystem((IUpdateable)sender, updateableGameSystems, UpdateableSearcher.Default, UpdateableComparison, true);
        }       
        /// <summary>
        /// The comparer used to order <see cref="IDrawable"/> objects.
        /// </summary>
        internal struct DrawableSearcher : IComparer<IDrawable>
        {
            public static readonly DrawableSearcher Default = new DrawableSearcher();

            public int Compare(IDrawable left, IDrawable right)
            {
                if (Equals(left, right))
                {
                    return 0;
                }

                if (left == null)
                {
                    return 1;
                }

                if (right == null)
                {
                    return -1;
                }

                return (left.DrawOrder < right.DrawOrder) ? -1 : 1;
            }
        }
        /// <summary>
        /// The comparer used to order <see cref="IUpdateable"/> objects.
        /// </summary>
        internal struct UpdateableSearcher : IComparer<IUpdateable>
        {
            public static readonly UpdateableSearcher Default = new UpdateableSearcher();

            public int Compare(IUpdateable left, IUpdateable right)
            {
                if (Equals(left, right))
                {
                    return 0;
                }

                if (left == null)
                {
                    return 1;
                }

                if (right == null)
                {
                    return -1;
                }

                return (left.UpdateOrder < right.UpdateOrder) ? -1 : 1;
            }
        }
    }
}
