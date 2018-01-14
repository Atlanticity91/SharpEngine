using Microsoft.Xna.Framework;

using SharpEngine.Kernel;
using SharpEngine.Kernel.Processors;
using SharpEngine.Kernel.Processors.Types;

using System.Collections.Generic;

namespace SharpEngine.AI {

    public class AIProcessor : GameProcessor<StateMachine>, IUProcessor {

        private AIProcessor MyRef;
        private Dictionary<string, GameAIState > States;

        /// <summary>
        /// Constructor
        /// </summary>
        public AIProcessor( ) : base( ) {
            this.MyRef = this;
            this.States = new Dictionary<string, GameAIState>( );
        }

        /// <summary>
        /// Register a state to the current game A.I. processor.
        /// </summary>
        /// <typeparam name="Type" >Type of the state to register</typeparam>
        /// <param name="name" >Name of the state to register</param>
        /// <returns>The current A.I. processor</returns>
        public AIProcessor Register<Type>( string name ) where Type : GameAIState, new( ) {
            if ( !this.States.ContainsKey( name ) )
                this.States.Add( name, new Type( ) );

            return this;
        }

        /// <summary>
        /// Switch the current state of a state machine.
        /// </summary>
        /// <param name="target" >The new state of the state machine</param>
        /// <param name="owner" >Index of the game oject who "own" the state machine component</param>
        /// <param name="ai_processor" >Reference to the current A.I. processor</param>
        /// <param name="game_time" >Reference to the current game time</param>
        /// <param name="world" >Reference to the current game world</param>
        /// <param name="processor_manager" >Reference to the current game processor manager</param>
        public void SwitchState( string target, int owner, ref AIProcessor ai_processor, ref GameTime game_time, ref GameWorld world, ref GameProcessorManager processor_manager ) {
            var machine = this.Content[ owner ];

            this.States[ machine.CurrentState ].OnQuit( owner, ref ai_processor, ref game_time, ref world, ref processor_manager );
            machine.CurrentState = target;

            this.States[ target ].OnEnter( owner, ref ai_processor, ref game_time, ref world, ref processor_manager );
        }

        /// <summary>
        /// Process the current game AI processor.
        /// </summary>
        /// <param name="game_time" >Reference to the current game time</param>
        /// <param name="world" >Reference to the current world</param>
        /// <param name="processor_manager" >Reference to the current game processor manager</param>
        public void Process( ref GameTime game_time, ref GameWorld world, ref GameProcessorManager processor_manager ) {
            while ( this.GetNext( ) ) {
                if ( !this.Current.HasChanged )
                    this.States[ this.Current.CurrentState ].Process( this.CurrentID, ref this.MyRef, ref game_time, ref world, ref processor_manager );
                else {
                    this.Current.HasChanged = false;
                    this.States[ this.Current.CurrentState ].OnEnter( this.CurrentID, ref this.MyRef, ref game_time, ref world, ref processor_manager );
                }
            }
        }

    }

}
