namespace SharpEngine.Utils {

    public class GamePool<Type> where Type : class, new( ) {

        public int Size {
            get;
            private set;
        }

        public int Pointer {
            get;
            protected set;
        }

        public Type Default {
            get;
            private set;
        }

        public Type[ ] Content {
            get;
            protected set;
        }

        /// <summary>
        /// Constructor I
        /// </summary>
        public GamePool( ) {
            this.Size = 0;
            this.Pointer = 0;
            this.Default = default( Type );
            this.Content = null;
        }

        /// <summary>
        /// Constructor II
        /// </summary>
        /// <param name="size" >Size of the new pool</param>
        public GamePool( int size ) {
            this.Size = size;
            this.Pointer = 0;
            this.Default = new Type( );
            this.Content = new Type[ size ];
        }

        /// <summary>
        /// Setup the game pool.
        /// </summary>
        /// <param name="size" >Size of the game pool</param>
        public virtual void Setup( int size ) {
            this.Size = size;
            this.Default = new Type( );
            this.Content = new Type[ size ];
            for ( this.Pointer = 0; this.Pointer < size; this.Pointer++ )
                this.Content[ this.Pointer ] = new Type( );

            this.Pointer = 0;
        }

        /// <summary>
        /// Awake the game pool.
        /// </summary>
        public virtual void Awake( ) {
            for ( ; this.Pointer < this.Size; this.Pointer++ )
                this.Content[ this.Pointer ] = new Type( );

            this.Pointer = 0;
        }

        /// <summary>
        /// Add object to the current pool.
        /// </summary>
        /// <returns>The new object of the pool</returns>
        public virtual Type Add( ) {
            if ( this.Pointer < this.Size )
                return this.Content[ this.Pointer++ ];

            return default( Type );
        }

        /// <summary>
        /// Get if an object exist on the current pool.
        /// </summary>
        /// <param name="index" >Index of the object</param>
        /// <returns>True of false</returns>
        public virtual bool Exist( int index ) {
            return ( index > -1 ) && ( index < this.Pointer );
        }

        /// <summary>
        /// Get an object from the current pool.
        /// </summary>
        /// <param name="index" >Index of the object</param>
        /// <returns>The query object of the pool</returns>
        public virtual Type Get( int index ) {
            if ( index > -1 && index < this.Size )
                return this.Content[ index ];

            return default( Type );
        }

        /// <summary>
        /// Get fast value of the current game pool.
        /// </summary>
        /// <param name="index" >Index of the element</param>
        /// <returns>The query element</returns>
        public virtual ref Type FastGet( int index ) {
            return ref this.Content[ index ];
        }

        /// <summary>
        /// Remove an object from the current pool.
        /// </summary>
        /// <param name="index" >Index of the object</param>
        public virtual void Remove( int index ) {
            if ( index > -1 && index < this.Pointer ) {
                while ( index < this.Pointer )
                    this.Content[ index++ ] = this.Content[ index ];

                this.Content[ --this.Pointer ] = this.Default;
            }
        }

    }

}
