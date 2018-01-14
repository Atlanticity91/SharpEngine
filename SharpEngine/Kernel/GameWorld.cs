using Microsoft.Xna.Framework;

using SharpEngine.Audio;
using SharpEngine.Graphics.Rendering;
using SharpEngine.Kernel.Processors;
using SharpEngine.Utils;

namespace SharpEngine.Kernel {

    public abstract class GameWorld : GamePool<GameObject> {

        protected GameWorld MyRef;
        protected int Current;

        private Vector2 Temp;
        private float TempRot;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="size" >Size of the game world</param>
        public GameWorld( int size ) : base( size ) {
            this.MyRef = this;
        }

        /// <summary>
        /// Awake the current game world.
        /// </summary>
        /// <param name="processor_manager" >Reference to the current game processor manager</param>
        public abstract void Awake( ref GameProcessorManager processor_manager );

        /// <summary>
        /// Create a new game object.
        /// </summary>
        /// <param name="x" >Default game object x value</param>
        /// <param name="y" >Default game object y value</param>
        /// <returns>The new game object</returns>
        public virtual int Create( float x, float y ) {
            if ( this.Pointer < this.Size ) {
                this.Content[ this.Pointer ].Setup( x, y, 0f, 1f, 1f );

                return this.Pointer++;
            }

            return -1;
        }

        /// <summary>
        /// Create a new game object.
        /// </summary>
        /// <typeparam name="Type" >Type of the new game object</typeparam>
        /// <param name="x" >Default game object x value</param>
        /// <param name="y" >Default game object y value</param>
        /// <param name="processor_manager" >Reference to the current game processor manager</param>
        /// <returns>The new game object</returns>
        public virtual int Create<Type>( float x, float y, ref GameProcessorManager processor_manager ) where Type : GameObject, new( ) {
            if ( this.Pointer < this.Size ) {
                int temp = this.Pointer++;

                this.Content[ temp ] = new Type( );
                this.Content[ temp ].Setup( x, y, 0f, 1f, 1f );
                this.Content[ temp ].Init( temp, ref this.MyRef, ref processor_manager );

                return temp;
            }

            return -1;
        }

        /// <summary>
        /// Get a game object from the current game world.
        /// </summary>
        /// <typeparam name="Type" >Type of the query game object</typeparam>
        /// <param name="game_object" >Index of the game object on the current game world</param>
        /// <returns>The query game object</returns>
        public virtual Type Get<Type>( int game_object ) where Type : GameObject {
            return (Type)this.Content[ game_object ];
        }

        /// <summary>
        /// Get the state of a game object of the current game world.
        /// </summary>
        /// <param name="game_object" >Index of the query game object</param>
        /// <returns>True if the game object is active</returns>
        public virtual bool GetStateOf( int game_object ) {
            return this.Content[ game_object ].IsActive;
        }

        /// <summary>
        /// Set the state of a game object of the current game world.
        /// </summary>
        /// <param name="game_object" >Index of the game world</param>
        /// <param name="state" >New state of the game object</param>
        public virtual void SetStateOf( int game_object, bool state ) {
            this.Content[ game_object ].IsActive = state;
        }

        /// <summary>
        /// Toggle a game object of the current game world.
        /// </summary>
        /// <param name="game_object" >Index of the game object</param>
        public virtual void Toggle( int game_object ) {
            this.Content[ game_object ].IsActive = !this.Content[ game_object ].IsActive;
        }

        /// <summary>
        /// Move a game object.
        /// </summary>
        /// <param name="game_object" >Index of the query game object</param>
        /// <param name="position" >Reference to the position offset of the game object</param>
        public virtual void Move( int game_object, ref Vector2 position ) {
            this.Content[ game_object ].Position.X += position.X;
            this.Content[ game_object ].Position.Y += position.Y;
        }

        /// <summary>
        /// Rotate a game object.
        /// </summary>
        /// <param name="game_object" >Index of the query game object</param>
        /// <param name="angle" >Rotation angle value</param>
        public virtual void Rotate( int game_object, float angle ) {
            this.Content[ game_object ].Rotation += angle;
        }

        /// <summary>
        /// Scale a game object.
        /// </summary>
        /// <param name="game_object" >Index of the query game object</param>
        /// <param name="x" >Scale x value</param>
        /// <param name="y" >Scale y value</param>
        public virtual void Scale( int game_object, float x, float y ) {
            this.Content[ game_object ].Scale.X *= x;
            this.Content[ game_object ].Scale.Y *= y;
        }

        /// <summary>
        /// Remove game object from the game world.
        /// </summary>
        /// <param name="game_object" >Index of the game object</param>
        /// <param name="processor_manager" >Reference to the current game processor manager</param>
        public virtual void Remove( int game_object, ref GameProcessorManager processor_manager ) {
            this.Content[ game_object ].Cleanup( game_object, ref this.MyRef, ref processor_manager );
            this.Remove( game_object );
            processor_manager.DeleteComponentOf( game_object );
        }

        /// <summary>
        /// Get the position of a game object on the game world.
        /// </summary>
        /// <param name="game_object" >index of the query game object</param>
        /// <returns>Reference to the position of the game object</returns>
        public virtual ref Vector2 GetPosition( int game_object ) {
            this.Temp = Vector2.Zero;

            do {
                this.Temp += this.Content[ game_object ].Position;
                game_object = this.Content[ game_object ].Parent;
            } while ( game_object > -1 );

            return ref this.Temp;
        }

        /// <summary>
        /// Get the rotation of a game object on the game world.
        /// </summary>
        /// <param name="game_object" >index of the query game object</param>
        /// <returns>The value of the game object rotation</returns>
        public virtual float GetRotation( int game_object ) {
            this.TempRot = 0f;

            do {
                this.TempRot += this.Content[ game_object ].Rotation;
                game_object = this.Content[ game_object ].Parent;
            } while ( game_object > -1 );

            return this.TempRot;
        }

        /// <summary>
        /// Get the scale of a game object on the game world.
        /// </summary>
        /// <param name="game_object" >index of the query game object</param>
        /// <returns>Reference to the scale of the game object</returns>
        public virtual ref Vector2 GetScale( int game_object ) {
            this.Temp = Vector2.One;

            do {
                this.Temp *= this.Content[ game_object ].Scale;
                game_object = this.Content[ game_object ].Parent;
            } while ( game_object > -1 );

            return ref this.Temp;
        }

        /// <summary>
        /// Process the current game world.
        /// </summary>
        /// <param name="processor_manager" >Reference to the current game processor manager</param>
        /// <param name="renderer" >Reference to the current game renderer</param>
        /// <param name="audio_manager" >Reference to the current game audio manager</param>
        public virtual void Process( ref GameProcessorManager processor_manager, ref GameRenderer renderer, ref AudioManager audio_manager ) {
            this.Current = 0;

            while ( this.Current < this.Size ) {
                if ( this.Content[ this.Current ].IsActive )
                    if ( this.Content[ this.Current ].UseProcess )
                        this.Content[ this.Current ].Process( this.Current, ref this.MyRef, ref processor_manager, ref renderer, ref audio_manager );

                this.Current++;
            }
        }

    }

}
