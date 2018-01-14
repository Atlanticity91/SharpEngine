namespace SharpEngine.Kernel.Processors { 

    public abstract class GameProcessorCore {

        protected int Size;
        protected int Count;

        /// <summary>
        /// Get if the game processor is active.
        /// </summary>
        public bool IsActive {
            get { return ( this.Count > 0 ); }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public GameProcessorCore( ) {
            this.Size = 0;
            this.Count = 0;
        }

        /// <summary>
        /// Awake the game processor.
        /// </summary>
        /// <param name="world_size" >Size of the game world</param>
        public abstract void Awake( int world_size );

        /// <summary>
        /// Add a game component to the processor.
        /// </summary>
        /// <param name="game_object" >Index of the game object who "own" the new component</param>
        /// <returns>The new component</returns>
        public abstract GameComponent Add( int game_object );

        /// <summary>
        /// Get a component from the processor.
        /// </summary>
        /// <param name="game_object" >Index of the game object who "own" the new component</param>
        /// <returns>The query component</returns>
        public abstract GameComponent Get( int game_object );

        /// <summary>
        /// Get if a component is used.
        /// </summary>
        /// <param name="game_object" >Index of the game object who "own" the new component</param>
        /// <returns>True if the component is in use</returns>
        public abstract bool IsUsed( int game_object );

        /// <summary>
        /// Remove a component from the processor.
        /// </summary>
        /// <param name="game_object" >Index of the game object who "own" the new component</param>
        public abstract void Remove( int game_object );

        /// <summary>
        /// Delete a component from the processor.
        /// </summary>
        /// <param name="game_object" >Index of the game object who "own" the new component</param>
        public abstract void Delete( int game_object );

    }

}
