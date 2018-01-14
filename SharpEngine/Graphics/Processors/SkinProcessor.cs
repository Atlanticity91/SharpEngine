using Microsoft.Xna.Framework;

using SharpEngine.Graphics.Components;
using SharpEngine.Graphics.Rendering;
using SharpEngine.Kernel;
using SharpEngine.Kernel.Processors;
using SharpEngine.Kernel.Processors.Types;

namespace SharpEngine.Graphics.Processors {

    public class SkinProcessor : GameProcessor<Skin>, IDProcessor {

        private Vector2 Position;
        private Vector2 Scale;

        private float MinX;
        private float MinY;
        private float MaxX;
        private float MaxY;

        /// <summary>
        /// Constructor
        /// </summary>
        public SkinProcessor( ) : base( ) {
            this.MinX = -1f;
            this.MinY = -1f;
            this.MaxX = 1280f;
            this.MaxY = 720f;
        }

        /// <summary>
        /// Process the current game skin processor.
        /// </summary>
        /// <param name="renderer" >Reference to the current game renderer</param>
        /// <param name="world" >Reference to the current game world</param>
        /// <param name="processor_manager" >Reference to the current game processor manager</param>
        public void Process( ref GameRenderer renderer, ref GameWorld world, ref GameProcessorManager processor_manager ) {
            while ( this.GetNext( ) ) {
                this.Position = world.GetPosition( this.CurrentID );
                this.Scale = world.GetScale( this.CurrentID );

                if ( this.Position.X > this.MinX && this.Position.Y > this.MinY && this.Position.X < this.MaxX && this.Position.Y < this.MaxY && this.Scale != Vector2.Zero ) {
                    renderer.Push(
                        this.Current.Shader,
                        this.Current.SpriteSheet,
                        ref this.Position,
                        this.Current.Sprite,
                        this.Current.Filter,
                        world.GetRotation( this.CurrentID ),
                        ref this.Scale,
                        this.Current.Depth
                    );
                }
            }
        }

    }

}
