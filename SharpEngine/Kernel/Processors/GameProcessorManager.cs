using Microsoft.Xna.Framework;

using SharpEngine.Graphics.Rendering;
using SharpEngine.Kernel.Processors.Types;

using System.Collections.Generic;

namespace SharpEngine.Kernel.Processors {

    public class GameProcessorManager {

        private GameProcessorManager MyRef;
        private int WorldSize;
        private int Count;
        private int Current;
        private int Finder;
        private List<GameProcessorCore> Processors;

        /// <summary>
        /// Constructor
        /// </summary>
        public GameProcessorManager( ) {
            this.MyRef = this;
            this.Count = 0;
            this.Current = 0;
            this.Processors = new List<GameProcessorCore>( );
        }

        /// <summary>
        /// Awake the current game processor manager.
        /// </summary>
        public void Awake( int world_size ) {
            this.WorldSize = world_size;
        }

        /// <summary>
        /// Register a new game processor to the current game processor manager.
        /// </summary>
        /// <typeparam name="Type" >Type of the new game processor</typeparam>
        /// <returns>The new game processor</returns>
        public Type Register<Type>( ) where Type : GameProcessorCore, new() {
            Type temp = new Type( );
            temp.Awake( this.WorldSize );
            this.Processors.Add( temp );

            return (Type)this.Processors[ this.Count++ ];
        }

        /// <summary>
        /// Find the processor for a certain game component type.
        /// </summary>
        /// <typeparam name="Type" >Type of the game component</typeparam>
        /// <returns>The index of the game processor</returns>
        public int Find<Type>( ) where Type : GameComponent, new( ) {
            this.Finder = 0;

            while ( this.Finder < this.Count ) {
                if ( !( this.Processors[ this.Finder ] is GameProcessor<Type> ) )
                    this.Finder++;
                else
                    return this.Finder;
            }

            return -1;
        }

        /// <summary>
        /// Add a component to a game object.
        /// </summary>
        /// <typeparam name="Type" >Type of the component</typeparam>
        /// <param name="game_object" >Index of the game object who "own" the new component</param>
        /// <returns></returns>
        public Type AddComponenTo<Type>( int game_object ) where Type : GameComponent, new( ) {
            if ( this.Find<Type>( ) > -1 )
                return (Type)this.Processors[ this.Finder ].Add( game_object );

            return default( Type );
        }

        /// <summary>
        /// Get if a game object has a certain component.
        /// </summary>
        /// <typeparam name="Type" >Type of the component</typeparam>
        /// <param name="game_object" >Index of the game object who "own" the new component</param>
        /// <returns>True if the game object has the component</returns>
        public bool HasComponent<Type>( int game_object ) where Type : GameComponent, new( ) {
            if ( this.Find<Type>( ) > -1 )
                return this.Processors[ this.Finder ].IsUsed( game_object );

            return false;
        }

        /// <summary>
        /// Get a component of the game object.
        /// </summary>
        /// <typeparam name="Type" >Type of the component</typeparam>
        /// <param name="game_object" >Index of the game object who "own" the new component</param>
        /// <returns>The query component</returns>
        public Type GetComponenOf<Type>( int game_object ) where Type : GameComponent, new( ) {
            if ( this.Find<Type>( ) > -1 )
                return (Type)this.Processors[ this.Finder ].Get( game_object );

            return default( Type );
        }

        /// <summary>
        /// Remove a component of a game object.
        /// </summary>
        /// <typeparam name="Type" >Type of the component</typeparam>
        /// <param name="game_object" >Index of the game object who "own" the new component</param>
        public void RemoveComponentOf<Type>( int game_object ) where Type : GameComponent, new( ) {
            if ( this.Find<Type>( ) > -1 )
                this.Processors[ this.Finder ].Remove( game_object );
        }

        /// <summary>
        /// Remove all component of a game object. ( Remove disable components )
        /// </summary>
        /// <param name="game_object" >Index of the game object who "own" the new component</param>
        public void RemoveAllComponentOf( int game_object ) {
            foreach ( GameProcessor<GameComponent> processor in this.Processors )
                processor.Remove( game_object );
        }

        /// <summary>
        /// Delete all component of a game object. 
        /// ( Delete disable and shift all components to the right; only do when the "owner" is 
        ///   removed from the game world )
        /// </summary>
        /// <param name="game_object" >Index of the game object who "own" the new component</param>
        public void DeleteComponentOf( int game_object ) {
            foreach ( GameProcessor<GameComponent> processor in this.Processors )
                processor.Delete( game_object );
        }

        /// <summary>
        /// Update the current game processor manager.
        /// </summary>
        /// <param name="game_time" >Reference to the current game time</param>
        /// <param name="world" >Reference to the current game world</param>
        public void Update( ref GameTime game_time, ref GameWorld world ) {
            for ( this.Current = 0; this.Current < this.Count; this.Current++ ) {
                if ( this.Processors[ this.Current ].IsActive )
                    if ( this.Processors[ this.Current ] is IUProcessor )
                        ( (IUProcessor)this.Processors[ this.Current ] ).Process( ref game_time, ref world, ref this.MyRef );
            }
        }

        /// <summary>
        /// Draw the current game processor manager.
        /// </summary>
        /// <param name="renderer" >Reference to the current game renderer</param>
        /// <param name="world" >Reference to the current game world</param>
        public void Draw( ref GameRenderer renderer, ref GameWorld world ) {
            for ( this.Current = 0; this.Current < this.Count; this.Current++ ) {
                if ( this.Processors[ this.Current ].IsActive )
                    if ( this.Processors[ this.Current ] is IDProcessor )
                        ( (IDProcessor)this.Processors[ this.Current ] ).Process( ref renderer, ref world, ref this.MyRef );
            }
        }

    }

}
