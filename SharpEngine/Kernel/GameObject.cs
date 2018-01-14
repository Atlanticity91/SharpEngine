using Microsoft.Xna.Framework;

using SharpEngine.Audio;
using SharpEngine.Graphics.Rendering;
using SharpEngine.Kernel.Processors;

namespace SharpEngine.Kernel {

    public class GameObject {

        public bool IsActive;
        public bool UseProcess;
        public int Parent;
        public Vector2 Position;
        public float Rotation;
        public Vector2 Scale;

        /// <summary>
        /// Constructor
        /// </summary>
        public GameObject( ) {
            this.IsActive = false;
            this.UseProcess = false;
            this.Parent = -1;
            this.Position = new Vector2( 0f );
            this.Rotation = 0f;
            this.Scale = new Vector2( 1f );
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="use_process" >If the game object use process</param>
        public GameObject( bool use_process ) {
            this.IsActive = false;
            this.UseProcess = use_process;
            this.Parent = -1;
            this.Position = new Vector2( 0f );
            this.Rotation = 0f;
            this.Scale = new Vector2( 1f );
        }

        /// <summary>
        /// Setup the game object.
        /// </summary>
        /// <param name="x" >Default x value</param>
        /// <param name="y" >Default y value</param>
        /// <param name="rotation" >Default rotation value</param>
        /// <param name="scale_x" >Default scale x value</param>
        /// <param name="scale_y" >Default scale y value</param>
        public virtual void Setup( float x, float y, float rotation, float scale_x, float scale_y ) {
            this.IsActive = true;

            this.Position.X = x;
            this.Position.Y = y;

            this.Rotation = rotation;

            this.Scale.X = scale_x;
            this.Scale.Y = scale_y;
        }

        /// <summary>
        /// Cleanup the game object.
        /// </summary>
        /// <param name="me" >Index of the game object on the world</param>
        /// <param name="world" >Reference to the current game world</param>
        /// <param name="processor_manager" >Reference to the current game processor manager</param>
        public virtual void Cleanup( int me, ref GameWorld world, ref GameProcessorManager processor_manager ) {
            this.IsActive = false;
        }

        /// <summary>
        /// Set the position of the game object.
        /// </summary>
        /// <param name="x" >New game object x value</param>
        /// <param name="y" >New game object y value</param>
        public void SetPosition( float x, float y ) {
            this.Position.X = x;
            this.Position.Y = y;
        }

        /// <summary>
        /// Set the scale of the game object.
        /// </summary>
        /// <param name="x" >New game object x scale value</param>
        /// <param name="y" >New game object y scale value</param>
        public void SetScale( float x, float y ) {
            this.Scale.X = x;
            this.Scale.Y = y;
        }

        /// <summary>
        /// Initialize the game object.
        /// </summary>
        /// <param name="me" >Index of the current game object on the game world</param>
        /// <param name="world" >Reference of the current game world</param>
        /// <param name="processor_manager" >Reference to the current game processor manager</param>
        /// <returns>The current game object</returns>
        public virtual GameObject Init( int me, ref GameWorld world, ref GameProcessorManager processor_manager ) {
            // NOTING BY DEFAULT
            return this;
        }

        /// <summary>
        /// Process the game object.
        /// </summary>
        /// <param name="me" >Index of the current game object on the game world</param>
        /// <param name="world" >Reference to the current game world</param>
        /// <param name="processor_manager" >Reference to the current game processor manager</param>
        /// <param name="renderer" >Reference to the current game renderer</param>
        /// <param name="audio_manager" >Reference to the current game audio manager</param>
        public virtual void Process( int me, ref GameWorld world, ref GameProcessorManager processor_manager, ref GameRenderer renderer, ref AudioManager audio_manager ) {
            // NOTING BY DEFAULT
        }

    }

}
