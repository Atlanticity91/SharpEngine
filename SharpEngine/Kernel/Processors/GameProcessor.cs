namespace SharpEngine.Kernel.Processors {

    public class GameProcessor<Type> : GameProcessorCore where Type : GameComponent, new( ) {

        protected int CurrentID;
        protected Type Current;
        protected Type[ ] Content;

        /// <summary>
        /// Constructor
        /// </summary>
        public GameProcessor( ) : base( ) {
            this.CurrentID = -1;
            this.Current = default( Type );
        }

        /// <summary>
        /// Awake the game processor.
        /// </summary>
        /// <param name="world_size" >Size of the game world</param>
        public override void Awake( int world_size ) {
            this.Size = world_size;
            this.Content = new Type[ world_size ];
            for ( this.CurrentID = 0; this.CurrentID < world_size; this.CurrentID++ )
                this.Content[ this.CurrentID ] = new Type( );

            this.CurrentID = 0;
        }

        /// <summary>
        /// Add a game component to the current processor.
        /// </summary>
        /// <param name="index" >Index of the owner on the game world</param>
        /// <returns>The new game component of the processor</returns>
        public override GameComponent Add( int index ) {
            if ( index > -1 && index < this.Size ) {
                this.Content[ index ].IsEnable = true;
                this.Count++;

                return this.Content[ index ];
            }

            return default( Type );
        }

        /// <summary>
        /// Get a component from the game processor.
        /// </summary>
        /// <param name="game_object" >Index of the game object who "own" the component</param>
        /// <returns>The query component</returns>
        public override GameComponent Get( int game_object ) {
            return this.Content[ game_object ];
        }

        /// <summary>
        /// Get if a component is used.
        /// </summary>
        /// <param name="game_object" >Index of the game object who "own" the component</param>
        /// <returns>True if is in use</returns>
        public override bool IsUsed( int game_object ) {
            return this.Content[ game_object ].IsEnable;
        }

        /// <summary>
        /// Remove a game component from the processor.
        /// </summary>
        /// <param name="index" >Index of the game component</param>
        public override void Remove( int index ) {
            this.Content[ index ].IsEnable = false;
            this.Count--;
        }

        /// <summary>
        /// Delete a game component from the current processor.
        /// </summary>
        /// <param name="index" >Index of the component</param>
        public override void Delete( int index ) {
            if ( index > -1 && index < this.Size ) {
                while ( index < this.Size )
                    this.Content[ index++ ] = this.Content[ index ];

                this.Content[ index ].IsEnable = false;
                this.Count--;
            }
        }

        /// <summary>
        /// Get the next game component who can be process by the processor.
        /// </summary>
        /// <returns>True of false</returns>
        protected virtual bool GetNext( ) {
            do {
                this.CurrentID++;
            } while ( this.CurrentID < this.Size && !this.Content[ this.CurrentID ].IsEnable );

            if ( this.CurrentID < this.Size ) {
                this.Current = this.Content[ this.CurrentID ];

                return true;
            }

            this.CurrentID = -1;
            this.Current = default( Type );

            return false;
        }

    }

}
