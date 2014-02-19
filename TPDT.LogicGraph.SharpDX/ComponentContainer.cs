using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPDT.LogicGraph.SharpDXGame
{
    class ComponentContainer:DrawableGameComponent
    {
        public GameComponentCollection Components { get; set; }
        public DrawableComponentCollection DrawableComponents { get; set; }

        public ComponentContainer(Game2DBase game)
            : base(game)
        {

        }
        public override void Initialize()
        {
            Components = new GameComponentCollection();
            DrawableComponents = new DrawableComponentCollection();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            for (int i = 0; i < DrawableComponents.Count; i++)
            {
                if (DrawableComponents[i].Enabled)
                    DrawableComponents[i].Initialize();
            }
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            for (int i = 0; i < DrawableComponents.Count; i++)
            {
                if (DrawableComponents[i].Visible)
                    DrawableComponents[i].Draw(gameTime);
            }
            base.Draw(gameTime);
        }
        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < DrawableComponents.Count; i++)
            {
                if (Components[i].Enabled)
                    Components[i].Update(gameTime);
            }
            base.Update(gameTime);
        }

        public void AddComponent(GameComponent component)
        {
            this.Components.Add(component);
            if (component is DrawableGameComponent)
                this.DrawableComponents.Add(component as DrawableGameComponent);
        }
    }
}
